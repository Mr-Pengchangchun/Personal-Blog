using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personal_Blog.Web.Controllers
{
   /// <summary>
   /// 前台博客控制器
   /// </summary>
    public class BlogController : Controller
    {
        DAL.BlogDAL dal = new DAL.BlogDAL();
        DAL.CategoryDAL cadal = new DAL.CategoryDAL();

        public BlogController()
        {

        }
        public BlogController(DAL.BlogDAL bdal, DAL.CategoryDAL cadal)
        {
            this.dal = bdal;
            this.cadal = cadal;
        }

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取博客总记录数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTotalCount(string key, string month, string cabh)
        {
            int totalcount = dal.CalcCount(GetCond(key, month, cabh));
            return Content(totalcount.ToString());
        }
        /// <summary>
        /// 取博客总记录数
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 取分页数据，返回json
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ActionResult List(int pageindex, int pagesize, string key, string month, string cabh)
        {
            List<Model.Blog> list = dal.GetList("sort asc,id desc", pagesize, pageindex, GetCond(key, month, cabh));
            ArrayList arr = new ArrayList();
            foreach (var item in list)
            {
                arr.Add(new
                {
                    id = item.Id,
                    title = item.Title,
                    //  updatetime = item..ToString("yyyy-MM-dd HH:mm"),
                    createDate = item.CreateDate.ToString("yyyy-MM-dd HH:mm"),   
                    caName = item.CaName,
                    desc=Tool.StringTruncat(Tool.ReplaceHtmlTag( item.Body),60,"....."),//取出HTML标签，再取出60个字符
                });
            }
            return Json(arr);
        }
        /// <summary>
        /// 拼接条件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="canum"></param>
        /// <returns></returns>
        public string GetCond(string key, string month, string cabh)
        {

            string cond = "1=1";
            if (!string.IsNullOrEmpty(key))
            {
                key = Tool.GetSafeSQL(key);
                cond += $" and title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(month))
            {
                DateTime d;
                if (DateTime.TryParse(month+"01", out d))
                {
                    cond += $" and createdate>='{d.ToString("yyyy-MM-dd")}' and createdate<'{d.AddMonths(1).ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(cabh))
            {
                DateTime d;
                if (DateTime.TryParse(cabh, out d))
                {
                    cond += $" and createdate<='{d.ToString("yyyy-MM-dd")}'";
                }
            }
   
            return cond;
        }
        public ActionResult Show(int id)
        {
            ViewBag.blogdal = dal;

          //  ViewBag.calist = cadal.GetIndexLeft_Ca();
            Model.Blog b = dal.GetModel(id);
            if (b==null)
            {
                return Content("找不到该博客");
            }
            return View(b);
        }
    }
}