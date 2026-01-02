using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Listings")]
public partial class Listing
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SellerId { get; set; }

    public int? CategoryId { get; set; }

    [MaxLength(100)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal? StartingPrice { get; set; }

    public decimal? CurrentPrice { get; set; }

    public bool? IsAuction { get; set; }

    public DateTime? EndDate { get; set; }

    [MaxLength(50)]
    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public int? ProductId { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

    [ForeignKey("SellerId")]
    public virtual User? Seller { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
