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
using System.Windows.Media.Animation;
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
        public int FadeoutDuration { get; set; } = 200;
        private Storyboard storyboard = new Storyboard();
        private DoubleAnimation fadeAnimation = new DoubleAnimation();

        public MySplashWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)(arg =>
            {
                ((MySplashWindow)arg).Fadeout(FadeoutDuration);
                return null;
            }), this);

            storyboard.Completed += FadeoutCompleted;

        }

        private void Fadeout(int fadeoutDuration)
        {
            Storyboard.SetTarget(fadeAnimation, this);
            Storyboard.SetTargetProperty(fadeAnimation,new PropertyPath("Opacity"));
            fadeAnimation.From = 1;
            fadeAnimation.To = 0;
            fadeAnimation.Duration = TimeSpan.FromMilliseconds(fadeoutDuration);
            storyboard.Children.Add(fadeAnimation);
            storyboard.Begin();

        }

        private void FadeoutCompleted(object? sender, EventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                Close();
            } else
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (DispatcherOperationCallback)(arg =>
                {
                    ((MySplashWindow)arg).Close();
                    return null;
                }), this);
            }
        }
    }
}
