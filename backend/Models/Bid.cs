using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Bids")]
public partial class Bid
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ListingId { get; set; }

    [Required]
    public int BidderId { get; set; }

    [Required]
    public decimal BidAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [ForeignKey("BidderId")]
    public virtual User? Bidder { get; set; }

    [ForeignKey("ListingId")]
    public virtual Listing? Listing { get; set; }
}
