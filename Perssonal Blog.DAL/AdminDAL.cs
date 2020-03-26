using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personal_Blog.Model;
using System.Data.SqlClient;
using Dapper;

namespace Personal_Blog.DAL
{
    public class AdminDAL
    {
        public Admin Login(string username, string password)
        {
            string sql = "select * from admin where username=@username and password=@password";
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                var m = connection.Query<Model.Admin>(sql,
                   new { username = username, password = password }).FirstOrDefault();
                return m;
            }
        }
    }
}
