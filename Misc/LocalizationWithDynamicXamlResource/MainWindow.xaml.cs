using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalizationWithDynamicXamlResource
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnChinese_Click(object sender, RoutedEventArgs e)
        {

            Resources.MergedDictionaries.Clear();
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("pack://application:,,,/Resources/style.xaml");
            ResourceDictionary langDictionary = new ResourceDictionary();
            langDictionary.Source = new Uri("pack://application:,,,/Resources/zh.xaml");
            Resources.MergedDictionaries.Add(styleDictionary);
            Resources.MergedDictionaries.Add(langDictionary);
        }

        private void BtnEnglish_Click(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries.Clear();
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("pack://application:,,,/Resources/style.xaml");
            ResourceDictionary langDictionary = new ResourceDictionary();
            langDictionary.Source = new Uri("pack://application:,,,/Resources/en.xaml");
            Resources.MergedDictionaries.Add(styleDictionary);
            Resources.MergedDictionaries.Add(langDictionary);

        }
    }
}