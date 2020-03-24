using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using System.Data.Common;
using Personal_Blog.Model;
using System.Data.SqlClient;

namespace Personal_Blog.DAL
{

    public partial class BlogDAL
    {
        /// <summary>
        /// 数据库连接字符串，从web层传入
        /// </summary>
        //public string ConnStr { set; get; }
        /// <summary>增加一条数
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
                    @"INSERT INTO [dbo].[Blog]
                            ([Title]
                           ,[Body]
                           ,[Body_md]
                           ,[VistitNum]
                           ,[CaNumber]
                           ,[CaName]
                           ,[Remark]
                           ,[Sort])
                     VALUES
                           ( @Title
                           ,@Body
                           ,@Body_md
                           ,@VistitNum
                           ,@CaNumber
                           ,@CaName
                           ,@Remark
                           ,@Sort);SELECT @@IDENTITY;", m).First();
                return resid;
            }
        }
        /// <summary>
        /// 计算记录数
        /// </summary>
        /// <returns></returns>
        public int CalcCount(string cond)
        {
            string sql = "select count(1) from [blog]";
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

                   new { Number = caNumber }).FirstOrDefault();
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
                int res = connection.Execute(@"delete from Blog  where id=@id", new { id = id });
                if (res > 0)
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
        //public List<Model.Blog> GetList(string cond)
        //{
        //    using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
        //    {
        //        string sql = "select *from Blog";
        //        if (!string.IsNullOrEmpty(cond))
        //        {
        //            sql += $"where{cond}";
        //        }
        //        var list = connection.Query<Model.Blog>(sql).ToList();
        //        return list;
        //    }
        //}
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Blog GetModel(int id)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                var m = connection.Query<Model.Blog>("select * from Blog where id = @id",

                   new { id = id }).FirstOrDefault();
                return m;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool Update(Model.Blog m)
        {
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                int res = connection.Execute(@"UPDATE [dbo].[Blog]
                                        SET [Title] = @Title
                                        ,[Body_md] = @Body_md
                                         ,[VistitNum] = @VistitNum
                                         ,[CaNumber] = @CaNumber
                                         ,[CaName] = @CaName
                                         ,[Remark] = @Remark
                                         ,[Sort] = @Sort
                                          WHERE Id=@Id", m);
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
        ///分页，使用offset,mssql2012以后有用
        /// </summary> 
        /// <param name="orderstr">如：yydate desc,yytime asc,id desc,必须形成唯一性</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<Model.Blog> GetList(string orderstr, int PageSize, int PageIndex, string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " where " + strWhere;
            }
            string sql = string.Format(
                    "select * from [blog] {0} order by {1} offset {2} rows fetch next {3} rows only",
                    strWhere,
                    orderstr,
                    (PageIndex - 1) * PageSize,
                    PageSize
                );
            List<Model.Blog> list = new List<Model.Blog>();
            using (var connection = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                list = connection.Query<Model.Blog>(sql).ToList();
            }
            return list;
        }


    }
}


