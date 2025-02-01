﻿using MusicApp.ViewModels;
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
    /// Interaction logic for PlaylistsView.xaml
    /// </summary>
    public partial class PlaylistsView : BaseManyView
    {
        public PlaylistsView()
        {
            InitializeComponent();
        }

        public PlaylistsView(WorkspaceViewModel playlistsViewModel)
        {
            DataContext = playlistsViewModel;
            InitializeComponent();
        }
    }
}
