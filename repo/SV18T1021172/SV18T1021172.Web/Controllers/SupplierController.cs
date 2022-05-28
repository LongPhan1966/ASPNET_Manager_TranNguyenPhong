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
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        // GET: Supplier
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["SUPPLIER_SEARCH"] as Models.PaginationSearchInput;
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

            var data = BusinessLayer.CommonDataService.ListOfSuppliers(input.Page, input.PageSize, input.SearchValue, out rowCount);

            //model là 1 đối tượng có kiểu dữ liệu là BasePaginationResult
            Models.SupplierPaginationResult model = new Models.SupplierPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,

                //data là dữ liệu mang danh sách Nhà cung cấp (ListOfSuplier)
                Data = data
            };
            Session["SUPPLIER_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp";

            Supplier model = new Supplier()
            {
                SupplierID = 0
            };

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("edit/{supplierID}")]
        public ActionResult Edit(string supplierID)
        {
            ViewBag.Title = "Chỉnh sửa thông tin nhà cung cấp";

            int id = 0;
            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Supplier model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");

            return View("Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(string supplierID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(supplierID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Supplier model)
        {
            //Kiểm soát lỗi và bắt lỗi và đưa vào ModelState
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                //ModelState là tập hợp các thông báo lỗi
                ModelState.AddModelError("SupplierName", "Tên nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Tên quốc gia không được để trống");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");


            //Nếu dữ liệu đầu vào không hợp lệ thì trả dữ liệu đầu vào và đưa ra thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.SupplierID > 0)
                    ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
                else
                    ViewBag.Title = "Bổ sung thông tin nhà cung cấp";
                return View("Create", model);
            }

            if (model.SupplierID == 0)
            {
                CommonDataService.AddSupplier(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateSupplier(model);
                return RedirectToAction("Index");
            }
        }
    }
}