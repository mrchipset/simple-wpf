using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlipNumber
{
    public class NumberDigitControl : Control
    {
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(NumberDigitControl));   
        public int Number
        {
            get
            {
                int? v = GetValue(NumberProperty) as int?;
                return v ?? 0;
            }
            set
            {
                SetValue(NumberProperty, value);
            }
        }
        public NumberDigitControl() : base()
        {
            MinWidth = 32;
            MinHeight = 64;
            FontSize = 32;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            double dip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
            FormattedText formattedText = new FormattedText(Number.ToString(),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                this.Foreground ?? Brushes.Black,
                VisualTreeHelper.GetDpi(this).PixelsPerDip
                );
            formattedText.TextAlignment = TextAlignment.Center;
            
            double width = formattedText.Width;
            double height = formattedText.Height;
            double centerX = this.ActualWidth / 2;
            double centerY = this.ActualHeight / 2 - height / 2;
            //drawingContext.DrawRectangle(Background ?? Brushes.Black, new Pen(BorderBrush ?? Brushes.White, 1), new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            drawingContext.DrawRoundedRectangle(Background ?? Brushes.Black, new Pen(BorderBrush ?? Brushes.White, 1), new Rect(15, 15, this.ActualWidth - 30, this.ActualHeight - 30), 15, 15);
            drawingContext.DrawText(formattedText, new Point(centerX, centerY));
        }
    }
}
