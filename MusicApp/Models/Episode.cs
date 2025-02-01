using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

[Table("Episode")]
public partial class Episode
{
    [Key]
    [Column("EpisodeID")]
    public int EpisodeId { get; set; }

    [StringLength(255)]
    public string EpisodeName { get; set; } = null!;

    public string? EpisodeDescription { get; set; }

    [Column("PodcastID")]
    public int PodcastId { get; set; }

    public int EpisodeDuration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReleaseDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [ForeignKey("PodcastId")]
    [InverseProperty("Episodes")]
    public virtual Podcast Podcast { get; set; } = null!;
}
