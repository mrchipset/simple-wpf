using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;

namespace CustomRenderControl
{
    internal class MyCustomRenderControl : Control
    {
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
                "Click",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MyCustomRenderControl)
            );

        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        private bool _clicking = false;

        public MyCustomRenderControl() : base()
        {
            
        }

        
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Draw border
            Brush brush = Brushes.Red;
            Pen pen = new Pen(Brushes.Black, 2.0);
            
            double radiusX = 5;
            double radiusY = 5;
            Rect rectangle = new Rect(new Size(this.ActualWidth, this.ActualHeight));
            drawingContext.DrawRoundedRectangle(brush, pen, rectangle, radiusX, radiusY);

            // Draw text
            FormattedText formattedText = new FormattedText("Click",
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                16,
                Brushes.Black);

            double width = formattedText.Width;
            double height = formattedText.Height;
            double centerX = this.ActualWidth / 2 - width / 2;
            double centerY = this.ActualHeight / 2 - height / 2;

            drawingContext.DrawText(formattedText, new Point(centerX, centerY));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            return size;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(arrangeBounds);
            return size;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            Trace.WriteLine("Mouse Enter");
            Trace.Flush();

            _clicking = false;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            Trace.WriteLine("Mouse Leave");
            Trace.Flush();

            _clicking = false;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Trace.WriteLine("Mouse Left Button Down");
            Trace.Flush();

            _clicking = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Trace.WriteLine("Mouse Left Button Up");
            Trace.Flush();
            if (_clicking)
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent));
                _clicking = false;
            }
        }
    }
}
