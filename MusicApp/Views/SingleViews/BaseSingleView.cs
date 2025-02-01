using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Views.SingleViews
{
    public class BaseSingleView : UserControl
    {
        static BaseSingleView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseSingleView), new FrameworkPropertyMetadata(typeof(BaseSingleView)));
        }
    }
}
