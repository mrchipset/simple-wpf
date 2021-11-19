using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LongPressButton;

namespace JoyStick
{
    public class ArrowButton : LongPressButtonEx
    {
        public static readonly DependencyProperty AngleProperty
            = DependencyProperty.Register("Angle",
                typeof(double),
                typeof(ArrowButton),
                new PropertyMetadata(0.0));

        public double Angle
        {
            set => SetValue(AngleProperty, value);
            get => (double)GetValue(AngleProperty);
        }
    }
}
