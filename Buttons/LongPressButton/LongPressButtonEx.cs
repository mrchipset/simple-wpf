using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LongPressButton
{
    public class LongPressButtonEx : Button
    {
        /// <summary>
        /// DepedencyProperty for LongPressTime
        /// </summary>
        public static readonly DependencyProperty LongPressTimeProperty
            = DependencyProperty.Register("LongPressTime", typeof(int),
                typeof(LongPressButtonEx), new PropertyMetadata(500));

        /// <summary>
        /// DependencyProperty for IsLongPress 
        /// </summary>
        public static readonly DependencyProperty IsLongPressProperty
            = DependencyProperty.Register("IsLongPress", typeof(bool),
                typeof(LongPressButtonEx), new PropertyMetadata(false));

        /// <summary>
        /// LongPress Routed Event
        /// </summary>
        public static readonly RoutedEvent LongPressEvent
            = EventManager.RegisterRoutedEvent("LongPress",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(LongPressButtonEx));

        /// <summary>
        /// LongPressRelease Routed Event
        /// </summary>
        public static readonly RoutedEvent LongPressReleaseEvent
            = EventManager.RegisterRoutedEvent("LongPressRelease",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(LongPressButtonEx));

        public int LongPressTime
        {
            set => SetValue(LongPressTimeProperty, value);
            get => (int)GetValue(LongPressTimeProperty);
        }

        public bool IsLongPress
        {
            set => SetValue(IsLongPressProperty, value);
            get => (bool)GetValue(IsLongPressProperty);
        }

        public event RoutedEventHandler LongPress
        {
            add => AddHandler(LongPressEvent, value);
            remove => RemoveHandler(LongPressEvent, value);
        }

        public event RoutedEventHandler LongPressRelease
        {
            add => AddHandler(LongPressReleaseEvent, value);
            remove => RemoveHandler(LongPressReleaseEvent, value);
        }

        /// <summary>
        /// DispatcherTimer to listen whether the button is long clicked.
        /// </summary>
        private DispatcherTimer _pressDispatcherTimer;

        private void OnDispatcherTimeOut(object sender, EventArgs e)
        {
            IsLongPress = true;
            _pressDispatcherTimer?.Stop();
            Debug.WriteLine($"Timeout {LongPressTime}");
            RaiseEvent(new RoutedEventArgs(LongPressEvent));    // raise the long press event
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Debug.WriteLine("Button: Mouse down.");
            if (_pressDispatcherTimer == null)
            {
                _pressDispatcherTimer = new DispatcherTimer();
                _pressDispatcherTimer.Tick += OnDispatcherTimeOut;
                _pressDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, LongPressTime);
                _pressDispatcherTimer.Start();
                Debug.WriteLine("Button: Timer started");
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Debug.WriteLine("Button: Mouse up.");
            _pressDispatcherTimer?.Stop();
            _pressDispatcherTimer = null;
        }

        protected override void OnClick()
        {
            if (!IsLongPress)
            {
                base.OnClick();
            }
            else
            {
                RaiseEvent(new RoutedEventArgs(LongPressReleaseEvent));    // raise the long press event
                IsLongPress = false;
            }
        }
    }
}
