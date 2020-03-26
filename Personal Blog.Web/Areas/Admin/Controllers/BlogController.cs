using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace Personal_Blog.Web.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        DAL.BlogDAL dal = new DAL.BlogDAL();
        DAL.CategoryDAL cadal = new DAL.CategoryDAL();
        // GET: Admin/Blog
        public ActionResult Index()
        {
            ViewBag.calist = cadal.GetList("");
            return View();
           // List<Model.Blog> list = dal.GetList("");
            //return View(list);
        }
        /// <summary>
        /// 取博客总记录数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTotalCount(string key, string start, string end, string canum)
        {
            int totalcount = dal.CalcCount(GetCond(key, start, end, canum));
            return Content(totalcount.ToString());
        }
        /// <summary>
        /// 拼接条件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="canum"></param>
        /// <returns></returns>
        public string GetCond(string key, string start, string end, string canum)
        {

            string cond = "1=1";
            if (!string.IsNullOrEmpty(key))
            {
                key = Tool.GetSafeSQL(key);
                cond += $" and title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(start))
            {
                DateTime d;
                if (DateTime.TryParse(start, out d))
                {
                    cond += $" and createdate>='{d.ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(end))
            {
                DateTime d;
                if (DateTime.TryParse(end, out d))
                {
                    cond += $" and createdate<='{d.ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(canum))
            {
                canum = Tool.GetSafeSQL(canum);
                cond += $" and canumber='{canum}'";
            }
            return cond;
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
        public ActionResult List(int pageindex, int pagesize, string key, string start, string end, string canum)
        {
            List<Model.Blog> list = dal.GetList("sort asc,id desc", pagesize, pageindex,GetCond(key,start,end,canum));
            ArrayList arr = new ArrayList();
            foreach (var item in list)
            {
                arr.Add(new
                {
                    id = item.Id,
                    title = item.Title,
                  //  updatetime = item..ToString("yyyy-MM-dd HH:mm"),
                    createDate = item.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                    visitNum = item.VistitNum,
                    caName = item.CaName,
                    sort = item.Sort,

                });
            }
            return Json(arr);
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
        [ValidateInput(false)]
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool b= dal.Delete(id);
            if (b)
            {
                return Content("删除成功！");
            }
            else
            {
                return Content("删除失败,请联系管理员！");
            }
          
        }
    }
}