using CommunityToolkit.Mvvm.Messaging;
using MusicApp.Helpers;
using MusicApp.ViewModels.ManyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicApp.Models.ViewModels
{
    public class PlaylistVM
    {
        public ICommand OpenPlaylistSongListCommand { get; set; }
        public Playlist Playlist { get; set; }

        public PlaylistVM(Playlist playlist)
        {
            Playlist = playlist;
            OpenPlaylistSongListCommand = new BaseCommand(() => OpenPlaylistSongList());

        }
        public void OpenPlaylistSongList()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PlaylistSongsViewModel(Playlist.PlaylistId)));
        }
    }
}
