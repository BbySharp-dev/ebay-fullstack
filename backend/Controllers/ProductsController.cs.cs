using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data;
using Backend.DTOs.Product;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EBayDbContext _context;
        private readonly IMapper _mapper;
        private readonly IS3ImageService _s3Service;

        public ProductsController(
            EBayDbContext context,
            IMapper mapper,
            IS3ImageService s3Service)
        {
            _context = context;
            _mapper = mapper;
            _s3Service = s3Service;
        }

        // GET: api/products?pageNumber=1&pageSize=20
        // * LUONG: Query list -> ProjectTo (AutoMapper) -> ToList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductListDto>>> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var products = await _context.Products
                // * AsNoTracking: Khong track changes, tang performance cho READ-ONLY
                .AsNoTracking()
                // Filter: Chi lay san pham chua xoa
                .Where(p => p.Deleted != true)
                // Sort truoc khi phan trang
                .OrderBy(p => p.Name)
                // Pagination
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                // ! ProjectTo: AutoMapper SELECT columns tu DB luon (performance cao)
                // ? Syntax: .ProjectTo<DTO>(_mapper.ConfigurationProvider)
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(products); // 200 + data
        }

        // GET: api/products/5
        // * LUONG: Query entity + Include -> Map (AutoMapper) -> Return DTO
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailDto>> GetProduct(long id)
        {
            var product = await _context.Products
                .AsNoTracking() // Read-only
                                // ! Include: JOIN de lay related data (tranh N+1 query)
                .Include(p => p.ProductImages)  // JOIN ProductImages
                .Include(p => p.Ratings)        // JOIN Ratings
                    .ThenInclude(r => r.Rater)  // JOIN User qua Ratings.Rater
                .FirstOrDefaultAsync(p => p.Id == id && p.Deleted != true);

            if (product == null)
                return NotFound(); // 404

            // ! Map: Entity da co data -> DTO
            // ? Syntax: _mapper.Map<TargetDTO>(sourceEntity)
            var productDto = _mapper.Map<ProductDetailDto>(product);
            return Ok(productDto); // 200 + data
        }

        // POST: api/products
        // * LUONG: Map DTO -> Entity -> Add -> Save -> Reload -> Map lai -> Return
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createDto)
        {
            // ! Map: DTO -> Entity MOI (tao object)
            // ? Syntax: _mapper.Map<TargetEntity>(sourceDto)
            var product = _mapper.Map<Product>(createDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(); // INSERT vao DB, product.Id duoc tao

            // * Reload: Lay lai tu DB de co du data (Id, CreatedAt, navigation...)
            await _context.Entry(product).ReloadAsync();

            // Map lai sang DTO de tra ve
            var productDto = _mapper.Map<ProductDetailDto>(product);

            // 201 Created + Location header + data
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

        // PUT: api/products/5
        // * LUONG: Query entity cu -> Map DTO vao entity -> Save -> Reload -> Return
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDetailDto>> UpdateProduct(long id, UpdateProductDto updateDto)
        {
            // Query entity CU tu DB (KHONG dung AsNoTracking vi can track de update)
            var product = await _context.Products
                .Include(p => p.ProductImages)  // Lay luon de tra ve
                .Include(p => p.Ratings)
                    .ThenInclude(r => r.Rater)
                .FirstOrDefaultAsync(p => p.Id == id && p.Deleted != true);

            if (product == null)
                return NotFound(); // 404

            // ! Map: DTO -> Entity CU (ghi de properties)
            // ? Syntax: _mapper.Map(sourceDto, targetEntity) - GHI DE vao entity
            _mapper.Map(updateDto, product);
            await _context.SaveChangesAsync(); // UPDATE trong DB

            // * Reload: Lay data moi nhat sau UPDATE (neu DB co trigger/computed)
            await _context.Entry(product).ReloadAsync();

            // Map sang DTO de tra ve
            var productDto = _mapper.Map<ProductDetailDto>(product);

            return Ok(productDto); // 200 + data moi
        }

        // DELETE: api/products/5
        // * LUONG: Query entity -> Soft delete -> Save
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            // Chi can query entity, khong can Include (khong tra data)
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.Deleted != true);

            if (product == null)
                return NotFound(); // 404

            // * Soft delete: Danh dau Deleted thay vi xoa that
            product.Deleted = true;
            await _context.SaveChangesAsync(); // UPDATE Deleted = 1

            return NoContent(); // 204 No Content
        }

        // POST: api/products/5/images
        // * LUONG: Validate -> Upload S3 -> Luu DB -> Return DTO
        [HttpPost("{productId}/images")]
        public async Task<ActionResult<ProductImageDto>> UploadImage(
            long productId,
            [FromForm] UploadProductImageDto uploadDto)
        {
            // * 1. Kiem tra Product ton tai
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.Deleted != true);

            if (product == null)
                return NotFound(new { message = "San pham khong ton tai" });

            // * 2. Validate file
            // Max 5MB
            if (uploadDto.File.Length > 5 * 1024 * 1024)
                return BadRequest(new { message = "File size khong duoc vuot qua 5MB" });

            // Chi cho phep JPG, PNG
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
            if (!allowedTypes.Contains(uploadDto.File.ContentType.ToLower()))
                return BadRequest(new { message = "Chi chap nhan file JPG, PNG, WEBP" });

            // * 3. Upload len S3
            string imageUrl;
            try
            {
                imageUrl = await _s3Service.UploadImageAsync(uploadDto.File, "products");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Upload anh that bai", error = ex.Message });
            }

            // * 4. Tao ProductImage entity
            var productImage = new ProductImage
            {
                ProductId = (int)productId, // Cast long to int
                ImageUrl = imageUrl, // S3 URL
                IsPrimary = uploadDto.IsPrimary,
                CreatedAt = DateTime.UtcNow,
                Deleted = false
            };

            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();

            // * 5. Map sang DTO va return
            var imageDto = _mapper.Map<ProductImageDto>(productImage);

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = productId },
                imageDto
            );
        }
        // DELETE: api/products/5/images/3
        // * LUONG: Query -> Xoa S3 -> Soft delete DB -> Return
        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(long productId, long imageId)
        {
            // * 1. Tim ProductImage
            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(i =>
                    i.Id == imageId &&
                    i.ProductId == productId &&
                    i.Deleted != true);

            if (productImage == null)
                return NotFound(new { message = "Anh khong ton tai" });

            // * 2. Xoa khoi S3
            try
            {
                if (!string.IsNullOrEmpty(productImage.ImageUrl))
                    await _s3Service.DeleteImageAsync(productImage.ImageUrl);
            }
            catch (Exception)
            {
                // Log error nhung van tiep tuc soft delete DB
            }

            // * 3. Soft delete trong DB
            productImage.Deleted = true;
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }
    }
}
