using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseHook
{
    public class MouseHookEventArgs : EventArgs
    {
        public bool Handle { get; set; }

        /// <inheritdoc />
        public MouseHookEventArgs(MouseMessages mouseMessage)
        {
            MouseMessage = mouseMessage;
        }

        public MouseMessages MouseMessage { get; }

        public enum MouseMessages
        {
            MouseDown,
            MouseMove,
            MouseUp,
        }
    }

    /// <summary>
    /// 鼠标钩子
    /// </summary>
    public static class MouseHookUtils
    {
        private delegate IntPtr MouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static MouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
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

        private enum MouseMessages
        {
            WM_MOUSEMOVE = 0x0200
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
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

        /// <summary>
        /// 开启全局钩子
        /// </summary>
        /// <param name="moduleName"></param>
        public static void Start(string moduleName)
        {
            Debug.WriteLine($"模块 {moduleName} 开启全局鼠标钩子");

            _hookID = SetHook(_proc);
        }

        public static void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(MouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                     GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
            }
        }
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                System.Diagnostics.Trace.WriteLine($"Point: {mouseHookStruct.pt.x}, {mouseHookStruct.pt.y}, Flag: {mouseHookStruct.flags}, Time: {mouseHookStruct.time} Mouse Data: {mouseHookStruct.mouseData}");
                MouseHookEventArgs mouseHookEventArgs = null;
                switch (wParam.ToInt32())
                {
                    case WM_MOUSEMOVE:
                        mouseHookEventArgs = (new MouseHookEventArgs(MouseHookEventArgs.MouseMessages.MouseMove));
                        System.Diagnostics.Trace.WriteLine("Move");
                        break;
                    case WM_LBUTTONDOWN:
                        mouseHookEventArgs = (new MouseHookEventArgs(MouseHookEventArgs.MouseMessages.MouseDown));
                        System.Diagnostics.Trace.WriteLine("Press");
                        break;
                    case WM_LBUTTONUP:
                        mouseHookEventArgs = (new MouseHookEventArgs(MouseHookEventArgs.MouseMessages.MouseUp));
                        System.Diagnostics.Trace.WriteLine("Release");
                        break;
                }
                //MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                if (mouseHookEventArgs != null)
                {
                    OnMouseEvent(mouseHookEventArgs);

                    if (mouseHookEventArgs.Handle)
                    {
                        return IntPtr.Zero;
                    }
                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public static event EventHandler<MouseHookEventArgs> MouseEvent;

        private static void OnMouseEvent(MouseHookEventArgs e)
        {
            MouseEvent?.Invoke(null, e);
        }
    }
}
