using CommunityToolkit.Mvvm.Messaging;
using MusicApp.Helpers;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels.ManyViewModels
{
    public class SelectSongsViewModel : SearchViewModel
    {
        public object Recipient { get; set; }

        public SelectSongsViewModel(object recipent) : base() 
        {
            Recipient = recipent;
        }
        public override void SelectModel()
        {
            if (SelectedItem != null)
            {
                WeakReferenceMessenger.Default.Send(new SelectModelMessage<Song>(Recipient, SelectedItem));
                OnRequestClose();
            }
        }
    }
}
