using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog.Model
{
    /// <summary>
    /// 博客表
    /// </summary>
   public class Blog
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 内容_markdown
        /// </summary>
        public string Body_md { get; set; }
        /// <summary>
        /// 访问量
        /// </summary>
        public int VistitNum { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public string CaNumber { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CaName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }
    }
}
