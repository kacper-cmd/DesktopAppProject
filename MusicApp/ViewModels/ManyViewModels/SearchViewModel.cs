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
    public class SearchViewModel : BaseManyViewModel<Song, SongViewModel>
    {
        public SearchViewModel() : base(GlobalResources.Song)
        { 
            //GetModels();
        }

        public override void AddNew()
        {
            Window window = new Window();
            window.Title = "Add new song";
            window.Height = 270;
            window.Content = new Views.SingleViews.SongView();
            window.Owner = App.Current.MainWindow;
            window.ShowDialog();
            Refresh();
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Song? model = Database.Songs.FirstOrDefault(item => item.IsActive && item.SongId == SelectedItem.SongId);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new SongViewModel(SelectedItem.SongId)));
            }
        }

        public override IQueryable<Song> GetModels()
        {
            return new DatabaseContext().Songs.Include(item => item.Album)
                                               .ThenInclude(item => item.Artist)
                                               .Where(item => item.IsActive);
        }

        protected override List<GenericComboBoxVM<string>> GetSearchColumns()
        {
            return new()
            {
                new("Id",nameof(Song.SongId)),
                new("Title",nameof(Song.SongName)),
                new("Artist",nameof(Song.Artist.ArtistName)),
                new("Album",nameof(Song.Album.AlbumName)),
            };
        }

        protected override IQueryable<Song> Search(IQueryable<Song> models)
        {
            switch (SearchColumn)
            {
                case nameof(Song.SongId):
                    return models.Where(item => item.SongId.ToString().Contains(SearchInput));
                case nameof(Song.SongName):
                    return models.Where(item => item.SongName.Contains(SearchInput));
                case nameof(Song.Artist.ArtistName):
                    return models.Where(item => item.Artist.ArtistName.Contains(SearchInput));
                case nameof(Song.Album.AlbumName):
                    return models.Where(item => item.Album.AlbumName.Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<Song> Sort(IQueryable<Song> models)
        {
            switch (SortColumn)
            {
                case nameof(Song.SongId):
                    return SortDescending ? models.OrderByDescending(item => item.SongId) : models.OrderBy(item => item.SongName);
                case nameof(Song.SongName):
                    return SortDescending ? models.OrderByDescending(item => item.SongName) : models.OrderBy(item => item.SongName);
                case nameof(Song.Artist.ArtistName):
                    return SortDescending ? models.OrderByDescending(item => item.Artist.ArtistName) : models.OrderBy(item => item.Artist.ArtistName);
                case nameof(Song.Album.AlbumName):
                    return SortDescending ? models.OrderByDescending(item => item.Album.AlbumName) : models.OrderBy(item => item.Album.AlbumName);
                default:
                    return models;
            }
        }
    }
}
