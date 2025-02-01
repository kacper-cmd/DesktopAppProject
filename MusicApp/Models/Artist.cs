using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

[Index("ArtistName", Name = "AK_Artists_ArtistName", IsUnique = true)]
public partial class Artist
{
    [Key]
    [Column("ArtistID")]
    public int ArtistId { get; set; }

    [StringLength(100)]
    public string ArtistName { get; set; } = null!;

    public string? ArtistDescription { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [InverseProperty("Artist")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    [InverseProperty("Artist")]
    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
