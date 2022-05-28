using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021172.DomainModel;
using SV18T1021172.DataLayer;
using SV18T1021172.BusinessLayer;

namespace SV18T1021172.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// giám sát đã đăng nhập chưa 
        [Authorize]
        public ActionResult Index()
        {
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

        //public ActionResult Categories()
        //{
        //    var model = BusinessLayer.CommonDataService.ListOfCategories();
        //    return View(model);
        //}
    }
}