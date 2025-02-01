using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class LikedSong
{
    [Key]
    [Column("LikedSongID")]
    public int LikedSongId { get; set; }

    [Column("SongID")]
    public int SongId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LikedDate { get; set; }

    [ForeignKey("SongId")]
    [InverseProperty("LikedSongs")]
    public virtual Song Song { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("LikedSongs")]
    public virtual User User { get; set; } = null!;
}
