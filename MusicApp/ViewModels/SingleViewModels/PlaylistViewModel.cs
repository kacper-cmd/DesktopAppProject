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
    public class PlaylistViewModel : BaseSingleViewModel<Playlist>
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
        public string PlaylistName

        {
            get => Model.PlaylistName;
            set
            {
                if (PlaylistName != value)
                {
                    Model.PlaylistName = value;
                    OnPropertyChanged(() => PlaylistName);
                }
            }
        }
        #endregion

        #region Constructors
        public PlaylistViewModel() : base(GlobalResources.Playlist)
        {
            Initialize();
        }
        public PlaylistViewModel(int id) : base(id, GlobalResources.Playlist)
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
        protected override DbSet<Playlist> GetDBTable()
        {
            return Database.Playlists;
        }

        protected override Playlist? GetModelFromDatabase(int id)
         {
            return GetDBTable().Include(item => item.User)
                                .Where(item => item.IsActive)
                                .First(item => item.PlaylistId == id);
        }

        protected override Playlist InitializeModel()
        {
            return new Playlist()
            {
                IsActive = true,
                CreatedDate = DateTime.Now,
                PlaylistDuration = 0,
            };
        }

        protected override string? ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(PlaylistName):
                    if (PlaylistName.IsNullOrEmpty())
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
    } 
    #endregion
}
