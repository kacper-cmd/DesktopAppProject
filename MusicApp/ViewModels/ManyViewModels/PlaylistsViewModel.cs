using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using MusicApp.Helpers;
using MusicApp.Models;
using MusicApp.Models.Contexts;
using MusicApp.Models.ViewModels;
using MusicApp.ViewModels.SingleViewModels;
using MusicApp.Views.ViewResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicApp.ViewModels.ManyViewModels
{
    public class PlaylistsViewModel : BaseManyViewModel<PlaylistVM,PlaylistViewModel>
    {
        #region FieldsAndProperties
        public ICommand OpenPlaylistSongsViewCommand { get; set; }
        #endregion

        #region Constructors
        public PlaylistsViewModel() : base(GlobalResources.Playlist)
        {
            OpenPlaylistSongsViewCommand = new BaseCommand(() => OpenPlaylistSongsView());
        }

        #endregion

        #region Methods
        private void OpenPlaylistSongsView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PlaylistSongsViewModel()));
        public override void AddNew()
        {
            Window window = new Window();
            window.Title = "Add new playlist";
            window.Height = 270;
            window.Width = 400;
            window.Content = new Views.SingleViews.PlaylistView();
            window.Owner = App.Current.MainWindow;
            window.ShowDialog();
            Refresh();
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Playlist? model = Database.Playlists.FirstOrDefault(item => item.IsActive && item.PlaylistId == SelectedItem.Playlist.PlaylistId);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override IQueryable<PlaylistVM> GetModels()
        {
            return new DatabaseContext().Playlists.Include(item => item.User)
                                              .Where(item => item.IsActive)
                                              .Select(item => new PlaylistVM(item));
        }

        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PlaylistViewModel(SelectedItem.Playlist.PlaylistId)));
            }
        }

        protected override List<GenericComboBoxVM<string>> GetSearchColumns()
        {
            return new()
            {
                new("Name",nameof(Playlist.PlaylistName)),
                new("Author",nameof(Playlist.User.UserName)),
                new("Lenght",nameof(Playlist.PlaylistDuration)),
            };
        }

        protected override IQueryable<PlaylistVM> Search(IQueryable<PlaylistVM> models)
        {
            switch (SearchColumn)
            {
                case nameof(PlaylistVM.Playlist.PlaylistName):
                    return models.Where(item => item.Playlist.PlaylistName.Contains(SearchInput));
                case nameof(PlaylistVM.Playlist.User.UserName):
                    return models.Where(item => item.Playlist.User.UserName.Contains(SearchInput));
                case nameof(PlaylistVM.Playlist.PlaylistDuration):
                    return models.Where(item => item.Playlist.PlaylistDuration.ToString().Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<PlaylistVM> Sort(IQueryable<PlaylistVM> models)
        {
            switch (SortColumn)
            {
                case nameof(PlaylistVM.Playlist.PlaylistName):
                    return SortDescending ? models.OrderByDescending(item => item.Playlist.PlaylistName) : models.OrderBy(item => item.Playlist.PlaylistName);
                case nameof(PlaylistVM.Playlist.User.UserName):
                    return SortDescending ? models.OrderByDescending(item => item.Playlist.User.UserName) : models.OrderBy(item => item.Playlist.User.UserName);
                case nameof(PlaylistVM.Playlist.PlaylistDuration):
                    return SortDescending ? models.OrderByDescending(item => item.Playlist.PlaylistDuration) : models.OrderBy(item => item.Playlist.PlaylistDuration);
                default:
                    return models;
            }
        } 
        #endregion
    }
}
