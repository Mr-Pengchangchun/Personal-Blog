using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personal_Blog.Model;
using System.Data.SqlClient;



namespace Personal_Blog.DAL
{/// <summary>
 /// 分类表的数据库操作类
 /// </summary>
    public class CategoryDAL
    {
        public string ConnStr { set; get; }
        //public string ConnStr { set; get; }  
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
      
        public int Insert(Model.Category m)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                int resid = connection.Query<int>(
                    @"INSERT INTO Category(CaName,Number,Pbh,Remark) values(@CaName,@Number,@Pbh,@Remark);SELECT @@IDENTITY;",
                    m).First();
                return resid;
            }
        }
        /// <summary>
        /// 取所有分类的数据拼接成json字符返回
        /// </summary>
        /// <returns></returns>
        public string GetTreeJson()
        {
            List<Model.TreeNode_LayUi> list_return = new List<TreeNode_LayUi>();
            List<Model.Category> list = GetList("");
            var top = list.Where(a => a.Pbh == "0");
            foreach (var item in top)
            {
                Model.TreeNode_LayUi node = new TreeNode_LayUi() { id = item.Id, name = item.CaName, spread = true,pid=0, };
                var sub = list.Where(a => a.Pbh == item.Number);
                List<Model.TreeNode_LayUi> list_sub = new List<TreeNode_LayUi>();
                foreach (var item2 in sub)
                {
                    Model.TreeNode_LayUi node2 = new TreeNode_LayUi() { id = item2.Id, name = item2.CaName, spread = true, pid=item.Id};
                    list_sub.Add(node2);
                }
                node.children = list_sub;

                list_return.Add(node);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list_return);
        }
        /// <summary>
        /// 根据编号取实体
        /// </summary>
        /// <param name="caNumber"></param>
        /// <returns></returns>
        public Category GetModelByNumber(string caNumber)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                var m = connection.Query<Model.Category>("select * from category where Number=@Number",

                   new { Number=caNumber }).FirstOrDefault();
                return m;
            }
        }

        /// <summary>
        /// 根据分类编号取实体类
        /// </summary>
        /// <param name="caBh"></param>
        /// <returns></returns>
        public Category GetModelByBh(string caBh)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {

                var m = connection.Query<Model.Category>("select * from category where Number=@Number",

                  new { Number = caBh }).FirstOrDefault();
                return m;
            }
        }

        /// 
        /// </summary>
        /// <param name="pbh">父编号</param>
        /// <param name="x">每一级编号的位数</param>
        /// <returns></returns>
        public string GenBH(string pbh, int x)
        {
            string sql = "select right(max(number)," + x + ") from category where pbh=" + pbh;
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                string res = connection.ExecuteScalar<string>(sql);
                if (string.IsNullOrEmpty(res))
                {
                    int a = 1;
                    if (pbh != "0")
                    {
                        return pbh + a.ToString("d" + x);
                    }
                    return a.ToString("d" + x);
                }
                else
                {
                    int a = int.Parse(res) + 1;
                    int b = (int)Math.Pow(10, x);
                    if (a >= b)
                    {
                        throw new Exception("编号超过限制!");
                    }
                    if (pbh != "0")
                    {
                        return pbh + a.ToString("d" + x);
                    }
                    return a.ToString("d" + x);
                }

            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
               int res= connection.Execute(@"delete from Category where id=@id",new { id=id});
                if (res>0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<Model.Category> GetList(string cond)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                string sql = "select *from Category";
                if (!string.IsNullOrEmpty(cond))
                {
                    sql += $"where{cond}";
                }
                var list = connection.Query<Model.Category>(sql).ToList();
                return list;
            }
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Category GetModel(int id)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                var m = connection.Query<Model.Category>("select * from category where id = @id",

                   new { id = id }).FirstOrDefault();
                return m;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool Update(Model.Category m)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                int res = connection.Execute(@"UPDATE [category]
                                               SET  [CaName] = @CaName
                                                   ,[Number] = @Number
                                                   ,[Pbh] = @Pbh
                                                   ,[Remark] = @Remark
                                                WHERE Id=@Id", m);

                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public int CalcCount(string cond)
        {
            string sql = "select count(1) from category";
            if (!string.IsNullOrEmpty(cond))
            {
                sql += " where " + cond;
            }
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                int i = connection.QuerySingle<int>(sql);
                return i;
            }
        }

    }
}
