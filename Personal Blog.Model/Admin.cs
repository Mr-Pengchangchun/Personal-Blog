using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog.Model
{
    /// <summary>
    /// 管理员表
    /// </summary>
   public class Admin
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Remark { get; set; }
    }
}
