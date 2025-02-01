using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace MusicApp.Views.ManyViews
{
    public class BaseManyView : UserControl
    {
        static BaseManyView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseManyView), new FrameworkPropertyMetadata(typeof(BaseManyView)));
        }
    }
}
