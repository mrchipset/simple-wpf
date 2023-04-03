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

namespace BindingLabel
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ContentModel contentModel;
		public MainWindow()
		{
			InitializeComponent();
			contentModel = new ContentModel();
			this.DataContext= contentModel;
		}

		private void btn_Click(object sender, RoutedEventArgs e)
		{
			string[] list = new string[] { "Hello", "World", "Foo", "Bar" };
			Random random= new Random();
			int id = random.Next(list.Length);
			contentModel.Content = list[id];
		}
	}
}
