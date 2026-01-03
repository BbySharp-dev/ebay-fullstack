namespace Backend.DTOs.Product;

public class ProductImageDto
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty; // S3 URL
    public int DisplayOrder { get; set; }
}