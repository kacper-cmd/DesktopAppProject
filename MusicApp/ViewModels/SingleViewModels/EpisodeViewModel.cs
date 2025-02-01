using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicApp.Models;
using MusicApp.Views.ViewResources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels.SingleViewModels
{
    public class EpisodeViewModel : BaseSingleViewModel<Episode>
    {
        #region FieldsAndProperties
        public List<ComboBoxVM> Podcasts { get; set; }
        public string EpisodeName
        {
            get => Model.EpisodeName;
            set
            {
                if (EpisodeName != value)
                {
                    Model.EpisodeName = value;
                    OnPropertyChanged(() => EpisodeName);
                }
            }
        }
        public string? EpisodeDescription
        {
            get => Model.EpisodeDescription;
            set
            {
                if (EpisodeDescription != value)
                {
                    Model.EpisodeDescription = value;
                    OnPropertyChanged(() => EpisodeDescription);
                }
            }
        }
        public int PodcastID
        {
            get => Model.PodcastId;
            set
            {
                if (PodcastID != value)
                {
                    Model.PodcastId = value;
                    OnPropertyChanged(() => PodcastID);
                }
            }
        }
        public int EpisodeDuration
        {
            get => Model.EpisodeDuration;
            set
            {
                if (EpisodeDuration != value)
                {
                    Model.EpisodeDuration = value;
                    OnPropertyChanged(() => EpisodeDuration);
                }
            }
        }
        public DateTime? ReleaseDate
        {
            get => Model.ReleaseDate;
            set
            {
                if (ReleaseDate != value)
                {
                    Model.ReleaseDate = value;
                    OnPropertyChanged(() => ReleaseDate);
                }
            }
        }

        #endregion
        public EpisodeViewModel() : base(GlobalResources.Episode)
        {
            Initialize();
        }

        private void Initialize()
        {
            Podcasts = Database.Podcasts.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.PodcastId,
                Title = item.PodcastName
            }
            ).ToList();

            WeakReferenceMessenger.Default.Register<string>(this, (recipent, message) =>
            {
                Debug.WriteLine(message);
                Debug.WriteLine(recipent);
            });
        }

        protected override DbSet<Episode> GetDBTable()
        {
            return Database.Episodes;
        }

        protected override Episode? GetModelFromDatabase(int id)
        {
            return GetDBTable().Include(item => item.Podcast)
                                        .Where(item => item.IsActive)
                                        .First(item => item.EpisodeId == id);
        }

        protected override Episode InitializeModel()
        {
            return new Episode()
            {
                IsActive = true,
                CreatedDate = DateTime.Now,
            };
        }

        protected override string? ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(PodcastID):
                    if (PodcastID == 0)
                    {
                        return "Podcast is not set.";
                    }
                    break;

                case nameof(EpisodeName):
                    if (EpisodeName.IsNullOrEmpty())
                    {
                        return "Episode title cannot be empty.";
                    }
                    break;

                case nameof(EpisodeDuration):
                    if (EpisodeDuration == 0 || EpisodeDuration <= 0)
                    {
                        return "Duration must be a number above 0.";
                    }
                    break;
                
            }
            return null;
        }

        protected override void Select()
        {
            //throw new NotImplementedException();
        }
    }
}
