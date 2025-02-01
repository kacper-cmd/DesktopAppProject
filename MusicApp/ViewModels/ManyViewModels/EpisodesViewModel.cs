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
    public class EpisodesViewModel : BaseManyViewModel<Episode, EpisodeViewModel>
    {
        public int PodcastId { get; set; }
        public EpisodesViewModel(int podcastId) : base(GlobalResources.Episode)
        {
            PodcastId = podcastId;
            Refresh();
        }
        public EpisodesViewModel() : base(GlobalResources.Episode)
        {
            
        }

        public override void AddNew()
        {
            Window window = new Window();
            window.Title = "Add new episode";
            window.Height = 450;
            window.Width = 700;
            window.Content = new Views.SingleViews.EpisodeView();
            window.Owner = App.Current.MainWindow;
            window.ShowDialog();
            Refresh();
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Episode? model = Database.Episodes.FirstOrDefault(item => item.IsActive && item.EpisodeId == SelectedItem.EpisodeId);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override IQueryable<Episode> GetModels()
        {
            return new DatabaseContext().Episodes.Include(item => item.Podcast)
                                                .Where(item => item.IsActive && item.PodcastId == PodcastId);
        }

        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new SongViewModel(SelectedItem.EpisodeId)));
            }
        }

        protected override List<GenericComboBoxVM<string>> GetSearchColumns()
        {
            return new()
            {
                new("Title",nameof(Episode.EpisodeName)),
                new("Release date", nameof(Episode.ReleaseDate)),
                new("Id", nameof(Episode.EpisodeId)),
                new("Duration", nameof(Episode.EpisodeDuration)),
            };
        }

        protected override IQueryable<Episode> Search(IQueryable<Episode> models)
        {
            switch (SearchColumn)
            {
                case nameof(Episode.EpisodeName):
                    return models.Where(item => item.EpisodeName.Contains(SearchInput));
                case nameof(Episode.ReleaseDate):
                    return models.Where(item => item.ReleaseDate.ToString().Contains(SearchInput));
                case nameof(Episode.EpisodeId):
                    return models.Where(item => item.EpisodeId.ToString().Contains(SearchInput)); 
                case nameof(Episode.EpisodeDuration):
                    return models.Where(item => item.EpisodeDuration.ToString().Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<Episode> Sort(IQueryable<Episode> models)
        {
            switch (SortColumn)
            {
                case nameof(Episode.EpisodeName):
                    return SortDescending ? models.OrderByDescending(item => item.EpisodeName) : models.OrderBy(item => item.EpisodeName);
                case nameof(Episode.ReleaseDate):
                    return SortDescending ? models.OrderByDescending(item => item.ReleaseDate) : models.OrderBy(item => item.ReleaseDate);
                case nameof(Episode.EpisodeId):
                    return SortDescending ? models.OrderByDescending(item => item.EpisodeId) : models.OrderBy(item => item.EpisodeId);
                case nameof(Episode.EpisodeDuration):
                    return SortDescending ? models.OrderByDescending(item => item.EpisodeDuration) : models.OrderBy(item => item.EpisodeDuration);
                default:
                    return models;
            }
        }
    }
}
