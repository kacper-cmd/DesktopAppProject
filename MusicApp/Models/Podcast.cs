using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Podcast
{
    [Key]
    [Column("PodcastID")]
    public int PodcastId { get; set; }

    [StringLength(255)]
    public string PodcastName { get; set; } = null!;

    public string? PodcastDescription { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [InverseProperty("Podcast")]
    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

    [InverseProperty("Podcast")]
    public virtual ICollection<PodcastFollower> PodcastFollowers { get; set; } = new List<PodcastFollower>();

    [ForeignKey("UserId")]
    [InverseProperty("Podcasts")]
    public virtual User User { get; set; } = null!;
}
