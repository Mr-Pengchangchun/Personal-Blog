using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personal_Blog.Web.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        DAL.BlogDAL dal = new DAL.BlogDAL();
        DAL.CategoryDAL cadal = new DAL.CategoryDAL();
        // GET: Admin/Blog
        public ActionResult Index()
        {
            List<Model.Blog> list = dal.GetList("");
            return View(list);
        }
        public ActionResult Add(int?id)
        {
            ViewBag.calist = cadal.GetList("");
            Model.Blog m = new Model.Blog();
            if (id!=null)
            {
                m = dal.GetModel(id.Value);
            }
            return View(m);     
        }
        [HttpPost]
        public ActionResult Add(Model.Blog m)
        {
            Model.Category ca = cadal.GetModelByNumber(m.CaNumber);
            if (ca!=null)
            {
                m.CaName = ca.CaName;
            }
            if (m.Id==0)
            {
                //新增
                dal.Insert(m);
            }
            else
            {
                //修改
                dal.Update(m);
            }
            return Redirect("/Admin/Blog/Index");
        }
        public ActionResult Delete(int id)
        {
            dal.Delete(id);
            return Redirect("/Admin/Blog/Index");
        }
    }
}