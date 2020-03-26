using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Net.Http;


namespace Personal_Blog.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //过滤器
        public ActionResult Index()
        {
            if (Session["userid"]==null) {
                //未登录
                return Redirect("/Admin/Login/");
            }
            return View();
        }

        public ActionResult Top()
        {
            return View();
        }
        public ActionResult Left()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        /// <summary>
        /// layui编辑器的上传图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ImgUpload()
        {


            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase file = files[0];
            //获取文件名后缀
            string extName = Path.GetExtension(file.FileName).ToLower();
            #region 判断后缀
            if (!extName.Contains("jpg") && !extName.Contains("png") && !extName.Contains("gif"))
            {
                return Json(new { code = 1, msg = "只允许上传jpg,png,gif格式的图片.", });
            }
            #endregion
            #region 判断大小
            long mb = file.ContentLength / 1024 / 1024; // MB
            if (mb > 5)
            {
                return Json(new { code = 1, msg = "只允许上传小于 5MB 的图片.", });
            }
            #endregion
            //获取保存目录的物理路径 
            var path = Server.MapPath("/upload/"); //path为某个文件夹的绝对路径，不要直接保存到数据库
                                                   //    string path = "E:E:\VS2019\个人博客\Personal Blog\Personal Blog.Web\upload";
            string dir = DateTime.Now.ToString("yyyyMMdd");
            string fileNewName = Guid.NewGuid().ToString();
            var filename = dir + "_" + Guid.NewGuid().ToString().Substring(0, 6) + extName;
            string physic_Path = path + $"{Path.DirectorySeparatorChar}upload{Path.DirectorySeparatorChar}{dir}{Path.DirectorySeparatorChar}";
            if (System.IO.Directory.Exists(physic_Path))//如果不存在就创建images文件夹
            {
                System.IO.Directory.CreateDirectory(physic_Path);
            }
            var uploadPath = physic_Path + filename;
            using (FileStream fs = System.IO.File.Create(uploadPath))
            {
                file.SaveAs(uploadPath);
                fs.Flush();
            }
            return Json(new { code = 0, msg = "上传成功", data = new { src = $"{ Path.DirectorySeparatorChar}upload{ Path.DirectorySeparatorChar} { dir}{ Path.DirectorySeparatorChar} " } });
        }



    }
}
