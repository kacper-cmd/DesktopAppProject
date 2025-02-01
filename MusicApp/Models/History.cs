using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

[Table("History")]
public partial class History
{
    [Key]
    [Column("HistoryID")]
    public int HistoryId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("SongID")]
    public int SongId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TimeListened { get; set; }

    [ForeignKey("SongId")]
    [InverseProperty("Histories")]
    public virtual Song Song { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Histories")]
    public virtual User User { get; set; } = null!;
}
