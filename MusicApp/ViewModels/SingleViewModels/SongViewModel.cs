using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicApp.Helpers;
using MusicApp.Models;
using MusicApp.Views.ViewResources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicApp.ViewModels.SingleViewModels
{
    public class SongViewModel  : BaseSingleViewModel<Song>
    {
        #region FieldsAndProperties
        public List<ComboBoxVM> Artists { get; set; }
        public List<ComboBoxVM> Albums { get; set; }
        public List<ComboBoxVM> Genres { get; set; }
        public string SongName 
        { 
            get => Model.SongName; 
            set
            {
                if (SongName != value) 
                {
                    Model.SongName = value;
                    OnPropertyChanged(() => SongName);
                }
            }
        }
        public DateTime? ReleaseDate 
        { 
            get => Model.ReleaseDate; 
            set
            {
                if (ReleaseDate != value) 
                {
                    Model.ReleaseDate = value;
                    OnPropertyChanged(() => ReleaseDate);
                }
            }
        }
        public int SongDuration 
        { 
            get => Model.SongDuration; 
            set
            {
                if (SongDuration != value) 
                {
                    Model.SongDuration = value;
                    OnPropertyChanged(() => SongDuration);
                }
            }
        }
        public int ArtistID 
        { 
            get => Model.ArtistId; 
            set
            {
                if (ArtistID != value) 
                {
                    Model.ArtistId = value;
                    OnPropertyChanged(() => ArtistID);
                }
            }
        }
        public int AlbumID 
        { 
            get => Model.AlbumId; 
            set
            {
                if (AlbumID != value) 
                {
                    Model.AlbumId = value;
                    OnPropertyChanged(() => AlbumID);
                }
            }
        }
        public int? GenreID 
        { 
            get => Model.GenreId; 
            set
            {
                if (GenreID != value) 
                {
                    Model.GenreId = value;
                    OnPropertyChanged(() => GenreID);
                }
            }
        }
        #endregion

        #region Constructors
        public SongViewModel() : base(GlobalResources.Song)
        {
            Initialize();
        }
        public SongViewModel(int id) : base(id, GlobalResources.Song)
        {
            Initialize();
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            Artists = Database.Artists.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.ArtistId,
                Title = item.ArtistName
            }
            ).ToList();

            Albums = Database.Albums.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.AlbumId,
                Title = item.AlbumName
            }
            ).ToList();

            Genres = Database.Genres.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.GenreId,
                Title = item.GenreName
            }
            ).ToList();

            WeakReferenceMessenger.Default.Register<string>(this, (recipent, message) =>
            {
                Debug.WriteLine(message);
                Debug.WriteLine(recipent);
            });
        }
        protected override DbSet<Song> GetDBTable()
        {
            return Database.Songs;
        }

        protected override Song InitializeModel()
        {
            return new Song()
            {
                IsActive = true,
                CreatedDate = DateTime.Now
            };
        }

        protected override Song? GetModelFromDatabase(int id)
        {
            return GetDBTable().Include(item => item.Artist)
                                .ThenInclude(item => item.Albums)
                                .Where(item => item.IsActive)
                                .First(item => item.SongId == id);
        }

        protected override string? ValidateProperty(string propertyName)
        {
            switch(propertyName)
            {
                case nameof(ArtistID):
                    if (ArtistID == 0)
                    {
                        return "Artist is not set.";
                    }
                    break;

                case nameof(AlbumID):
                    if (AlbumID == 0)
                    {
                        return "Album is not set.";
                    }
                    break;

                case nameof(SongDuration):
                    if(SongDuration == 0 || SongDuration <= 0)
                    {
                        return "Duration cannot be 0 or lower.";
                    }
                    break;

                case nameof(SongName):
                    if(SongName.IsNullOrEmpty())
                    {
                        return "Song title cannot be empty.";
                    }
                    break;
            }
            return null;
        }

        protected override void Select()
        {
            if (SelectedItem != null)
            {
                AddSongToLiked(SelectedItem);
            }
        }

        public void AddSongToLiked(Song selectedSong)
        {
            LikedSong newLikedSong = new LikedSong
            {
                LikedDate = DateTime.Now,
                IsActive = true,
                Song = selectedSong
            };

            Database.LikedSongs.Add(newLikedSong);
            Database.SaveChanges();
        }
        #endregion
    }
}
