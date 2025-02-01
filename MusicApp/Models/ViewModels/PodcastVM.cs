using CommunityToolkit.Mvvm.Messaging;
using MusicApp.Helpers;
using MusicApp.ViewModels.ManyViewModels;
using MusicApp.ViewModels.SingleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicApp.Models.ViewModels
{
    public class PodcastVM
    {
        public ICommand OpenEpisodeListCommand { get; set; }
        public Podcast Podcast { get; set; }

        public PodcastVM(Podcast podcast)
        {
            Podcast = podcast;
            OpenEpisodeListCommand = new BaseCommand(() => OpenEpisodeList());

        }
        public void OpenEpisodeList()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new EpisodesViewModel(Podcast.PodcastId)));
        }
    }
}
