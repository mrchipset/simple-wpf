using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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

namespace S3Uploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string _bucket;
        private string _uploadFile;
        private string _largeBucket;
        private string _uploadLargeFile;

        private string _endpoint;
        private string _accessKey;
        private string _secretKey;

        public string Bucket
        {
            get => _bucket;
            set
            {
                if (_bucket != value)
                {
                    _bucket = value;
                    notifyPropertyChanged(nameof(Bucket));
                }
            }
        }

        public string UploadFile
        {
            get => _uploadFile;
            set
            {
                if (_uploadFile != value)
                {
                    _uploadFile = value;
                    notifyPropertyChanged(nameof(UploadFile));
                }
            }
        }

        public string LargeBucket
        {
            get => _largeBucket;
            set
            {
                if (_largeBucket != value)
                {
                    _largeBucket = value;
                    notifyPropertyChanged(nameof(LargeBucket));
                }
            }
        }

        public string UploadLargeFile
        {
            get => _uploadLargeFile;
            set
            {
                if (_uploadLargeFile != value)
                {
                    _uploadLargeFile = value;
                    notifyPropertyChanged(nameof(UploadLargeFile));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            loadConfiguration();
        }

        private void loadConfiguration()
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            if (string.IsNullOrEmpty(appConfig["endpoint"]))
            {
                ConfigurationManager.AppSettings.Set("endpoint", "endpoint");
                MessageBox.Show(this, "Endpoint is not set in the App.Config", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            if (string.IsNullOrEmpty(appConfig["accessKey"]))
            {
                MessageBox.Show(this, "AccessKey is not set in the App.Config", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            if (string.IsNullOrEmpty(appConfig["secretKey"]))
            {
                MessageBox.Show(this, "SecretKey is not set in the App.Config", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            _endpoint = appConfig["endpoint"];
            _accessKey = appConfig["accessKey"];
            _secretKey = appConfig["secretKey"];
        }

        private async void uploadBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bucket: {Bucket}");
            sb.AppendLine($"File: {UploadFile}");
            statusTxtBlk.Text = sb.ToString();
            var ret = await UploadFileAsync();
            if (ret)
            {
                statusTxtBlk.Text = "Upload Successfully!";
            }
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                UploadFile = openFileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void notifyPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async Task<bool> UploadFileAsync()
        {
            var credentials = new BasicAWSCredentials(_accessKey, _secretKey);

            var clientConfig = new AmazonS3Config
            {
                ForcePathStyle = true,
                ServiceURL = _endpoint,
            };

            bool ret = true;
            using (var client = new AmazonS3Client(credentials, clientConfig)) 
            {
                try
                {
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = _bucket,
                        FilePath = UploadFile
                    };
                    var response = await client.PutObjectAsync(putRequest);
                }
                catch(FileNotFoundException e)
                {
                    ret = false;
                    this.Dispatcher.Invoke(new Action(() => this.statusTxtBlk.Text = e.Message));
                }
                catch (AmazonS3Exception e)
                {
                    ret = false;
                    if (e.ErrorCode != null &&
                        (e.ErrorCode.Equals("InvalidAccessKeyId") ||
                    e.ErrorCode.Equals("InvalidSecurity")))
                    {
                        this.Dispatcher.Invoke(new Action(() => this.statusTxtBlk.Text = "Please check the provided AWS Credentials"));
                    } else
                    {
                        this.Dispatcher.Invoke(new Action(() => this.statusTxtBlk.Text = $"An error occurred with the message '{e.Message}' when writing an object"));
                    }
                }   
            }
            return ret;
        }

        private async Task<bool> UploadLargeFileAsync()
        {
            var credentials = new BasicAWSCredentials(_accessKey, _secretKey);

            var clientConfig = new AmazonS3Config
            {
                ForcePathStyle = true,
                ServiceURL = _endpoint,
            };

            bool ret = true;
            using (var client = new AmazonS3Client(credentials, clientConfig))
            {


                try
                {
                    var fileTransferUtility = new TransferUtility(client);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = LargeBucket,
                        FilePath = UploadLargeFile,
                        Key = System.IO.Path.GetFileName(UploadLargeFile)
                    };

                    uploadRequest.UploadProgressEvent += UploadRequest_UploadProgressEvent;

                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
                catch (FileNotFoundException e)
                {
                    ret = false;
                    this.Dispatcher.Invoke(new Action(() => this.statusLargeTxtBlk.Text = e.Message));
                }
                catch (AmazonS3Exception e)
                {
                    ret = false;
                    if (e.ErrorCode != null &&
                        (e.ErrorCode.Equals("InvalidAccessKeyId") ||
                    e.ErrorCode.Equals("InvalidSecurity")))
                    {
                        this.Dispatcher.Invoke(new Action(() => this.statusLargeTxtBlk.Text = "Please check the provided AWS Credentials"));
                    }
                    else
                    {
                        this.Dispatcher.Invoke(new Action(() => this.statusLargeTxtBlk.Text = $"An error occurred with the message '{e.Message}' when writing an object"));
                    }
                }
                catch(Exception e)
                {
                    this.Dispatcher.Invoke(new Action(() => this.statusLargeTxtBlk.Text = $"An error occurred with the message '{e.Message}' when writing an object"));
                }
            }
            return ret;
        }

        private void UploadRequest_UploadProgressEvent(object? sender, UploadProgressArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.uploadProgress.Value = e.TransferredBytes * 100 / e.TotalBytes ;
            }));
        }

        private async void uploadLargeBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bucket: {LargeBucket}");
            sb.AppendLine($"File: {UploadLargeFile}");
            statusLargeTxtBlk.Text = sb.ToString();
            var ret = await UploadLargeFileAsync();
            if (ret)
            {
                statusLargeTxtBlk.Text = "Upload Successfully!";
            }
        }

        private void browseLargeBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                UploadLargeFile = openFileDialog.FileName;
            }
        }
    }
}
