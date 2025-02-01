using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

[Table("Queue")]
public partial class Queue
{
    [Key]
    [Column("QueueID")]
    public int QueueId { get; set; }

    public int SongPosition { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("SongID")]
    public int SongId { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("SongId")]
    [InverseProperty("Queues")]
    public virtual Song Song { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Queues")]
    public virtual User User { get; set; } = null!;
}
