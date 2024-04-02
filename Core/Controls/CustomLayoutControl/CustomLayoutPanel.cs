using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomLayoutControl
{
    class CustomLayoutPanel : Panel
    {
        public CustomLayoutPanel() : base() { }

        public double Spacing { get; set; }
        protected override Size ArrangeOverride(Size finalSize)
        {
            double offsetX = 0;
            double offsetY = 0;
            double spacing = Spacing;
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(offsetX, offsetY), child.DesiredSize));
                offsetX += child.DesiredSize.Width + spacing;
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();
            double spacing = Spacing;

            foreach (UIElement child in InternalChildren)
            {
               
                child.Measure(availableSize);
                panelDesiredSize.Width += child.DesiredSize.Width + spacing;
                panelDesiredSize.Height = Height;
            }
            return panelDesiredSize;
        }
    }
}
