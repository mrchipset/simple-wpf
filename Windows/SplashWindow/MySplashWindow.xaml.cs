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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SplashWindow
{
    /// <summary>
    /// SplashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MySplashWindow : Window
    {
        public MySplashWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)(arg =>
            {
                ((MySplashWindow)arg).Close();
                return null;
            }), this);
        }
    }
}
