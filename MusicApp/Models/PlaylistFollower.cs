using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class PlaylistFollower
{
    [Key]
    [Column("PlaylistFollowerID")]
    public int PlaylistFollowerId { get; set; }

    [Column("PlaylistID")]
    public int PlaylistId { get; set; }

    [Column("FollowerUserID")]
    public int FollowerUserId { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("FollowerUserId")]
    [InverseProperty("PlaylistFollowers")]
    public virtual User FollowerUser { get; set; } = null!;

    [ForeignKey("PlaylistId")]
    [InverseProperty("PlaylistFollowers")]
    public virtual Playlist Playlist { get; set; } = null!;
}
