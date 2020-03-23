using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
namespace Personal_Blog.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 取数据库连接
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <returns></returns>string connStr 
        public static string ConnectionString
          {
              get { return ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString; }
          }
    }
}
