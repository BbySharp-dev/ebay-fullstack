namespace Backend.DTOs.Product;

public class ProductListDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ThumbnailUrl { get; set; }
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool InStock { get; set; }
}


