using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomRenderControl
{
    internal class MyCustomRenderControl : Control
    {
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
    }
}
