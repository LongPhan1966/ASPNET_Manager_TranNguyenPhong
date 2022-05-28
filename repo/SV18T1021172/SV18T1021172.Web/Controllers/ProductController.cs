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
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["PRODUCT_SEARCH"] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.ProductSearchInput()
                {
                    Page = 1,
                    PageSize = 8,
                    SearchValue = "",
                    CategoryName = "0",
                    SupplierName = "0"
                };
            }
            return View(model);
        }

        public ActionResult Search(Models.ProductSearchInput input)
        {
            int rowCount = 0;
            //TODO: sửa input.Category input.SupplierID
            var data = ProductDataService.ListOfProducts(input.Page, input.PageSize, input.SearchValue, Convert.ToInt32(input.CategoryName), Convert.ToInt32(input.SupplierName), out rowCount);
            //model là 1 đối tượng có kiểu dữ liệu là BasePaginationResult
            Models.ProductPaginationResult model = new Models.ProductPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };
            Session["PRODUCT_SEARCH"] = input;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung mặt hàng";

            Product model = new Product()
            {
                ProductID = 0
            };

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(string productID, string attributeID, string photoID)
        {
            ViewBag.Title = "Chỉnh sửa thông tin mặt hàng";

            int id = 0;
            int aID = 0;
            int pID = 0;
            try
            {
                id = Convert.ToInt32(productID);
                aID = Convert.ToInt32(attributeID);
                pID = Convert.ToInt32(photoID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            Product model = ProductDataService.GetProduct(id);
            ProductDataService.DeleteProductAttribute(aID);
            ProductDataService.DeleteProductPhoto(pID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("delete/{productID}")]
        public ActionResult Delete(string productID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }

            var model = ProductDataService.GetProduct(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Tên sản phẩm không được để trống");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính của sản phẩm không được để trống");
            if (string.IsNullOrWhiteSpace(model.Price))
                ModelState.AddModelError("Price", "Giá sản phẩm không được để trống");
            if (string.IsNullOrWhiteSpace(model.SupplierID.ToString()))
                ModelState.AddModelError("SupplierID", "Nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(model.CategoryID.ToString()))
                ModelState.AddModelError("CategoryID", "Loại sản phẩm không được để trống");
            if (string.IsNullOrWhiteSpace(model.Photo))
                model.Photo = "";
            try
            {
                int price = Convert.ToInt32(model.Price);
            }
            catch
            {
                ModelState.AddModelError("Price", "Giá sản phẩm không phải là 1 chuỗi");
            }

            //Xử lý ảnh
            if (uploadPhoto != null)
            {
                string physicalPath = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";

                string filePath = System.IO.Path.Combine(physicalPath, fileName);
                uploadPhoto.SaveAs(filePath);

                model.Photo = $"Images/Products/{fileName}";
            }

            if (!ModelState.IsValid)
            {
                if (model.ProductID > 0)
                    return View("Edit", model);
                else
                    return View("Create", model);
            }

            if (model.ProductID == 0)
            {
                ProductDataService.AddProduct(model);
                return RedirectToAction("Index");
            }
            else
            {
                ProductDataService.UpdateProduct(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, int productID, int? photoID)
        {
            if (photoID != null)
            {
                ProductPhoto model = ProductDataService.GetProductPhoto(Convert.ToInt32(photoID));
                switch (method)
                {

                    case "edit":
                        ViewBag.Title = "Thay đổi ảnh";
                        break;
                    case "delete":
                        return RedirectToAction("Edit", new { productID = productID, photoID = photoID });
                    default:
                        return RedirectToAction("Index");
                }

                return View(model);
            }
            else
            {
                ProductPhoto model = new ProductPhoto()
                {
                    PhotoID = 0,
                    ProductID = productID
                };
                switch (method)
                {
                    case "add":
                        ViewBag.Title = "Bổ sung ảnh";
                        break;

                    default:
                        return RedirectToAction("Index");
                }

                return View(model);
            }
        }
        
        /// <summary>
        /// xử lý lưu ảnh
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase uploadPhoto)
        {
            //Xử lý ảnh
            if (uploadPhoto != null)
            {
                string physicalPath = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";

                string filePath = System.IO.Path.Combine(physicalPath, fileName);
                uploadPhoto.SaveAs(filePath);

                model.Photo = $"Images/Products/{fileName}";
            }
            if (string.IsNullOrWhiteSpace(model.Photo))
            {
                ModelState.AddModelError("Photo", "Ảnh không được để trống");
            }


            if (string.IsNullOrWhiteSpace(model.Description))
            {
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            }

            if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự không được để trống");
            }

            if (!ModelState.IsValid)
            {

                return View("Photo", model);
            }
            if (model.PhotoID == 0)
            {
                ProductDataService.AddProductPhoto(model);
                
            }
            else
            {
                ProductDataService.UpdateProductPhoto(model);
                
            }
            return RedirectToAction("Edit", new { productID = model.ProductID });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            if (attributeID != null)
            {
                ProductAttribute model = ProductDataService.GetProductAttribute(Convert.ToInt32(attributeID));
                switch (method)
                {

                    case "edit":
                        ViewBag.Title = "Thay đổi thuộc tính";
                        break;
                    case "delete":
                        return RedirectToAction("Edit", new { productID = productID, attributeID = attributeID });
                    default:
                        return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                ProductAttribute model = new ProductAttribute()
                {
                    AttributeID = Convert.ToInt32(attributeID),
                    ProductID = productID
                };
                switch (method)
                {
                    case "add":
                        ViewBag.Title = "Bổ sung thuộc tính";
                        break;

                    default:
                        return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        /// <summary>
        /// xử lý lưu thuộc tính
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute model)
        {
            if (string.IsNullOrWhiteSpace(model.ProductID.ToString()))
            {
                ModelState.AddModelError("ProductID", "Tên không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.AttributeName))
            {
                ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.AttributeValue))
            {
                ModelState.AddModelError("AttributeValue", "Giá trị không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự không được để trống");
            }

            if (!ModelState.IsValid)
            {

                return View("Attribute", model);
            }

            if (model.AttributeID == 0)
            {
                ProductDataService.AddProductAttribute(model);
                
            }
            else
            {
                ProductDataService.UpdateProductAttribute(model);
                
            }
            return RedirectToAction("Edit", new { productID = model.ProductID });
        }
    }
}