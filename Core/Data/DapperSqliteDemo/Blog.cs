using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSqliteDemo
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return $"User Id={Id}, Title={Title}, Description={Description}";
        }
    }
}
