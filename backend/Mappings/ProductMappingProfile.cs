using AutoMapper;
using Backend.DTOs.Product;
using Backend.Models;

namespace Backend.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Product → ProductListDto
        // AutoMapper sẽ tự động map các properties cùng tên
        CreateMap<Product, ProductListDto>()
            .ForMember(d => d.ThumbnailUrl, o => o.MapFrom(s => 
                s.ProductImages
                    .Where(i => i.IsPrimary == true)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()))
            .ForMember(d => d.AverageRating, o => o.MapFrom(s => 
                s.Ratings.Any() 
                    ? (decimal)s.Ratings.Average(r => r.RatingScore) 
                    : 0m))
            .ForMember(d => d.ReviewCount, o => o.MapFrom(s => s.Ratings.Count))
            .ForMember(d => d.InStock, o => o.MapFrom(s => s.Stock > 0));
        
        // Product → ProductDetailDto
        CreateMap<Product, ProductDetailDto>()
            .ForMember(d => d.Images, o => o.MapFrom(s => 
                s.ProductImages.Where(i => i.Deleted != true)))
            .ForMember(d => d.AverageRating, o => o.MapFrom(s => 
                s.Ratings.Any() 
                    ? (decimal)s.Ratings.Average(r => r.RatingScore) 
                    : 0m))
            .ForMember(d => d.ReviewCount, o => o.MapFrom(s => s.Ratings.Count))
            .ForMember(d => d.Ratings, o => o.MapFrom(s => 
                s.Ratings.Where(r => r.Deleted != true)))
            .ForMember(d => d.InStock, o => o.MapFrom(s => s.Stock > 0));
        
        // ProductImage → ProductImageDto
        CreateMap<ProductImage, ProductImageDto>()
            .ForMember(d => d.Url, o => o.MapFrom(s => s.ImageUrl ?? string.Empty))
            .ForMember(d => d.DisplayOrder, o => o.MapFrom(s => s.IsPrimary == true ? 0 : 1));
        
        // Rating → ProductRatingDto
        CreateMap<Rating, ProductRatingDto>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => 
                s.Rater != null ? s.Rater.Username : "Anonymous"))
            .ForMember(d => d.Rating, o => o.MapFrom(s => s.RatingScore));
        
        // CreateProductDto → Product
        CreateMap<CreateProductDto, Product>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.Deleted, o => o.MapFrom(s => false))
            .ForMember(d => d.Listings, o => o.Ignore())
            .ForMember(d => d.OrderDetails, o => o.Ignore())
            .ForMember(d => d.ProductImages, o => o.Ignore())
            .ForMember(d => d.Ratings, o => o.Ignore());
        
        // UpdateProductDto → Product
        CreateMap<UpdateProductDto, Product>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.Deleted, o => o.Ignore())
            .ForMember(d => d.Listings, o => o.Ignore())
            .ForMember(d => d.OrderDetails, o => o.Ignore())
            .ForMember(d => d.ProductImages, o => o.Ignore())
            .ForMember(d => d.Ratings, o => o.Ignore());
    }
}
