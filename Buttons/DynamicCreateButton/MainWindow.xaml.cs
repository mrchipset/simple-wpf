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

namespace DynamicCreateButton
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public Button button;
		public Property property;
		public MainWindow()
		{
			InitializeComponent();

			button = new Button();
			button.Width = 256;
			button.Height = 24;
			button.Name = "Button";
			property= new Property();
			Binding newBinding= new Binding("Name");
			newBinding.Source = property;
			button.SetBinding(Button.ContentProperty, newBinding);
			button.Click += Button_Click;
			this.container.Children.Add(button);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			property.Name = "Clicked";
		}
	}
}
