using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using MusicApp.Helpers;
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

namespace MusicApp.ViewModels.ManyViewModels
{
    public class PlaylistSongsViewModel : BaseManyViewModel<PlaylistSong, PlaylistSongsViewModel>
    {
        public int PlaylistId { get; set; }

        #region Constructors
        public PlaylistSongsViewModel(int playlistId) : base(GlobalResources.Playlist)
        {
            PlaylistId = playlistId;
            Refresh();
        }
        public PlaylistSongsViewModel() : base(GlobalResources.Playlist)
        {
        } 
        #endregion
        #region Methods

        public override void AddNew()
        {
            Window window = new Window();
            window.Title = "Add song to playlist";
            window.Height = 250;
            window.Width = 400;
            window.Content = new Views.SingleViews.PlaylistSongView();
            window.Owner = App.Current.MainWindow;
            window.ShowDialog();
            Refresh();
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                PlaylistSong? model = Database.PlaylistSongs.FirstOrDefault(item => item.IsActive && item.PlaylistSongId == SelectedItem.PlaylistSongId);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override IQueryable<PlaylistSong> GetModels()
        {
            return new DatabaseContext().PlaylistSongs.Include(item => item.Playlist)
                                                    //.ThenInclude(item => item.PlaylistSong)
                                                    .Include(item => item.Song)
                                                    .ThenInclude(item => item.Artist)
                                                    .ThenInclude(item => item.Albums)
                                                    .Where(item => item.IsActive && item.PlaylistId == PlaylistId);
        }

        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PlaylistSongViewModel(SelectedItem.PlaylistSongId)));
            }
        }

        protected override List<GenericComboBoxVM<string>> GetSearchColumns()
        {
            return new()
            {
                new("Playlist",nameof(PlaylistSong.Playlist.PlaylistName)),
                new("Song Id",nameof(PlaylistSong.SongId)),
                new("Title",nameof(PlaylistSong.Song.SongName)),
                new("Artist",nameof(PlaylistSong.Song.Artist.ArtistName)),
                new("Album",nameof(PlaylistSong.Song.Album.AlbumName)),
            };
        }

        protected override IQueryable<PlaylistSong> Search(IQueryable<PlaylistSong> models)
        {
            switch (SearchColumn)
            {
                case nameof(PlaylistSong.Playlist.PlaylistName):
                    return models.Where(item => item.Playlist.PlaylistName.Contains(SearchInput));
                case nameof(PlaylistSong.SongId):
                    return models.Where(item => item.SongId.ToString().Contains(SearchInput));
                case nameof(PlaylistSong.Song.SongName):
                    return models.Where(item => item.Song.SongName.Contains(SearchInput));
                case nameof(PlaylistSong.Song.Artist.ArtistName):
                    return models.Where(item => item.Song.Artist.ArtistName.Contains(SearchInput));
                case nameof(PlaylistSong.Song.Album.AlbumName):
                    return models.Where(item => item.Song.Album.AlbumName.Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<PlaylistSong> Sort(IQueryable<PlaylistSong> models)
        {
            switch (SortColumn)
            {
                case nameof(PlaylistSong.Playlist.PlaylistName):
                    return SortDescending ? models.OrderByDescending(item => item.Playlist.PlaylistName) : models.OrderBy(item => item.Playlist.PlaylistName);
                case nameof(PlaylistSong.SongId):
                    return SortDescending ? models.OrderByDescending(item => item.SongId) : models.OrderBy(item => item.SongId);
                case nameof(PlaylistSong.Song.SongName):
                    return SortDescending ? models.OrderByDescending(item => item.Song.SongName) : models.OrderBy(item => item.Song.SongName);
                case nameof(PlaylistSong.Song.Artist.ArtistName):
                    return SortDescending ? models.OrderByDescending(item => item.Song.Artist.ArtistName) : models.OrderBy(item => item.Song.Artist.ArtistName);
                case nameof(PlaylistSong.Song.Album.AlbumName):
                    return SortDescending ? models.OrderByDescending(item => item.Song.Album.AlbumName) : models.OrderBy(item => item.Song.Album.AlbumName);
                case nameof(PlaylistSong.AddedTime):
                    return SortDescending ? models.OrderByDescending(item => item.AddedTime) : models.OrderBy(item => item.AddedTime);
                default:
                    return models;
            }
        }
    } 
    #endregion
}
