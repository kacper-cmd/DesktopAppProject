using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MusicApp
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private bool isImageClicked = false;

        private string selectedView;

        private DispatcherTimer timer;

        public string SelectedView
        {
            get { return selectedView; }
            set
            {
                if (selectedView != value)
                {
                    selectedView = value;
                    OnPropertyChanged(nameof(SelectedView));
                }
            }
        }

        private string currentTime;
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        public MainWindow()
        {
            //DataContext = ;
            SelectedView = "Home"; // Set the default view to "Home"

            // Initialize and start the clock timer
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Start the timer to update the time every second
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the TextBlock with the current time
            TimeTextBlock.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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