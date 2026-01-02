using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Orders")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BuyerId { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [MaxLength(50)]
    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [ForeignKey("BuyerId")]
    public virtual User? Buyer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
