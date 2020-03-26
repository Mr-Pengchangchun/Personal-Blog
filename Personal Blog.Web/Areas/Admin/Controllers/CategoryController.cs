using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personal_Blog.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        DAL.CategoryDAL dal = new DAL.CategoryDAL();
        // GET: Admin/Category
        public ActionResult Index()
        {
            #region 节点数据json
            ViewBag.nodejson = dal.GetTreeJson();
            #endregion
            ViewBag.calist = dal.GetList("");
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool b = dal.Delete(id);
            if (b)
            {
                return Content("删除成功！");
            }
            else
            {
                return Content("删除失败,请联系管理员！");
            }

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="caname"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(int pid, string caname)
        {
            caname = Tool.GetSafeSQL(caname);//字符过滤
            string pbh = "0";
            if (pid != 0)
            {
                Model.Category pca = dal.GetModel(pid);
                if (pca != null)
                {
                    if (pca.Pbh!="0")
                    {
                        return Json(new { status = "n", info = "最多只能添加二级分类" });
                    }
                    pbh = pca.Number;
                    if (dal.CalcCount($"pbh='{pca.Number}' and caname='{caname}'") > 0)
                    {
                        return Json(new { status = "n", info = "已有同名分类，不允许添加" });
                    }
                }

            }
            else
            {
                if (dal.CalcCount($"pbh='0' and caname='{caname}'") > 0)
                {
                    return Json(new { status = "n", info = "已有同名分类，不允许添加" });
                }
            }
            string bh = dal.GenBH(pbh, 2);
            dal.Insert(new Model.Category() { CaName = caname, Pbh = pbh, Number = bh, });
            return Json(new { status = "y", info = "新增分类成功！" });
        }
        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="caname"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Mod(int pid, string caname,int id)
        {
            Model.Category ca = dal.GetModel(id);
            if (ca==null)
            {
                return Json(new { status = "n", info = "分类ID传入错误！" });
            }
            string bh = ca.Number;
            string pbh = ca.Pbh;
            int source_pid = ca.Pbh == "0" ? 0 : dal.GetModelByBh(ca.Pbh).Id;//分类原本的父级ID
            Model.Category pca = dal.GetModel(pid);
            if (pca!=null)
            {
                if (pca.Id!= source_pid)
                {
                    //修改父级分类，重新生成编号
                    pbh = pca.Number;
                    bh = dal.GenBH(pbh, 2);
                }

            }
            else if (pid == 0)
            {
                //设置为顶级分类
                pbh = "0";
                bh = dal.GenBH(pbh, 2);
            }
            ca.CaName = caname;
            ca.Number = bh;
            ca.Pbh = pbh;
          bool b=  dal.Update(ca);
            if (b)
            {
                return Json(new { status = "y", info = "分类修改成功！" });
            }
            else
            {
                return Json(new { status = "n", info = "分类修改失败！" });
            }

        }
    }
}