using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseTracker.Core
{
	public struct Point
	{
		public int x;
		public int y;

		public override string ToString() 
		{
			return $"x: {x}, y:{y}";
		}
	}

	public class LowLevelMouseEventArgs : EventArgs
	{
		public bool Handled { get; set; }

		public Point Point { get; set; }

		public EventType Type { get; set; }
		public MouseButton Button { get; set; }
		public int Angle { get; set; }

		public enum EventType
		{
			MouseDown = 0x01,
			MouseUp = 0x01 << 1,
			MouseMove = 0x01 << 2,
			MouseWheel = 0x01 << 3,
			MouseDoubleClick = 0x01 << 4,
		}

		public enum MouseButton
		{
			None = 0,
			Left = 1,
			Right = 2,
		}

		
		public LowLevelMouseEventArgs(EventType eventType, Point point) 
		{
			Handled = false;
			Angle = 0;
			Type = eventType;
			Point = point;
		}

		public LowLevelMouseEventArgs(EventType type)
		{
			Handled = false;
			Angle = 0;
			Type = type;
		}

		
	}
	public static class MouseHookUtils
	{
		private delegate IntPtr MouseProc(int nCode, IntPtr wParam, IntPtr lParam);
		private static MouseProc Proc = HookCallback;
		private static IntPtr HookID = IntPtr.Zero;
		public static event EventHandler<LowLevelMouseEventArgs>? MouseEvent = null;

		private const int WH_MOUSE_LL = 14;
		public const int WM_MOUSEMOVE = 0x200; // 鼠标移动
		public const int WM_LBUTTONDOWN = 0x201;// 鼠标左键按下
		public const int WM_RBUTTONDOWN = 0x204;// 鼠标右键按下
		public const int WM_MBUTTONDOWN = 0x207;// 鼠标中键按下
		public const int WM_LBUTTONUP = 0x202;// 鼠标左键抬起
		public const int WM_RBUTTONUP = 0x205;// 鼠标右键抬起
		public const int WM_MBUTTONUP = 0x208;// 鼠标中键抬起
		public const int WM_LBUTTONDBLCLK = 0x203;// 鼠标左键双击
		public const int WM_RBUTTONDBLCLK = 0x206;// 鼠标右键双击
		public const int WM_MBUTTONDBLCLK = 0x209;// 鼠标中键双击
		public const int WM_MOUSEWHEEL = 0x20A;//鼠标滚轮

		[StructLayout(LayoutKind.Sequential)]
		private struct LowlevelMouseHookStruct
		{
			public Point pt;
			public uint mouseData;
			public uint flags;
			public uint time;
			public IntPtr dwExtraInfo;
		}


		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			MouseProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
			IntPtr wParam, IntPtr lParam);

		// 取得当前线程编号（线程钩子需要用到）
		[DllImport("kernel32.dll")]
		static extern int GetCurrentThreadId();

		//使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string name);

		public static void InstallHook()
		{
			Process process= Process.GetCurrentProcess();
			ProcessModule? module = process.MainModule;
			if (module != null)
			{
				HookID = SetWindowsHookEx(WH_MOUSE_LL, Proc, GetModuleHandle(module.ModuleName), 0);
			}
		}

		public static void UninstallHook()
		{
			UnhookWindowsHookEx(HookID);
		}

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				LowlevelMouseHookStruct mouseHookStruct = (LowlevelMouseHookStruct)Marshal.PtrToStructure(lParam, typeof(LowlevelMouseHookStruct));
				//System.Diagnostics.Trace.WriteLine($"Point: {mouseHookStruct.pt.x}, {mouseHookStruct.pt.y}, Flag: {mouseHookStruct.flags}, Time: {mouseHookStruct.time} Mouse Data: {mouseHookStruct.mouseData}");
				LowLevelMouseEventArgs? mouseHookEventArgs = null;
				
				switch (wParam.ToInt32())
				{
					case WM_MOUSEMOVE:
						mouseHookEventArgs = (new LowLevelMouseEventArgs(LowLevelMouseEventArgs.EventType.MouseMove));
						break;
					case WM_LBUTTONDOWN:
					case WM_RBUTTONDOWN:
						mouseHookEventArgs = (new LowLevelMouseEventArgs(LowLevelMouseEventArgs.EventType.MouseDown));
						break;
					case WM_LBUTTONUP:
					case WM_RBUTTONUP:
						mouseHookEventArgs = (new LowLevelMouseEventArgs(LowLevelMouseEventArgs.EventType.MouseUp));
						break;
					case WM_LBUTTONDBLCLK:
					case WM_RBUTTONDBLCLK:
						mouseHookEventArgs = (new LowLevelMouseEventArgs(LowLevelMouseEventArgs.EventType.MouseDoubleClick));
						break;
					case WM_MOUSEWHEEL:
						mouseHookEventArgs = (new LowLevelMouseEventArgs(LowLevelMouseEventArgs.EventType.MouseWheel));
						ushort mouseData = (ushort)(mouseHookStruct.mouseData >> 16);
						short angle = BitConverter.ToInt16(BitConverter.GetBytes(mouseData), 0);
						mouseHookEventArgs.Angle = angle;
						break;
				}

				LowLevelMouseEventArgs.MouseButton button = LowLevelMouseEventArgs.MouseButton.None;
				switch (wParam.ToInt32())
				{
					case WM_MOUSEMOVE:
						button = LowLevelMouseEventArgs.MouseButton.None;
						break;
					case WM_RBUTTONUP:
					case WM_RBUTTONDOWN:
					case WM_RBUTTONDBLCLK:
						button = LowLevelMouseEventArgs.MouseButton.Right;
						break;
					case WM_LBUTTONUP:
					case WM_LBUTTONDOWN:
					case WM_LBUTTONDBLCLK:
						button = LowLevelMouseEventArgs.MouseButton.Left;
						break;
				}

				if (mouseHookEventArgs != null)
				{
					mouseHookEventArgs.Point = mouseHookStruct.pt;
					mouseHookEventArgs.Button = button;
				
					OnMouseEvent(mouseHookEventArgs);

					if (mouseHookEventArgs.Handled)
					{
						return IntPtr.Zero;
					}
				}

			}
			return CallNextHookEx(HookID, nCode, wParam, lParam);
		}

		private static void OnMouseEvent(LowLevelMouseEventArgs e)
		{
			MouseEvent?.Invoke(null, e);
		}
	}
}
