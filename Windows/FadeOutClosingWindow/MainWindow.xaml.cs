using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FadeOutClosingWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard fadeOutStoryboard = new Storyboard();
        private DoubleAnimation fadeOutAnimation = new DoubleAnimation();
        private bool FadeOutCompleteFlag = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!FadeOutCompleteFlag)
            {
                fadeOutStoryboard.Children.Clear();
                Storyboard.SetTarget(fadeOutAnimation, this);
                Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));
                fadeOutAnimation.From = 1;
                fadeOutAnimation.To = 0;
                fadeOutAnimation.Duration = TimeSpan.FromMilliseconds(2000);
                fadeOutStoryboard.Children.Add(fadeOutAnimation);
                fadeOutStoryboard.Completed += FadeOutStoryboard_Completed;
                fadeOutStoryboard.Begin();
                e.Cancel = true;
            }
        }

        private void FadeOutStoryboard_Completed(object? sender, EventArgs e)
        {
            FadeOutCompleteFlag = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}