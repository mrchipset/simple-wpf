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

namespace DapperSqliteDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlogService blogService;
        public MainWindow()
        {
            InitializeComponent();
            blogService = new BlogService();
        }

        private void BtnCreateMockData_Click(object sender, RoutedEventArgs e)
        {
            bool ret = blogService.CreateMockData();
            if(!ret)
            {
                MessageBox.Show("Create Mock Data Error.");
            } else
            {
                MessageBox.Show("Create Mock Data Successfully.");
            }
        }

        private void BtnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            bool ret = blogService.ClearMockData();
            if (!ret)
            {
                MessageBox.Show("Clear Mock Data Error.");
            }
            else
            {
                MessageBox.Show("Clear Mock Data Successfully.");
            }
        }

        private void BtnFetchData_Click(object sender, RoutedEventArgs e)
        {
            Blog blog = blogService.QuerySingleBlogByTitle("Blog 1");
            if (blog == null)
            {
                BlkContent.Text = "No Data";
            } else
            {
                BlkContent.Text = blog.ToString();
            }
        }

        private void BtnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            Blog blog = blogService.QuerySingleBlogByTitle("Blog 1");
            if (blog == null)
            {
                BlkContent.Text = "No Data";
            }
            else
            {
                blog.Title = "Blog new";
                blog.Description = "New description";
                blogService.UpdateSingleBlog(blog);
            }
        }

        private void BtnFetchAll_Click(object sender, RoutedEventArgs e)
        {
            List<Blog> blogs = blogService.QueryAll();
            StringBuilder builder = new StringBuilder();
            foreach (Blog blog in blogs)
            {
                builder.Append(blog.ToString());
                builder.AppendLine();
            }
            string content = builder.ToString();
            if (content == string.Empty)
            {
                BlkContent.Text = "No Data";
            } else
            {
                BlkContent.Text = content;
            }
        }
    }
}