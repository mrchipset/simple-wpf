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

namespace SimpleXMLRpcDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RpcWorker rpcWorker;
        public MainWindow()
        {
            rpcWorker = new RpcWorker();
            InitializeComponent();
        }

        private void btnCallFoo_Click(object sender, RoutedEventArgs e)
        {
            string bar = rpcWorker.Foo();
            MessageBox.Show($"Call Rpc Method: Foo, Ret: {bar}.");
        }

        private void btnReturnStructure_Click(object sender, RoutedEventArgs e)
        {
            Structure bar = rpcWorker.BarWithStructureInvoke();
            MessageBox.Show($"Call Rpc Method: GetStructure, Ret structure:\n" +
                $"Name: {bar.Name}, Age: {bar.Age}, Gender: {bar.Gender}.");
        }

        private void btnPassArray_Click(object sender, RoutedEventArgs e)
        {
            double value = rpcWorker.SumArray(new double[] { 1, 2, 3, 4, 5 });
            MessageBox.Show($"Call Rpc Method: SumArray, Ret value: {value}.");
        }
    }
}
