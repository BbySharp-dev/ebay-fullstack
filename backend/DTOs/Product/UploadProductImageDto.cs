using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Product;

public class UploadProductImageDto
{
    [Required(ErrorMessage = "File anh la bat buoc")]
    public IFormFile File { get; set; } = null!;
    
    public bool IsPrimary { get; set; } = false;
}