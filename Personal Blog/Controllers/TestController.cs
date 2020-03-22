using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Personal_Blog.DAL;
using Personal_Blog.Model;

namespace Personal_Blog.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            string str = "";
            DAL.CategoryDAL caadl = new CategoryDAL();
            str = "新增的分类返回的ID值" + caadl.Insert(new Model.Category() { CaName="new name"});
            return Content(str);
            
        }
    }
}