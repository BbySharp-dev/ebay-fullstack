using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

[Table("ConnectionCountLog")]
public partial class ConnectionCountLog
{
    [Key]
    public int Id { get; set; }

    [MaxLength(45)]
    [Required]
    public string IpAddress { get; set; } = string.Empty;

    public DateTime? ConnectionTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? ConnectionCount { get; set; }
}
