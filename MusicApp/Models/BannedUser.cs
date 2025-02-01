using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class BannedUser
{
    [Key]
    [Column("BannedUserID")]
    public int BannedUserId { get; set; }

    [StringLength(255)]
    public string? BanReason { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BanDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UnbanDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("BannedUsers")]
    public virtual User User { get; set; } = null!;
}
