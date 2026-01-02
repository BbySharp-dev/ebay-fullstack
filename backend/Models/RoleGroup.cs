using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("RoleGroups")]
public partial class RoleGroup
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public int GroupId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [ForeignKey("GroupId")]
    public virtual Group? Group { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role? Role { get; set; }
}
