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

namespace DynamicBindingLabel
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ContentModel contentModel;
		Label _label;
		public MainWindow()
		{
			InitializeComponent();
			contentModel = new ContentModel();
			this.DataContext = contentModel;
			_label = new Label();
			Binding binding= new Binding("Content");			// "Content" 相当于Xaml文件中制定的Path
			binding.Source = contentModel;						// 制定数据上下文源
			_label.SetBinding(Label.ContentProperty, binding);	// 绑定到Content属性
			this.container.Children.Add(_label);				// 添加创建的Label到Grid布局中
			_label.SetValue(Grid.RowProperty, 1);				// 设置Label所在行
			
		}

		private void btn_Click(object sender, RoutedEventArgs e)
		{
			string[] list = new string[] { "Hello", "World", "Foo", "Bar" };
			Random random = new Random();
			int id = random.Next(list.Length);
			contentModel.Content = list[id];
		}
	}
}
