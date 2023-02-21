using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MouseTracker.Core
{
	public class MouseTrackingService
	{
		[DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetDesktopWindow();//该函数返回桌面窗口的句柄。
		[DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr WindowFromPoint(Point point);//该函数返回桌面窗口的句柄。
		[DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags); //获取显示设备上下文环境的句柄
		[DllImport("user32.dll", EntryPoint = "InvalidateRect", CharSet = CharSet.Auto, ExactSpelling = true)]
		static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
		[DllImport("user32.dll", EntryPoint = "UpdateWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
		static extern bool UpdateWindow(IntPtr hWnd);
		[DllImport("user32.dll", EntryPoint = "RedrawWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
		static extern bool RedrawWindow(IntPtr hWnd, ref Rectangle lprcUpdate, IntPtr hrgnUpdate, uint fuRedraw);
		[DllImport("user32.dll", EntryPoint = "ReleaseDC", ExactSpelling = true)]
		private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);
		const int RDW_INVALIDATE = 0x0001;
		const int RDW_INTERNALPAINT = 0x0002;
		const int RDW_ERASE = 0x0004;

		const int RDW_VALIDATE = 0x0008;
		const int RDW_NOINTERNALPAINT = 0x0010;
		const int RDW_NOERASE = 0x0020;

		const int RDW_NOCHILDREN = 0x0040;
		const int RDW_ALLCHILDREN = 0x0080;

		const int RDW_UPDATENOW = 0x0100;
		const int RDW_ERASENOW = 0x0200;

		const int RDW_FRAME = 0x0400;
		const int RDW_NOFRAME = 0x0800;

		private static MouseTrackingService Instance = new();
		private DispatcherTimer timer = new();
		private Point p;
		public static MouseTrackingService GetService() => Instance;

		public void Start()
		{
			MouseHookUtils.MouseEvent += MouseHookEventHandler;
			timer.Interval += new TimeSpan(2000000);
			timer.Start();
		}

		private void Timer_Tick(object? sender, EventArgs e)
		{
			IntPtr pHwnd = WindowFromPoint(p);
			IntPtr hwnd = GetDesktopWindow();
			InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
			UpdateWindow(hwnd);
			IntPtr hdc = GetDCEx(hwnd, IntPtr.Zero, 0x403);

			Rectangle rect = new Rectangle(p.x - 10, p.y - 10, 20, 20);
			using (Graphics g = Graphics.FromHdc(hdc))
			{
				g.FillEllipse(Brushes.Yellow, rect);
			}
			ReleaseDC(hwnd, hdc);
		}

		public void Stop()
		{
			MouseHookUtils.MouseEvent -= MouseHookEventHandler;
			IntPtr hwnd = GetDesktopWindow();
			InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
			timer.Stop();
		}
		private	MouseTrackingService()
		{
			timer.Tick += Timer_Tick;
		}

		private void MouseHookEventHandler(object? sender, LowLevelMouseEventArgs e)
		{
			p = e.Point;
		}
	}
}
