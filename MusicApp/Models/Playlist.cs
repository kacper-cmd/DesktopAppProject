using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Playlist
{
    [Key]
    [Column("PlaylistID")]
    public int PlaylistId { get; set; }

    [StringLength(255)]
    public string PlaylistName { get; set; } = null!;

    public int PlaylistDuration { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [InverseProperty("Playlist")]
    public virtual ICollection<PlaylistFollower> PlaylistFollowers { get; set; } = new List<PlaylistFollower>();

    [InverseProperty("Playlist")]
    public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();

    [ForeignKey("UserId")]
    [InverseProperty("Playlists")]
    public virtual User User { get; set; } = null!;
}
