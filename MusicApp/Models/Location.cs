using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

public partial class Location
{
    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Locations")]
    public virtual User User { get; set; } = null!;
}
