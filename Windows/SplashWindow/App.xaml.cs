using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;


namespace SplashWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DispatcherTimer Timer { get; set; } = new();
        public MySplashWindow splash;
        protected override async void OnStartup(StartupEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.MainWindow = mainwindow;
            splash = new MySplashWindow();
            splash.Show();
            base.OnStartup(e);

            await Task.Delay(2000).ContinueWith(_ =>
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)(arg =>
                {
                    App app = (App)arg;
                    app.splash.FadeClose();
                    app.MainWindow.Show();
                    return null;
                }), this);
                
            });
       
        }


    }

}
