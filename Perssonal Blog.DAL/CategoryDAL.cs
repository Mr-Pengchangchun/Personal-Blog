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
      
        public int Insert(Model.Blog m)
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

        
    }
}
