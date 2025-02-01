using Microsoft.EntityFrameworkCore;
using MusicApp.Helpers;
using MusicApp.Models;
using MusicApp.Views.ViewResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusicApp.ViewModels.SingleViewModels
{
    public class LikedSongViewModel : BaseSingleViewModel<LikedSong>
    {
        #region FieldsAndProperties
        public string LikedSongName
        {
            get => Model.Song.SongName;
            set
            {
                if (LikedSongName != value)
                {
                    Model.Song.SongName = value;
                    OnPropertyChanged(() => LikedSongName);
                }
            }
        }
        public string LikedSongArtistName
        {
            get => Model.Song.Artist.ArtistName;
            set
            {
                if (LikedSongName != value)
                {
                    Model.Song.Artist.ArtistName = value;
                    OnPropertyChanged(() => LikedSongArtistName);
                }
            }
        }
        public int LikedSongDuration
        {
            get => Model.Song.SongDuration;
            set
            {
                if (LikedSongDuration != value)
                {
                    Model.Song.SongDuration = value;
                    OnPropertyChanged(() => LikedSongDuration);
                }
            }
        }
        #endregion

        #region Constructors
        public LikedSongViewModel() : base(GlobalResources.Song)
        { }
        public LikedSongViewModel(int id) : base(id, GlobalResources.Song)
        { }
        #endregion

        #region Methods
        protected override DbSet<LikedSong> GetDBTable()
        {
            return Database.LikedSongs;
        }

        protected override LikedSong InitializeModel()
        {
            return new LikedSong()
            {
                LikedDate = DateTime.Now,
                IsActive = true
            };
        }

        protected override LikedSong? GetModelFromDatabase(int id)
        {
            return GetDBTable().Include(item => item.Song.Artist)
                                .ThenInclude(item => item.Albums)
                                .Where(item => item.IsActive)
                                .First(item => item.LikedSongId == id);
        }

        protected override string? ValidateProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        protected override void Select()
        {
            //throw new NotImplementedException();
        } 
        #endregion
    }
}
