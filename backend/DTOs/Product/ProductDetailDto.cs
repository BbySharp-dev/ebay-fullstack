using Backend.Models;

namespace Backend.DTOs.Product;

public class ProductDetailDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<ProductImageDto> Images { get; set; } = new();
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public List<ProductRatingDto> Ratings { get; set; } = new();
    public bool InStock { get; set; }
}

