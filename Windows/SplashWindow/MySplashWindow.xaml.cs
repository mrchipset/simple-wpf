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
        public int FadeOutDuration { get; set; } = 200;
        private Storyboard fadeOutStoryboard = new Storyboard();
        private DoubleAnimation fadeOutAnimation = new DoubleAnimation();

        public int FadeInDuration { get; set; } = 600;
        private Storyboard fadeInStoryboard = new Storyboard();
        private DoubleAnimation fadeInAnimation = new DoubleAnimation();

        public MySplashWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Topmost = true;
            InitializeComponent();

            //Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)(arg =>
            //{
            //    ((MySplashWindow)arg).Fadeout(FadeOutDuration);
            //    return null;
            //}), this);

            fadeOutStoryboard.Completed += FadeoutCompleted;

        }

        public void FadeClose()
        {
            Fadeout(FadeOutDuration);
        }

        private void Fadeout(int fadeoutDuration)
        {
            fadeOutStoryboard.Children.Clear();
            Storyboard.SetTarget(fadeOutAnimation, this);
            Storyboard.SetTargetProperty(fadeOutAnimation,new PropertyPath("Opacity"));
            fadeOutAnimation.From = 1;
            fadeOutAnimation.To = 0;
            fadeOutAnimation.Duration = TimeSpan.FromMilliseconds(fadeoutDuration);
            fadeOutStoryboard.Children.Add(fadeOutAnimation);
            fadeOutStoryboard.Begin();

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fadeInStoryboard.Children.Clear();
            Storyboard.SetTarget(fadeInAnimation, this);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromMilliseconds(FadeInDuration);
            fadeInStoryboard.Children.Add(fadeInAnimation);
            fadeInStoryboard.Begin();
        }
    }
}
