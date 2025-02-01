using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class PlaylistSong
{
    [Key]
    [Column("PlaylistSongID")]
    public int PlaylistSongId { get; set; }

    public int SongPosition { get; set; }

    [Column("PlaylistID")]
    public int PlaylistId { get; set; }

    [Column("SongID")]
    public int SongId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AddedTime { get; set; }

    [ForeignKey("PlaylistId")]
    [InverseProperty("PlaylistSongs")]
    public virtual Playlist Playlist { get; set; } = null!;

    [ForeignKey("SongId")]
    [InverseProperty("PlaylistSongs")]
    public virtual Song Song { get; set; } = null!;
}
