using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using MusicApp.Models;
using MusicApp.Views.ViewResources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels.SingleViewModels
{
    public class PlaylistSongViewModel : BaseSingleViewModel<PlaylistSong>
    {
        #region FieldsAndProperties
        public List<ComboBoxVM> Songs { get; set; }
        public List<ComboBoxVM> Playlists { get; set; }
        public int PlaylistID
        {
            get => Model.PlaylistId;
            set
            {
                if (PlaylistID != value)
                {
                    Model.PlaylistId = value;
                    OnPropertyChanged(() => PlaylistID);
                }
            }
        }
        public int SongID
        {
            get => Model.SongId;
            set
            {
                if (SongID != value)
                {
                    Model.SongId = value;
                    OnPropertyChanged(() => SongID);
                }
            }
        }
        public string PlaylistSongName
        {
            get => Model.Song.SongName;
            set
            {
                if (PlaylistSongName != value)
                {
                    Model.Song.SongName = value;
                    OnPropertyChanged(() => PlaylistSongName);
                }
            }
        }
        public string PlaylistSongPlaylistName
        {
            get => Model.Playlist.PlaylistName;
            set
            {
                if (PlaylistSongPlaylistName != value)
                {
                    Model.Playlist.PlaylistName = value;
                    OnPropertyChanged(() => PlaylistSongPlaylistName);
                }
            }
        }
        public string PlaylistSongArtistName
        {
            get => Model.Song.Artist.ArtistName;
            set
            {
                if (PlaylistSongArtistName != value)
                {
                    Model.Song.Artist.ArtistName = value;
                    OnPropertyChanged(() => PlaylistSongArtistName);
                }
            }
        }
        public int PlaylistSongDuration
        {
            get => Model.Song.SongDuration;
            set
            {
                if (PlaylistSongDuration != value)
                {
                    Model.Song.SongDuration = value;
                    OnPropertyChanged(() => PlaylistSongDuration);
                }
            }
        }
        #endregion
        #region Constructors
        public PlaylistSongViewModel() : base(GlobalResources.Song)
        {
            Initialize();
        }

        public PlaylistSongViewModel(int id) : base(id, GlobalResources.Song)
        {
            Initialize();
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            Songs = Database.Songs.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.SongId,
                Title = item.SongName
            }).ToList();

            Playlists = Database.Playlists.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.PlaylistId,
                Title = item.PlaylistName
            }).ToList();

            WeakReferenceMessenger.Default.Register<string>(this, (recipent, message) =>
            {
                Debug.WriteLine(message);
                Debug.WriteLine(recipent);
            });
        }
        protected override DbSet<PlaylistSong> GetDBTable()
        {
            return Database.PlaylistSongs;
        }

        protected override PlaylistSong? GetModelFromDatabase(int id)
        {
            return GetDBTable().Include(item => item.Playlist)
                                .Include(item => item.Song)
                                .ThenInclude(item => item.Artist)
                                .ThenInclude(item => item.Albums)
                                .Where(item => item.IsActive)
                                .First(item => item.PlaylistSongId == id);
        }

        protected override PlaylistSong InitializeModel()
        {
            return new PlaylistSong()
            {
                IsActive = true,
                AddedTime = DateTime.Now,
            };
        }

        protected override void Select()
        {
            //throw new NotImplementedException();
        }

        protected override string? ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(PlaylistID):
                    if (PlaylistID == 0)
                    {
                        return "Playlist is not set.";
                    }
                    break;
            case nameof(SongID):
                if (SongID == 0)
                {
                    return "Song is not set.";
                }
                break;
            }
            return null;
        } 
        #endregion
    }
}
