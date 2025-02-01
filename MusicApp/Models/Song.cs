using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Song
{
    [Key]
    [Column("SongID")]
    public int SongId { get; set; }

    [StringLength(100)]
    public string SongName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? ReleaseDate { get; set; }

    public int SongDuration { get; set; }

    [Column("ArtistID")]
    public int ArtistId { get; set; }

    [Column("AlbumID")]
    public int AlbumId { get; set; }

    [Column("GenreID")]
    public int? GenreId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [ForeignKey("AlbumId")]
    [InverseProperty("Songs")]
    public virtual Album Album { get; set; } = null!;

    [ForeignKey("ArtistId")]
    [InverseProperty("Songs")]
    public virtual Artist Artist { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Songs")]
    public virtual Genre? Genre { get; set; }

    [InverseProperty("Song")]
    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    [InverseProperty("Song")]
    public virtual ICollection<LikedSong> LikedSongs { get; set; } = new List<LikedSong>();

    [InverseProperty("Song")]
    public virtual ICollection<Lyric> Lyrics { get; set; } = new List<Lyric>();

    [InverseProperty("Song")]
    public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();

    [InverseProperty("Song")]
    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();
}
