using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Preference
{
    [Key]
    [Column("PreferencesID")]
    public int PreferencesId { get; set; }

    [StringLength(255)]
    public string? ThemePreference { get; set; }

    public string? NotificationPreferences { get; set; }

    public string? PlaybackPreferences { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Preferences")]
    public virtual User User { get; set; } = null!;
}
