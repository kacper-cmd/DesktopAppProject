using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class PodcastFollower
{
    [Key]
    [Column("PodcastFollowerID")]
    public int PodcastFollowerId { get; set; }

    [Column("PodcastID")]
    public int PodcastId { get; set; }

    [Column("FollowerUserID")]
    public int FollowerUserId { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("FollowerUserId")]
    [InverseProperty("PodcastFollowers")]
    public virtual User FollowerUser { get; set; } = null!;

    [ForeignKey("PodcastId")]
    [InverseProperty("PodcastFollowers")]
    public virtual Podcast Podcast { get; set; } = null!;
}
