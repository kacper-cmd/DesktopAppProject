using CommunityToolkit.Mvvm.Messaging;
using MusicApp.Helpers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicApp.ViewModels.ManyViewModels
{
    public class SideMenuViewModel
    {
        public ICommand OpenHomeViewCommand { get; set; }
        public ICommand OpenLikedSongsViewCommand { get; set; }
        public ICommand OpenPodcastsViewCommand { get; set; }
        public ICommand OpenPlaylistsViewCommand { get; set; }
        public ICommand OpenSearchViewCommand { get; set; }

        public SideMenuViewModel()
        {
            OpenHomeViewCommand = new BaseCommand(() => OpenHomeView());
            OpenLikedSongsViewCommand = new BaseCommand(() => OpenLikedSongsView());
            OpenPodcastsViewCommand = new BaseCommand(() => OpenPodcastsView());
            OpenPlaylistsViewCommand = new BaseCommand(() => OpenPlaylistsView());
            OpenSearchViewCommand = new BaseCommand(() => OpenSearchView());
        }

        private void OpenHomeView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new HomeViewModel()));
        private void OpenLikedSongsView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new LikedSongsViewModel()));
        private void OpenPodcastsView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PodcastsViewModel()));
        private void OpenPlaylistsView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PlaylistsViewModel()));
        private void OpenSearchView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new SearchViewModel()));
    }
}
