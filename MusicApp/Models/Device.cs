using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Device
{
    [Key]
    [Column("DeviceID")]
    public int DeviceId { get; set; }

    [StringLength(255)]
    public string DeviceName { get; set; } = null!;

    [StringLength(255)]
    public string? DeviceType { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUsed { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Devices")]
    public virtual User User { get; set; } = null!;
}
