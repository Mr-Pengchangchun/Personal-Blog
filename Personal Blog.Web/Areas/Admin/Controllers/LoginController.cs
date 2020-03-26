using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Personal_Blog.DAL;
using Microsoft.Net.Http;

namespace Personal_Blog.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        DAL.AdminDAL adal=new AdminDAL();
        public LoginController()
        {
        }
        public LoginController(DAL.AdminDAL adal)
        {
            this.adal = adal;
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (adal==null)
            {
                return Content("<script> alert('对象未实例化！');location.href='/Admin/Login/'</script>");
            }
            else
            {
            username = Tool.GetSafeSQL(username);
            password = Tool.GetSafeSQL(password);
             Model.Admin a = adal.Login(username, password);
                //Model.Admin a = new DAL.AdminDAL().Login(username, password);
            if (a == null)
            {
                return Content("<script> alert('登录失败，用户名或密码错误！');location.href='/Admin/Login/'</script>");
            }
                Session["userid"] = a.UserName;
                Session["password"] = a.PassWord;
                return Redirect("/Admin/Home/Index");
            }
            
        }
        
    }
}