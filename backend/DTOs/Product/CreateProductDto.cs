using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Product;

public class CreateProductDto
{
    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
    [StringLength(255, ErrorMessage = "Tên không được vượt quá 255 ký tự")]
    public string Name {get;set;} = string.Empty;

    [StringLength(2000, ErrorMessage = "Mô tả không vượt quá 2000 ký tự")]
    public string? Description {get;set;}

    [Required(ErrorMessage = "Giá là bắt buộc")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
    public decimal Price {get; set;}

    [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
    public int Stock {get; set;}

}