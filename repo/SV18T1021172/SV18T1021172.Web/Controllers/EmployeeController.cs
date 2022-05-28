using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021172.DomainModel;
using SV18T1021172.BusinessLayer;
using System.Globalization;

namespace SV18T1021172.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// /// giám sát đã đăng nhập chưa 
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["EMPLOYEE_SEARCH"] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 6,
                    SearchValue = ""
                };
            }
            return View(model);
        }

        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;

            var data = BusinessLayer.CommonDataService.ListOfEmployees(input.Page, input.PageSize, input.SearchValue, out rowCount);

            //model là 1 đối tượng có kiểu dữ liệu là BasePaginationResult
            Models.EmployeePaginationResult model = new Models.EmployeePaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,

                //data là dữ liệu mang danh sách Nhân viên (ListOfEmployees)
                Data = data
            };
            Session["EMPLOYEE_SEARCH"] = input;

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";

            Employee model = new Employee()
            {
                EmployeeID = 0
            };

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// tham số có dấu chấm hỏi nghĩa là không cần phải nhập
        [Route("edit/{employeeID?}")]
        public ActionResult Edit(string employeeID)
        {
            ViewBag.Title = "Chỉnh sửa thông tin nhân viên";

            int id = 0;
            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Employee model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");

            return View("Create", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [Route("delete/{employeeID?}")]
        public ActionResult Delete(string employeeID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Employee model, string dateOfBirth, HttpPostedFileBase uploadPhoto)
        {
            //Kiểm soát lỗi và bắt lỗi và đưa vào ModelState
            if (string.IsNullOrWhiteSpace(model.LastName))
                //ModelState là tập hợp các thông báo lỗi
                ModelState.AddModelError("LastName", "Họ nhân viên không được để trống");
            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(model.Photo))
                model.Photo = "";
            if (string.IsNullOrWhiteSpace(model.Notes))
                model.Notes = "";

            // Chuyển dateOfBirth (dd/MM/yyyy) sang giá trị kiểu ngày
            try
            {
                model.BirthDate = DateTime.ParseExact(dateOfBirth, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                ModelState.AddModelError("BirthDate", $"Ngày sinh {dateOfBirth} không đúng");
            }

            if (model.BirthDate.Year < 1753)
                ModelState.AddModelError("Birdate", "Năm sinh không thấp hơn năm 1753");

            //xử lý ảnh
            if (uploadPhoto != null)
            {
                string physicalPath = Server.MapPath("~/Images/Employees");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";

                //ghép 2 chuỗi thành 1
                string filePath = System.IO.Path.Combine(physicalPath, fileName);
                uploadPhoto.SaveAs(filePath);

                model.Photo = $"Images/Employees/{fileName}";
            }


            //Nếu dữ liệu đầu vào không hợp lệ thì trả dữ liệu đầu vào và đưa ra thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.EmployeeID > 0)
                    ViewBag.Title = "Cập nhật thông nhân viên";
                else
                    ViewBag.Title = "Bổ sung thông nhân viên";
                return View("Create", model);
            }

            if (model.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateEmployee(model);
                return RedirectToAction("Index");
            }


        }
    }
}