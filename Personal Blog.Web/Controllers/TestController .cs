﻿using System;
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
            DAL.CategoryDAL call = new CategoryDAL();
            str += "新增分类" + call.Insert(new Model.Blog {CaName="newCaName" })+ "<hr />";
            bool b = call.Delete(7);
            str+= "删除Id-7的数据：" + b + "<hr />";
            Model.Category ca = call.GetModel(5);
            if (ca!=null)
            {
                ca.CaName = "新修改的名称" + DateTime.Now;
                bool b2 = call.Update(ca);
                str+="修改后的数据"+b2+ "<hr />";
            }
            return Content(str);
        }
    }
}