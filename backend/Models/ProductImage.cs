using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("ProductImages")]
public partial class ProductImage
{
    [Key]
    public int Id { get; set; }

    public int? ProductId { get; set; }

    [MaxLength(255)]
    public string? ImageUrl { get; set; }

    public bool? IsPrimary { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }
}
