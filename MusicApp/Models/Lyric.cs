using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Lyric
{
    [Key]
    [Column("LyricsID")]
    public int LyricsId { get; set; }

    [Column("SongID")]
    public int SongId { get; set; }

    public string? LyricsText { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [ForeignKey("SongId")]
    [InverseProperty("Lyrics")]
    public virtual Song Song { get; set; } = null!;
}
