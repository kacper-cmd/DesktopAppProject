using MusicApp.ViewModels;
using MusicApp.ViewModels.ManyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.Views.ManyViews
{
    /// <summary>
    /// Interaction logic for LikedSongsView.xaml
    /// </summary>
    public partial class LikedSongsView : BaseManyView
    {
        public LikedSongsView()
        {
            InitializeComponent();
        }

        public LikedSongsView(WorkspaceViewModel likedSongsViewModel)
        {
            DataContext = likedSongsViewModel;
            InitializeComponent();
        }

        private bool isImageClicked = false;

        private void PlayImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isImageClicked)
            {
                // Change the image source to the new image
                PlayImage.Source = new BitmapImage(new Uri("pack://application:,,,/Views/ViewResources/Icons/pause.png"));
            }
            else
            {
                // Change the image source back to the original image
                PlayImage.Source = new BitmapImage(new Uri("pack://application:,,,/Views/ViewResources/Icons/play.png"));
            }

            // Toggle the clicked state
            isImageClicked = !isImageClicked;
        }
    }
}
