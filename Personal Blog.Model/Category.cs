using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog.Model
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class Category
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createdate { set; get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CaName { set; get; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public string Number { set; get; }
        /// <summary>
        /// 父编号
        /// </summary>
        public string Pbh { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
    }
}
