using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusicApp.Models;

namespace MusicApp.Models.Contexts;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<BannedUser> BannedUsers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<LikedSong> LikedSongs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Lyric> Lyrics { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistFollower> PlaylistFollowers { get; set; }

    public virtual DbSet<PlaylistSong> PlaylistSongs { get; set; }

    public virtual DbSet<Podcast> Podcasts { get; set; }

    public virtual DbSet<PodcastFollower> PodcastFollowers { get; set; }

    public virtual DbSet<Preference> Preferences { get; set; }

    public virtual DbSet<Queue> Queues { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=MusicApp;TrustServerCertificate=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__97B4BE176888E5EB");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Albums__ArtistID__3D5E1FD2");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B70A1972802");
        });

        modelBuilder.Entity<BannedUser>(entity =>
        {
            entity.HasKey(e => e.BannedUserId).HasName("PK__BannedUs__DDB1AB3219688567");

            entity.HasOne(d => d.User).WithMany(p => p.BannedUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BannedUse__UserI__74AE54BC");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Devices__49E12331D2033AD8");

            entity.HasOne(d => d.User).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__UserID__6EF57B66");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.EpisodeId).HasName("PK__Episode__AC667615D1379F4D");

            entity.HasOne(d => d.Podcast).WithMany(p => p.Episodes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Episode__Podcast__66603565");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055E3F40A7F0");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ADD86D223EC");

            entity.HasOne(d => d.Song).WithMany(p => p.Histories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__History__SongID__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__History__UserID__4AB81AF0");
        });

        modelBuilder.Entity<LikedSong>(entity =>
        {
            entity.HasKey(e => e.LikedSongId).HasName("PK__LikedSon__D7EAE9BD510E147D");

            entity.HasOne(d => d.Song).WithMany(p => p.LikedSongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LikedSong__SongI__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.LikedSongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LikedSong__UserI__4F7CD00D");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA4778B1052C6");

            entity.HasOne(d => d.User).WithMany(p => p.Locations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Locations__UserI__693CA210");
        });

        modelBuilder.Entity<Lyric>(entity =>
        {
            entity.HasKey(e => e.LyricsId).HasName("PK__Lyrics__F71D1C06EAB8F6FA");

            entity.HasOne(d => d.Song).WithMany(p => p.Lyrics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lyrics__SongID__52593CB8");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E3285D563E7");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__71D1E811");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__B3016780D996BFEE");

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Playlists__UserI__5535A963");
        });

        modelBuilder.Entity<PlaylistFollower>(entity =>
        {
            entity.HasKey(e => e.PlaylistFollowerId).HasName("PK__Playlist__1899B937F27D0B01");

            entity.HasOne(d => d.FollowerUser).WithMany(p => p.PlaylistFollowers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistF__Follo__5CD6CB2B");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistFollowers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistF__Playl__5BE2A6F2");
        });

        modelBuilder.Entity<PlaylistSong>(entity =>
        {
            entity.HasKey(e => e.PlaylistSongId).HasName("PK__Playlist__D58F7B0EBE4EB67C");

            entity.ToTable(tb => tb.HasTrigger("tr_UpdatePlaylistDuration"));

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistSongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistS__Playl__19DFD96B");

            entity.HasOne(d => d.Song).WithMany(p => p.PlaylistSongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistS__SongI__1AD3FDA4");
        });

        modelBuilder.Entity<Podcast>(entity =>
        {
            entity.HasKey(e => e.PodcastId).HasName("PK__Podcasts__EB66937481D3301F");

            entity.HasOne(d => d.User).WithMany(p => p.Podcasts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Podcasts__UserID__5FB337D6");
        });

        modelBuilder.Entity<PodcastFollower>(entity =>
        {
            entity.HasKey(e => e.PodcastFollowerId).HasName("PK__PodcastF__7D6FCF8E8E8AE950");

            entity.HasOne(d => d.FollowerUser).WithMany(p => p.PodcastFollowers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PodcastFo__Follo__6383C8BA");

            entity.HasOne(d => d.Podcast).WithMany(p => p.PodcastFollowers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PodcastFo__Podca__628FA481");
        });

        modelBuilder.Entity<Preference>(entity =>
        {
            entity.HasKey(e => e.PreferencesId).HasName("PK__Preferen__D57657CE3B43EE4F");

            entity.HasOne(d => d.User).WithMany(p => p.Preferences)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preferenc__UserI__6C190EBB");
        });

        modelBuilder.Entity<Queue>(entity =>
        {
            entity.HasKey(e => e.QueueId).HasName("PK__Queue__8324E8F56ECE6C1D");

            entity.HasOne(d => d.Song).WithMany(p => p.Queues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Queue__SongID__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.Queues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Queue__UserID__46E78A0C");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__Songs__12E3D6F7FC6E85A6");

            entity.ToTable(tb => tb.HasTrigger("tr_UpdateLikedSongsIsActive"));

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Songs__AlbumID__4316F928");

            entity.HasOne(d => d.Artist).WithMany(p => p.Songs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Songs__ArtistID__4222D4EF");

            entity.HasOne(d => d.Genre).WithMany(p => p.Songs).HasConstraintName("FK__Songs__GenreID__440B1D61");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACFD623FD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
