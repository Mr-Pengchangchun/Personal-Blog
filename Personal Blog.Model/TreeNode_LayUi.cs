using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Blog.Model
{
    /// <summary>
    /// layui的节点树形模型
    /// </summary>
    public class TreeNode_LayUi
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool spread { get; set; }
        public List<TreeNode_LayUi> children { get; set; }
        public int pid { get; set; }
    }
}
