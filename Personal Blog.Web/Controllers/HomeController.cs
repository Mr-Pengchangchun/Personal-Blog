using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personal_Blog.Web.Controllers
{
 
    public class HomeController : Controller
    {
       
        public ActionResult Index(string key,string month,string cabh)
        {
            ViewBag.calist = new DAL.CategoryDAL().GetList("");
            ViewBag.blogmonth = new DAL.BlogDAL().GetBlogMonth();
            ViewBag.search_key = key;
            ViewBag.search_month = month;
            ViewBag.search_cabh = cabh;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}