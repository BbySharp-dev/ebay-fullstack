using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Categories")]
public partial class Category
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
