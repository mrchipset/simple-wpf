using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpinBox
{
    /// <summary>
    /// SpinBox.xaml 的交互逻辑
    /// </summary>
    [ContentProperty("Value")]
    public partial class MySpinBox : UserControl
    {

        /// <summary>
        /// DepedencyProperty for Step
        /// </summary>
        public static readonly DependencyProperty StepProperty
            = DependencyProperty.Register("Step", typeof(double),
                typeof(MySpinBox), new PropertyMetadata(1.0));

        /// <summary>
        /// DepedencyProperty for Value
        /// </summary>
        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register("Value", typeof(double),
                typeof(MySpinBox), new FrameworkPropertyMetadata(0.0,
                     FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                    | FrameworkPropertyMetadataOptions.Journal
                    | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnValueChanged))
                );

        /// <summary>
        /// DepedencyProperty for Precision
        /// </summary>
        public static readonly DependencyProperty PrecisionProperty
            = DependencyProperty.Register("Precision", typeof(int),
                typeof(MySpinBox), new PropertyMetadata(0));


        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                if (Value != value)
                {
                    SetValue(ValueProperty, value);
                }
            }
        }

        public int Precision
        {
            get => (int)GetValue(PrecisionProperty);
            set
            {
                if (Precision != value)
                {
                    SetValue(PrecisionProperty, value);
                }
            }
        }

        public double Step
        {
            get => (double)GetValue(StepProperty);
            set
            {
                if (Step != value)
                {
                    SetValue(StepProperty, value);
                }
            }
        }

        public MySpinBox()
        {
            InitializeComponent();
            txtBoxValue.Text = Value.ToString();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            Value += Step;
        }

        private void btnMinor_Click(object sender, RoutedEventArgs e)
        {
            Value -= Step;
        }

        private void txtBoxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Value = double.Parse(txtBoxValue.Text.Trim());
        }

        private void txtBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /**
             * Unfortunatelly, this method cannot filter the textbox contente.
             * It seems, for dotnet core, it is not possible to filter the text at input stage.
             * 
             */
            e.Handled = false;
        }

        
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spinBox = d as MySpinBox;
            if (spinBox != null)
            {
                spinBox.txtBoxValue.Text = e.NewValue.ToString();
            }
        }
    }
}
