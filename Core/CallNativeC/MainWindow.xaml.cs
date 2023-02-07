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

namespace CallNativeC
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

        private void btn_call_foo_Click(object sender, RoutedEventArgs e)
        {
            CWrapper.Foo();
        }

        private void btn_call_add_Click(object sender, RoutedEventArgs e)
        {
            int sum = CWrapper.Add(1, 2);
            Console.WriteLine($"1 + 2 = {sum}");
        }

        private void btn_call_sum_Click(object sender, RoutedEventArgs e)
        {
            double[] arr = new double[] { 1, 3, 4 };
            double sum = CWrapper.Sum(arr, arr.Length);
            Console.WriteLine($"Sum = {sum}");
        }

        private void btn_call_change_Click(object sender, RoutedEventArgs e)
        {
            double[] arr = new double[] { 1, 3, 4 };
            Console.Write("Original Value:\t");
            foreach(double d in arr)
            {
                Console.Write($"{d}\t");
            }
            Console.WriteLine();
            CWrapper.Change(arr, arr.Length);
            Console.Write("Changed Value:\t");
            foreach (double d in arr)
            {
                Console.Write($"{d}\t");
            }
            Console.WriteLine();
        }
    }
}
