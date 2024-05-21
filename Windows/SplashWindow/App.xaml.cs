using System.Configuration;
using System.Data;
using System.Windows;

namespace SplashWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //MainWindow mainwindow = new MainWindow();
            //this.MainWindow = mainwindow;
            MySplashWindow splash = new MySplashWindow();
            splash.Show();
            base.OnStartup(e);
            //mainwindow.Show();
            //splash.Close();
            

        }

    }

}
