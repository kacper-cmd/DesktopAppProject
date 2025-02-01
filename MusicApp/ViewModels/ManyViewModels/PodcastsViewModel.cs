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
    public class PodcastsViewModel : BaseManyViewModel<PodcastVM, PodcastsViewModel>
    {
        #region FieldsAndProperties
        //public ICommand OpenEpisodesViewCommand { get; set; }
        #endregion

        #region Constructors
        public PodcastsViewModel() : base(GlobalResources.Podcast)
        {
            //OpenEpisodesViewCommand = new BaseCommand(() => OpenEpisodesView());
        }
        #endregion

        #region Methods
        //private void OpenEpisodesView() => WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new EpisodesViewModel()));

        public override void AddNew()
        {
            Window window = new Window();
            window.Title = "Add new podcast";
            window.Height = 300;
            window.Width = 700;
            window.Content = new Views.SingleViews.PodcastView();
            window.Owner = App.Current.MainWindow;
            window.ShowDialog();
            Refresh();
        }

        public override void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Podcast? model = Database.Podcasts.FirstOrDefault(item => item.IsActive && item.PodcastId == SelectedItem.Podcast.PodcastId);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public override IQueryable<PodcastVM> GetModels()
        {
            return new DatabaseContext().Podcasts.Include(item => item.User)
                                                .Where(item => item.IsActive)
                                                .Select(item => new PodcastVM(item));
        }

        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage(new PlaylistViewModel(SelectedItem.Podcast.PodcastId)));
                //OpenEpisodesView();
            }
        }

        protected override List<GenericComboBoxVM<string>> GetSearchColumns()
        {
            return new()
            {
                new("Title",nameof(Podcast.PodcastName)),
                new("Release date",nameof(Podcast.CreatedDate)),
            };
        }

        protected override IQueryable<PodcastVM> Search(IQueryable<PodcastVM> models)
        {
            switch (SearchColumn)
            {
                case nameof(PodcastVM.Podcast.PodcastName):
                    return models.Where(item => item.Podcast.PodcastName.Contains(SearchInput));
                case nameof(PodcastVM.Podcast.CreatedDate):
                    return models.Where(item => item.Podcast.CreatedDate.ToString().Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected override IQueryable<PodcastVM> Sort(IQueryable<PodcastVM> models)
        {
            switch (SortColumn)
            {
                case nameof(PodcastVM.Podcast.PodcastName):
                    return SortDescending ? models.OrderByDescending(item => item.Podcast.PodcastName) : models.OrderBy(item => item.Podcast.PodcastName);
                case nameof(PodcastVM.Podcast.CreatedDate):
                    return SortDescending ? models.OrderByDescending(item => item.Podcast.CreatedDate) : models.OrderBy(item => item.Podcast.CreatedDate);
                default:
                    return models;
            }
        }
    } 
    #endregion
}
