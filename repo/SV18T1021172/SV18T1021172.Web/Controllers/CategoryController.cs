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
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Category
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["CATEGORY_SEARCH"] as Models.PaginationSearchInput;
            if (model == null)
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

            var data = BusinessLayer.CommonDataService.ListOfCategories(input.Page, input.PageSize, input.SearchValue, out rowCount);

            //model là 1 đối tượng có kiểu dữ liệu là BasePaginationResult
            Models.CategoryPaginationResult model = new Models.CategoryPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,

                //data là dữ liệu mang danh sách Nhà cung cấp (ListOfSuplier)
                Data = data
            };
            Session["CATEGORY_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";

            Category model = new Category()
            {
                CategoryID = 0
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("edit/{categoryID}")]
        public ActionResult Edit(string categoryID)
        {
            ViewBag.Title = "Chỉnh sửa loại hàng";

            int id = 0;
            try
            {
                id = Convert.ToInt32(categoryID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Category model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");

            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("delete/{categoryID}")]
        public ActionResult Delete(string categoryID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(categoryID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }


        [HttpPost]
        public ActionResult Save(Category model)
        {
            //Kiểm soát lỗi và bắt lỗi và đưa vào ModelState
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                //ModelState là tập hợp các thông báo lỗi
                ModelState.AddModelError("CategoryName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Description))
                model.Description = "";

            //Nếu dữ liệu đầu vào không hợp lệ thì trả dữ liệu đầu vào và đưa ra thông báo lỗi
            if (!ModelState.IsValid)
            {
                if (model.CategoryID > 0)
                    ViewBag.Title = "Cập nhật thông tin loại hàng";
                else
                    ViewBag.Title = "Bổ sung thông tin loại hàng";
                return View("Create", model);
            }

            if (model.CategoryID == 0)
            {
                CommonDataService.AddCategory(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateCategory(model);
                return RedirectToAction("Index");
            }
        }
    }
}