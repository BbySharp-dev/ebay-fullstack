using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("UserRole")]
public partial class UserRole
{
    [Key]
    [Column(Order = 0)]
    [Required]
    public int UserId { get; set; }

    [Key]
    [Column(Order = 1)]
    [Required]
    public int RoleId { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role? Role { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
