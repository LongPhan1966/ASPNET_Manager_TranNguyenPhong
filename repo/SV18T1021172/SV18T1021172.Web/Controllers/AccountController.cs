using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021172.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// giám sát đã đăng nhập chưa 
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        ///  trả về view Đăng nhập
        /// </summary>
        /// <returns></returns>
        /// tất cả các action đều được authorize riêng chức năng được set allowAnonymous
        [AllowAnonymous]
        //nếu không để phương thức HttpGet thì hàm này tiếp nhận cả 2 phương thức get và post
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// xử lý đăng nhặp
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //TODO: code lại để kiểm tra đúng tài khoản có trong database
            if(username == "admin@abc.com" && password == "1")
            {
                //Ghi lại cookie phiên đăng nhập
                //không ghi lại username trên cookie
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                //Xác nhận đã đăng nhập

                return RedirectToAction("Index","Home");
            }
            
            ViewBag.UserName = username;
            ViewBag.Message = "Đặng nhập thất bại";
            return View();
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}