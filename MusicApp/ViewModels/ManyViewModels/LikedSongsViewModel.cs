using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MusicApp.Models;
using MusicApp.Models.Contexts;
using MusicApp.ViewModels.SingleViewModels;
using MusicApp.Views.ViewResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MusicApp.Helpers;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using MusicApp.BusinessLogic;

namespace MusicApp.ViewModels.ManyViewModels
{
    public class LikedSongsViewModel : BaseManyViewModel<LikedSong, LikedSongViewModel>
    {
        #region FieldsAndProperties
        private int _TotalLength;
        public int TotalLength
        {
            get => _TotalLength;
            set
            {
                if(_TotalLength != value) 
                {
                    _TotalLength = value;
                    OnPropertyChanged(() => TotalLength);
                }
            }
        }
        public ICommand CalculateLengthCommand { get; set; }
        #endregion

        #region Constructors
        public LikedSongsViewModel() : base(GlobalResources.Song)
        {
            CalculateLengthCommand = new BaseCommand(() => CalculateLength());
            WeakReferenceMessenger.Default.Register<SelectModelMessage<Song>>(this, (recipient, message) => ReceiveSong(message));
        }
        #endregion

        #region Methods
        public void CalculateLength()
        {
            LikedSongsLengthBL likedSongsLengthBL = new();
            TotalLength = likedSongsLengthBL.CalculateLikedSongsLength();
        }
        private void ReceiveSong(SelectModelMessage<Song> message)
        {
            if (this == message.Recipient)
            {
                SelectedItem = new LikedSong()
                {
                    LikedDate = DateTime.Now,
                    IsActive = true,
                    UserId = 1,
                    SongId = message.Message.SongId,
                };
                Database.LikedSongs.Add(SelectedItem);
                Database.SaveChanges();
                Refresh();
            }
        }

        public override void AddNew()
        {
            Window window = new Window();
            window.Title = "Add new song to liked";
            window.Content = new Views.SingleViews.SongsView();
            //window.Content = new Views.ManyViews.SearchView();
            SelectSongsViewModel selectSongsViewModel = new SelectSongsViewModel(this);
            window.DataContext = selectSongsViewModel;
            window.Owner = App.Current.MainWindow;
            selectSongsViewModel.RequestClose += delegate (object? sender, EventArgs eventArgs)
            {
                window.Close();
            };
            window.ShowDialog();
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                LikedSong? model = Database.LikedSongs.FirstOrDefault(item => item.IsActive && item.LikedSongId == SelectedItem.LikedSongId);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override IQueryable<LikedSong> GetModels()
        {
            return new DatabaseContext().LikedSongs.Include(item => item.Song)
                                                    .ThenInclude(item => item.Artist)
                                                    .ThenInclude(item => item.Albums)
                                                    .Where(item => item.IsActive);
        }

        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new LikedSongViewModel(SelectedItem.LikedSongId)));
            }
        }

        protected override List<GenericComboBoxVM<string>> GetSearchColumns()
        {
            return new()
            {
                new("Title",nameof(LikedSong.Song.SongName)),
                new("Artist",nameof(LikedSong.Song.Artist.ArtistName)),
                new("Album",nameof(LikedSong.Song.Album.AlbumName)),
            };
        }

        protected override IQueryable<LikedSong> Search(IQueryable<LikedSong> models)
        {
            switch (SearchColumn)
            {
                case nameof(LikedSong.Song.SongName):
                    return models.Where(item => item.Song.SongName.Contains(SearchInput));
                case nameof(LikedSong.Song.Artist.ArtistName):
                    return models.Where(item => item.Song.Artist.ArtistName.Contains(SearchInput));
                case nameof(LikedSong.Song.Album.AlbumName):
                    return models.Where(item => item.Song.Album.AlbumName.Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<LikedSong> Sort(IQueryable<LikedSong> models)
        {
            switch (SortColumn)
            {
                case nameof(LikedSong.Song.SongName):
                    return SortDescending ? models.OrderByDescending(item => item.Song.SongName) : models.OrderBy(item => item.Song.SongName);
                case nameof(LikedSong.Song.Artist.ArtistName):
                    return SortDescending ? models.OrderByDescending(item => item.Song.Artist.ArtistName) : models.OrderBy(item => item.Song.Artist.ArtistName);
                case nameof(LikedSong.Song.Album.AlbumName):
                    return SortDescending ? models.OrderByDescending(item => item.Song.Album.AlbumName) : models.OrderBy(item => item.Song.Album.AlbumName);
                case nameof(LikedSong.LikedDate):
                    return SortDescending ? models.OrderByDescending(item => item.LikedDate) : models.OrderBy(item => item.LikedDate);
                default:
                    return models;
            }
        }
    } 
    #endregion
}
