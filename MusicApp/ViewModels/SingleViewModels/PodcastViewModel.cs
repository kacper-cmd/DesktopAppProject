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
    public class PodcastViewModel : BaseSingleViewModel<Podcast>
    {
        #region FieldsAndProperties
        public List<ComboBoxVM> Users { get; set; }
        public int UserID
        {
            get => Model.UserId;
            set
            {
                if (UserID != value)
                {
                    Model.UserId = value;
                    OnPropertyChanged(() => UserID);
                }
            }
        }
        public string PodcastName
        {
            get => Model.PodcastName;
            set
            {
                if (PodcastName != value)
                {
                    Model.PodcastName = value;
                    OnPropertyChanged(() => PodcastName);
                }
            }
        }
        public string? PodcastDescription
        {
            get => Model.PodcastDescription;
            set
            {
                if (PodcastDescription != value)
                {
                    Model.PodcastDescription = value;
                    OnPropertyChanged(() => PodcastDescription);
                }
            }
        }
        #endregion
        #region Contructors
        public PodcastViewModel() : base(GlobalResources.Playlist)
        {
            Initialize();
        }

        public PodcastViewModel(int id) : base(id, GlobalResources.Playlist)
        {
            Initialize();
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            Users = Database.Users.Where(item => item.IsActive).Select(item => new ComboBoxVM()
            {
                Id = item.UserId,
                Title = item.UserName
            }
            ).ToList();
            WeakReferenceMessenger.Default.Register<string>(this, (recipent, message) =>
            {
                Debug.WriteLine(message);
                Debug.WriteLine(recipent);
            });
        }
        protected override DbSet<Podcast> GetDBTable()
        {
            return Database.Podcasts;
        }

        protected override Podcast? GetModelFromDatabase(int id)
        {
            return GetDBTable().Include(item => item.User)
                                .Where(item => item.IsActive)
                                .First(item => item.PodcastId == id);
        }   

        protected override Podcast InitializeModel()
        {
            return new()
            {
                IsActive = true,
                CreatedDate = DateTime.Now,
            };
        }

        protected override string? ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(PodcastName):
                    if (PodcastName.IsNullOrEmpty())
                    {
                        return "Playlist name cannot be empty";
                    }
                    break;

                case nameof(UserID):
                    if (UserID == 0)
                    {
                        return "You must choose a creator";
                    }
                    break;
            }
            return null;
        }

        protected override void Select()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
