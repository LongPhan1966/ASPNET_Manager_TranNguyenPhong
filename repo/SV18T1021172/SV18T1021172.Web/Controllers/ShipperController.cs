using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021172.DomainModel;
using SV18T1021172.BusinessLayer;

namespace SV18T1021172.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// /// giám sát đã đăng nhập chưa 
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Shipper
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["SHIPPER_SEARCH"] as Models.PaginationSearchInput;

            if(model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 8,
                    SearchValue = ""
                };
            } 
            return View(model);
        }

        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;

            var data = BusinessLayer.CommonDataService.ListOfShipper(input.Page, input.PageSize, input.SearchValue, out rowCount);

            //model là 1 đối tượng có kiểu dữ liệu là BasePaginationResult
            Models.ShipperPaginationResult model = new Models.ShipperPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,

                //data là dữ liệu mang danh sách Người giao hàng (ListOfShipper)
                Data = data
            };
            Session["SHIPPER_SEARCH"] = input;

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng";
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        [Route("edit/{shipperID}")]
        public ActionResult Edit(string shipperID)
        {
            ViewBag.Title = "Chỉnh sửa thông tin người giao hàng";

            int id = 0;
            try
            {
                id = Convert.ToInt32(shipperID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Shipper model = CommonDataService.GetShipper(id);

            if (model == null)
                return RedirectToAction("Index");
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Save(Shipper model)
        {
            //Kiểm soát lỗi và bắt lỗi và đưa vào ModelState
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                //ModelState là tập hợp các thông báo lỗi
                ModelState.AddModelError("ShipperName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");

            //Nếu dữ liệu đầu vào không hợp lệ thì trả dữ liệu đầu vào và đưa ra thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.ShipperID > 0)
                    ViewBag.Title = "Cập nhật thông tin khách hàng";
                else
                    ViewBag.Title = "Bổ sung thông tin khách hàng";
                return View("Create", model);
            }

            if (model.ShipperID == 0)
            {
                CommonDataService.AddShipper(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateShipper(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(string shipperID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(shipperID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetShipper(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}