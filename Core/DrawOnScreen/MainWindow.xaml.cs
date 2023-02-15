using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

namespace DrawOnScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();//该函数返回桌面窗口的句柄。
        [DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags); //获取显示设备上下文环境的句柄
        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
        private static int offset = 10;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr deskDC = GetDCEx(desk, IntPtr.Zero, 0x403);
            Graphics g = Graphics.FromHdc(deskDC);
            g.DrawString("测试", new Font("宋体", 50, System.Drawing.FontStyle.Bold), Brushes.Red, new PointF(100 + offset, 100 + offset));
            offset += 10;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr deskDC = GetDCEx(desk, IntPtr.Zero, 0x403);
            Graphics g = Graphics.FromHdc(deskDC);
            //g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            //g.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, 200, 200));
            //g.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, 1920, 1080));
            InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);

        }
    }
}
