using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace JoyStick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JoyStickEx_Click(object sender, JoyStickRoutedEventArgs e)
        {
            Debug.WriteLine($"JoyStick: Click {e.Direction}");
        }

        private void JoyStickEx_LongPress(object sender, JoyStickRoutedEventArgs e)
        {
            Debug.WriteLine($"JoyStick: Long Press {e.Direction}");
        }

        private void JoyStickEx_LongPressRelease(object sender, JoyStickRoutedEventArgs e)
        {
            Debug.WriteLine($"JoyStick: Long Press Release {e.Direction}");
        }
    }
}
