using MusicApp.ViewModels;
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
    /// Interaction logic for PlaylistSongsView.xaml
    /// </summary>
    public partial class PlaylistSongsView : BaseManyView
    {
        public PlaylistSongsView()
        {
            InitializeComponent();
        }
        public PlaylistSongsView(WorkspaceViewModel playlistSongsViewModel)
        {
            DataContext = playlistSongsViewModel;
            InitializeComponent();
        }
    }
}
