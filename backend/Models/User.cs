using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Users")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string Username { get; set; } = string.Empty;

    [MaxLength(100)]
    [Required]
    public string Email { get; set; } = string.Empty;

    [MaxLength(255)]
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? FullName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    [MaxLength(255)]
    public string? Phone { get; set; }

    public string? Ava { get; set; }

    // Navigation Properties
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Rating> RatingRatedUsers { get; set; } = new List<Rating>();

    public virtual ICollection<Rating> RatingRaters { get; set; } = new List<Rating>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
