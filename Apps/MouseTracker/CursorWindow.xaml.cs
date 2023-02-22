using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MouseTracker
{
	/// <summary>
	/// CursorWindow.xaml 的交互逻辑
	/// </summary>
	public partial class CursorWindow : Window
	{

		private const int WS_EX_TRANSPARENT = 0x20;

		private const int GWL_EXSTYLE = -20;

		[DllImport("user32", EntryPoint = "SetWindowLong")]
		private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

		[DllImport("user32", EntryPoint = "GetWindowLong")]
		private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

		public CursorWindow()
		{
			InitializeComponent();
		}

		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			IntPtr hwnd = new WindowInteropHelper(this).Handle;
			uint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
			SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
		}
    }
}
