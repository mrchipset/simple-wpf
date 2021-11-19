using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace JoyStick
{
    /// <summary>
    /// JoyStickEx.xaml 的交互逻辑
    /// </summary>
    public partial class JoyStickEx : UserControl
    {
        public static readonly RoutedEvent JoyStickClickEvent
            = EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(EventHandler<JoyStickRoutedEventArgs>),
                typeof(JoyStickEx)
                );

        public static readonly RoutedEvent JoyStickLongPressEvent
            = EventManager.RegisterRoutedEvent(
                "LongPress",
                RoutingStrategy.Bubble,
                typeof(EventHandler<JoyStickRoutedEventArgs>),
                typeof(JoyStickEx)
                );

        public static readonly RoutedEvent JoyStickLongPressReleaseEvent
            = EventManager.RegisterRoutedEvent(
                "LongPressRelease",
                RoutingStrategy.Bubble,
                typeof(EventHandler<JoyStickRoutedEventArgs>),
                typeof(JoyStickEx)
                );

        public event RoutedEventHandler Click
        {
            add => AddHandler(JoyStickClickEvent, value);
            remove => RemoveHandler(JoyStickClickEvent, value);
        }

        public event RoutedEventHandler LongPress
        {
            add => AddHandler(JoyStickLongPressEvent, value);
            remove => RemoveHandler(JoyStickLongPressEvent, value);
        }

        public event RoutedEventHandler LongPressRelease
        {
            add => AddHandler(JoyStickLongPressReleaseEvent, value);
            remove => RemoveHandler(JoyStickLongPressReleaseEvent, value);
        }

        private ArrowButton[] _btns;

        public JoyStickEx()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            _btns = new ArrowButton[4];
            _btns[(int)Direction.Forward] = BtnForward;
            _btns[(int)Direction.Right] = BtnRight;
            _btns[(int)Direction.Backward] = BtnBackward;
            _btns[(int)Direction.Left] = BtnLeft;
        }

        private Direction CheckSenderButtonDirection(object sender)
        {
            ArrowButton button = sender as ArrowButton;
            for (int i = 0; i < _btns.Length; ++i)
            {
                if (button == _btns[i])
                {
                    return (Direction)i;
                }
            }
            return Direction.Unknown;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Direction direction = CheckSenderButtonDirection(sender);
            if (direction != Direction.Unknown)
            {
                RaiseEvent(
                    new JoyStickRoutedEventArgs(JoyStickClickEvent, this)
                    {
                        Direction = direction
                    });
            }
            else
            {
                Debug.WriteLine("Unkown type button");
            }
        }

        private void Btn_LongPress(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Direction direction = CheckSenderButtonDirection(sender);
            if (direction != Direction.Unknown)
            {
                RaiseEvent(
                    new JoyStickRoutedEventArgs(JoyStickLongPressEvent, this)
                    {
                        Direction = direction
                    });
            }
            else
            {
                Debug.WriteLine("Unkown type button");
            }
        }

        private void Btn_LongPressRelease(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Direction direction = CheckSenderButtonDirection(sender);
            if (direction != Direction.Unknown)
            {
                RaiseEvent(
                    new JoyStickRoutedEventArgs(JoyStickLongPressReleaseEvent, this)
                    {
                        Direction = direction
                    });
            }
            else
            {
                Debug.WriteLine("Unkown type button");
            }
        }
    }

    public enum Direction
    {
        Forward,
        Right,
        Backward,
        Left,
        Unknown
    }

    public class JoyStickRoutedEventArgs : RoutedEventArgs
    {
        public JoyStickRoutedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }

        public Direction Direction { get; set; }
    }
}
