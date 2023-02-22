using MouseTracker.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MouseTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public string Info { get; set; } = string.Empty;

		private CursorWindow cursorWindow;
		public MainWindow()
		{
			InitializeComponent();
			DataContext= this;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MouseHookUtils.InstallHook();
			MouseHookUtils.MouseEvent += MouseHookEventHanlder;
			cursorWindow = new CursorWindow();
			cursorWindow.Owner = this;
			cursorWindow.Topmost = true;
			cursorWindow.Show();
		}

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			MouseHookUtils.UninstallHook();
		}

		private void MouseHookEventHanlder(object? sender, LowLevelMouseEventArgs e)
		{
			Info = $"{e.Button}\t{e.Type}\t{e.Point}\t\t{e.Angle}";
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));
			cursorWindow.Left = e.Point.x - cursorWindow.Width / 2;
			cursorWindow.Top = e.Point.y - cursorWindow.Height / 2;
		}

		private void Show_Click(object sender, RoutedEventArgs e)
		{
			MouseTrackingService.GetService().Start();
		}

		private void Hide_Click(object sender, RoutedEventArgs e)
		{
			MouseTrackingService.GetService().Stop();
		}
	}
}
