using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

[Index("Email", Name = "AK_Users_Email", IsUnique = true)]
[Index("UserName", Name = "AK_Users_UserName", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    public string UserName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string UserPassword { get; set; } = null!;

    public string? UserBio { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<BannedUser> BannedUsers { get; set; } = new List<BannedUser>();

    [InverseProperty("User")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    [InverseProperty("User")]
    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    [InverseProperty("User")]
    public virtual ICollection<LikedSong> LikedSongs { get; set; } = new List<LikedSong>();

    [InverseProperty("User")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("FollowerUser")]
    public virtual ICollection<PlaylistFollower> PlaylistFollowers { get; set; } = new List<PlaylistFollower>();

    [InverseProperty("User")]
    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    [InverseProperty("FollowerUser")]
    public virtual ICollection<PodcastFollower> PodcastFollowers { get; set; } = new List<PodcastFollower>();

    [InverseProperty("User")]
    public virtual ICollection<Podcast> Podcasts { get; set; } = new List<Podcast>();

    [InverseProperty("User")]
    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    [InverseProperty("User")]
    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();
}
