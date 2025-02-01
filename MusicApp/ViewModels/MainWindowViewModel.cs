using MusicApp.Views.ManyViews;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MusicApp.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MusicApp.ViewModels.ManyViewModels;
using CommunityToolkit.Mvvm.Messaging;
using MusicApp.Models;
using System.Windows;

namespace MusicApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private UIElement _currentView;
        public UIElement CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(() => CurrentView);
            }
        }
        

        public MainWindowViewModel()
        {
            CurrentView = new HomeView(new HomeViewModel());
            WeakReferenceMessenger.Default.Register<OpenViewMessage>(this, (recipient, message) => OpenView(message));
        }

        public void OpenView(OpenViewMessage openViewMessage)
        {
            if(openViewMessage.WorkspaceViewModel.GetType() == typeof(HomeViewModel)) { SetHomeView(openViewMessage.WorkspaceViewModel); }
            else if(openViewMessage.WorkspaceViewModel.GetType() == typeof(LikedSongsViewModel)) { SetLikedSongsView(openViewMessage.WorkspaceViewModel); }
            else if(openViewMessage.WorkspaceViewModel.GetType() == typeof(PodcastsViewModel)) { SetPodcastsView(openViewMessage.WorkspaceViewModel); }
            else if(openViewMessage.WorkspaceViewModel.GetType() == typeof(PlaylistsViewModel)) { SetPlaylistsView(openViewMessage.WorkspaceViewModel); }
            else if(openViewMessage.WorkspaceViewModel.GetType() == typeof(SearchViewModel)) { SetSearchView(openViewMessage.WorkspaceViewModel); }
            else if(openViewMessage.WorkspaceViewModel.GetType() == typeof(PlaylistSongsViewModel)) { SetPlaylistSongsView(openViewMessage.WorkspaceViewModel); }
            else if(openViewMessage.WorkspaceViewModel.GetType() == typeof(EpisodesViewModel)) { SetEpisodesView(openViewMessage.WorkspaceViewModel); }
        }

        public void SetHomeView(WorkspaceViewModel homeViewModel) => CurrentView = new HomeView(homeViewModel);
        public void SetLikedSongsView(WorkspaceViewModel likedSongsViewModel) => CurrentView = new LikedSongsView(likedSongsViewModel);
        public void SetPodcastsView(WorkspaceViewModel podcastsViewModel) => CurrentView = new PodcastsView(podcastsViewModel);
        public void SetPlaylistsView(WorkspaceViewModel playlistsViewModel) => CurrentView = new PlaylistsView(playlistsViewModel);
        public void SetSearchView(WorkspaceViewModel searchViewModel) => CurrentView = new SearchView(searchViewModel);
        public void SetPlaylistSongsView(WorkspaceViewModel playlistSongsViewModel) => CurrentView = new PlaylistSongsView(playlistSongsViewModel);
        public void SetEpisodesView(WorkspaceViewModel episodesViewModel) => CurrentView = new EpisodesView(episodesViewModel);
    }
}