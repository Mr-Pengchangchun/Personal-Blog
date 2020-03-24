using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Personal_Blog.DAL;
using Personal_Blog.Model;

namespace Personal_Blog.Web.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            string str = "";
            Random r = new Random();
            DAL.BlogDAL blo = new BlogDAL();
            
            DAL.CategoryDAL call = new CategoryDAL();
            List<Model.Category> list_ca = call.GetList("");
            for (int i = 0; i < 102; i++)
            {
                string title = $"新闻标题";
                string body = title + "的内容";
                Model.Category ca = list_ca[r.Next(0, list_ca.Count)];
                string cabh = ca.Number, caname = ca.CaName;
                blo.Insert( new Model.Blog { Title=title,Body=body,VistitNum=r.Next(100,9999),CaNumber=cabh,CaName=caname});
            }
            str += "添加102条测试新闻成功！";
            //str += "新增分类" + call.Insert(new Model.Blog {CaName="newCaName" })+ "<hr />";
            //bool b = call.Delete(7);
            //str+= "删除Id-7的数据：" + b + "<hr />";
            //Model.Category ca = call.GetModel(5);
            //if (ca!=null)
            //{
            //    ca.CaName = "新修改的名称" + DateTime.Now;
            //    bool b2 = call.Update(ca);
            //    str+="修改后的数据"+b2+ "<hr />";
            //}
            return Content(str);
        }
    }
}