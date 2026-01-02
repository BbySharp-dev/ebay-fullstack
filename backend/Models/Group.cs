using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("Groups")]
public partial class Group
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string GroupName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<RoleGroup> RoleGroups { get; set; } = new List<RoleGroup>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
