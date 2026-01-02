using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("UserGroups")]
public partial class UserGroup
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int GroupId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    [ForeignKey("GroupId")]
    public virtual Group? Group { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
