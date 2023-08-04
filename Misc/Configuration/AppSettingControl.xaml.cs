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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace MyConfiguration
{
    /// <summary>
    /// AppSettingControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppSettingControl : UserControl
    {
        private Configuration config;
        private string sectionName = "appSettings";

        public AppSettingControl()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                //configFile.ExeConfigFilename = @"myconfiguration.config";
                //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //string key = "Key" + config.AppSettings.Settings.Count;
                //string value = "Value" + config.AppSettings.Settings.Count;
                //config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(sectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace?.ToString());
            }
        }

        private void btnReadAll_Click(object sender, RoutedEventArgs e)
        {
            // Get the AppSettings section.
            try
            {
                AppSettingsSection appSettingSection =
                             (AppSettingsSection)config.GetSection(sectionName);

                Console.WriteLine();
                Console.WriteLine("Using GetSection(string).");
                Console.WriteLine("AppSettings section:");
                Console.WriteLine(
                appSettingSection.SectionInformation.GetRawXml());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace?.ToString());
            }
           
        }
    

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            string key = keyEditBox.Text;
            try
            {
                // Get the AppSettings section.
                AppSettingsSection appSettingSection =
                  (AppSettingsSection)config.GetSection(sectionName);
                if (appSettingSection.Settings.AllKeys.Contains(key)) {
                    string value = appSettingSection.Settings[key].Value;
                    valueEditBox.Text = value;
                } else
                {
                    valueEditBox.Text = "null";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace?.ToString());
            }

            
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            string key = keyEditBox.Text;
            string value = valueEditBox.Text;


            try
            {
                AppSettingsSection appSettingSection =
                    (AppSettingsSection)config.GetSection(sectionName);
                var settings = appSettingSection.Settings;
                if (settings[key] != null)
                {
                    settings[key].Value = value;
                } else
                {
                    settings.Add(key, value);
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(sectionName);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace?.ToString());
            }

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            string key = keyEditBox.Text;
            string value = valueEditBox.Text;

            AppSettingsSection appSettingSection =
              (AppSettingsSection)config.GetSection(sectionName);
            try
            {
                var settings = appSettingSection.Settings;
                if (settings[key] != null)
                {
                    settings.Remove(key);
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(sectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace?.ToString());
            }
        }
    }
}
