using Serilog;
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

namespace Log
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            var log = MyLogger.GetInstance();
            log.Logger.Information("Create Serilog Configuration.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var log = MyLogger.GetInstance();
            log.Logger.Information("Add Serilog Configuration");
            AddBufferToText();
        }

        private void AddBufferToText()
        {
            var log = MyLogger.GetInstance();
            while (log.Events.TryDequeue(out var e))
            {
                this.logTxt.Text += '\n';
                this.logTxt.Text += e;

            }
        }
    }
}
