using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021172.BusinessLayer;
using SV18T1021172.DomainModel;


namespace SV18T1021172.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// /// giám sát đã đăng nhập chưa 
    [Authorize]
    //quy định cấu trúc url
    [RoutePrefix("customer")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Customer
        public ActionResult Index()
        {
            //ép kiểu và lệnh ép kiểu này không lỗi nếu không thể ép được và hiện thị giá trị null
            Models.PaginationSearchInput model = Session["CUSTOMER_SEARCH"] as Models.PaginationSearchInput;
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

            var data = BusinessLayer.CommonDataService.ListOfCustomers(input.Page, input.PageSize, input.SearchValue, out rowCount);

            //model là 1 đối tượng có kiểu dữ liệu là BasePaginationResult
            Models.CustomerPaginationResult model = new Models.CustomerPaginationResult
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,

                //data là dữ liệu mang danh sách khách hàng (ListOfCustomers)
                Data = data
            };

            //session : giá trị giữ lại phiên làm việc
            // lưu lại điều kiện tìm kiếm
            Session["CUSTOMER_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";

            Customer model = new Customer()
            {
                CustomerID = 0
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// mô tả cấu trúc đường dẫn kèm theo tham số trong ngoặc {}
        /// nếu cần thêm tham số thì sẽ /{ten biến}
        [Route("edit/{customerID}")]
        public ActionResult Edit(string customerID)
        {
            ViewBag.Title = "Chỉnh sửa thông tin khách hàng";

            int id = 0;
            try
            {
                id = Convert.ToInt32(customerID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Customer model = CommonDataService.GetCustomer(id);
            if (model == null)
                return RedirectToAction("Index");

            //chỉ định view action trả về nếu không thì trả về view của action
            return View("Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// 
        [Route("delete/{customerID}")]
        public ActionResult Delete(string customerID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(customerID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }    

            var model = CommonDataService.GetCustomer(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Customer model)
        {
            //Kiểm soát lỗi và bắt lỗi và đưa vào ModelState
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                //ModelState là tập hợp các thông báo lỗi
                ModelState.AddModelError("CustomerName", "Tên không được để trống");
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

            //Nếu dữ liệu đầu vào không hợp lệ thì trả dữ liệu đầu vào và đưa ra thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.CustomerID > 0)
                    ViewBag.Title = "Cập nhật thông tin khách hàng";
                else
                    ViewBag.Title = "Bổ sung thông tin khách hàng";
                return View("Create", model);
            }

            if(model.CustomerID == 0)
            {
                CommonDataService.AddCustomer(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateCustomer(model);
                return RedirectToAction("Index");
            }
        }
    }
}