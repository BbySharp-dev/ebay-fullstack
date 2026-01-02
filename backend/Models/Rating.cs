using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Ratings")]
public partial class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RaterId { get; set; }

    [Required]
    public int RatedUserId { get; set; }

    public int? ProductId { get; set; }

    [Required]
    public int RatingScore { get; set; }

    [MaxLength(500)]
    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

    [ForeignKey("RatedUserId")]
    public virtual User? RatedUser { get; set; }

    [ForeignKey("RaterId")]
    public virtual User? Rater { get; set; }
}
