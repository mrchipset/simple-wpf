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

namespace FlipNumber
{
    /// <summary>
    /// FlipDigitControl.xaml 的交互逻辑
    /// </summary>
    public partial class FlipDigitControl : UserControl
    {

        public int CurrentNum {  get; set; }
        public int PrevNum { get; set; }
        public FlipDigitControl()
        {
            CurrentNum = 1;
            PrevNum = 1;
            this.DataContext = this;
            InitializeComponent();

            System.Diagnostics.Trace.WriteLine("");
        }
    }
}
