using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.Foundation.Collections;

namespace ToastNotification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            ToastArguments args = ToastArguments.Parse(e.Argument);
            ValueSet userInput = e.UserInput;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MessageBox.Show("Toast activated. Args:" + e.Argument + " State:" + ToastNotificationManagerCompat.WasCurrentProcessToastActivated());
            }));
        }

        private void btnToast_Click(object sender, RoutedEventArgs e)
        {
            // Code from learn.microsoft.com
            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            //new ToastContentBuilder()
            //    .AddArgument("action", "viewConversation")
            //    .AddArgument("conversationId", 9813)
            //    .AddText("Andrew sent you a picture")
            //    .AddText("Check this out, The Enchantments in Washington!")
            //    .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
            // A more complex toast
            // https://learn.microsoft.com/zh-cn/windows/apps/design/shell/tiles-and-notifications/adaptive-interactive-toasts?tabs=toolkit
            var builder = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)

                .AddText("Some text")

                .AddButton(new ToastButton()
                    .SetContent("Archive")
                    .AddArgument("action", "archive"))
                .AddButton(new ToastButton()
                    .SetContent("Show")
                    .AddArgument("action", "show"))
                ;
            builder.Show();
        }
    }
}
