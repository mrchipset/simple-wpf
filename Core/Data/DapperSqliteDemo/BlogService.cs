using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSqliteDemo
{
    public class BlogService
    {
        private SqliteConnection connection;
        private string connectionString = "Data Source=database.db";

        public BlogService()
        {
            connection = new SqliteConnection(connectionString);
        }
        public bool CreateMockData()
        {
            // create table;
            string createTable = "CREATE TABLE IF NOT EXISTS Blog (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Title TEXT," +
                "Description TEXT" +
                ");";

            int row = connection.Execute(createTable);
            
            // insert query
            string insertQuery = "INSERT INTO Blog (Title, Description) VALUES (@Title, @Description);";
            List<Blog> blogs = new List<Blog>
            {
                new Blog()
                {
                    Title = "Blog 1",
                    Description = "Description for blog1"
                },
                new Blog()
                {
                    Title = "Blog 2",
                    Description = "Description for blog2"
                }
            };

            int insertRowAffect = connection.Execute(insertQuery, blogs);
            return true;
        }

        public bool ClearMockData()
        {
            string deleteQuery = "DELETE FROM Blog WHERE 1=1;";
            connection.Execute(deleteQuery);
            return true;
        }

        public Blog QuerySingleBlogByTitle(string title)
        {
            string queryString = "SELECT * FROM Blog WHERE Title = @Title";
            Blog? blog = connection.QueryFirstOrDefault<Blog>(queryString, new { Title = title });
            return blog;
        }

        public bool UpdateSingleBlog(Blog blog)
        {
            string updateQuery = "UPDATE Blog SET Title=@Title, Description=@Description WHERE Id =@Id";
            connection.Execute(updateQuery, blog);
            return true;
        }

        public List<Blog> QueryAll()
        {
            string query = "SELECT * FROM Blog;";
            return connection.Query<Blog>(query).ToList();
        }
    }
}
