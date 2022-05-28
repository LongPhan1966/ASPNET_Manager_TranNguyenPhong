using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SV18T1021172.DataLayer;
using SV18T1021172.DomainModel;

namespace SV18T1021172.BusinessLayer
{
    /// <summary>
    /// Cung cấp các chức năng xử lý dữ liệu chung
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Country> countryDB;
        private static readonly ICommonDAL<Product> productDB;
        private static readonly ICommonDAL<ProductPhoto> productPhotoDB;
        private static readonly ICommonDAL<ProductAttribute> productAttributeDB;
        /// <summary>
        /// contructor
        /// </summary>
        static CommonDataService()
        {
            //đọc cấu hình
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            if(provider == "SQLServer")
            {

                categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                supplierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                shipperDB = new DataLayer.SQLServer.ShipperDAL(connectionString);
                countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                //productDB = new DataLayer.SQLServer.IProductDAL(connectionString);
                productPhotoDB = new DataLayer.SQLServer.ProductPhotoDAL(connectionString);
                productAttributeDB = new DataLayer.SQLServer.ProductAttributeDAL(connectionString);
            }
            else
            {
                categoryDB = new DataLayer.FakeDB.CategoryDAL();
            }
        }


        /// <summary>
        /// Lấy danh sách các mặt hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize">Số dòng mỗi trang (nếu pagesize > page count thì chuyển về trang cuối))</param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (pageSize < 0)
                pageSize = 0;

            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        ///  Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        
        /// <summary>
        /// Tìm kiếm và lấy danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShipper(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        
        ///// <summary>
        ///// tìm kiếm và lấy danh sách sản phẩm dưới dạng phân trang
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="searchValue"></param>
        ///// <param name="rowCount"></param>
        ///// <returns></returns>
        //public static List<Product> ListOfProduct(int page, int pageSize, string searchValue, out int rowCount)
        //{
        //    rowCount = productDB.Count(searchValue);
        //    return productDB.List(page, pageSize, searchValue).ToList();
        //}

        ///// <summary>
        ///// Hiển thị danh sách các ảnh của sản phẩm
        ///// </summary>
        ///// <param name="searchValue"></param>
        ///// <returns></returns>
        //public static List<ProductPhoto> ListOfProductPhotos(string searchValue)
        //{
        //    return productPhotoDB.List(1, 0, searchValue).ToList();
        //}

        ///// <summary>
        ///// hiển thị danh sách chi tiết sản phầm
        ///// </summary>
        ///// <param name="searchValue"></param>
        ///// <returns></returns>
        //public static List<ProductAttribute> ListOfProductAttributes(string searchValue)
        //{
        //    return productAttributeDB.List(1, 0, searchValue).ToList();
        //}

        ///// <summary>
        ///// Hiển thị list các quốc gia
        ///// </summary>
        ///// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }

        /// <summary>
        /// hiển thị danh sách các nhà sản xuất
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> SelectListOfSupplier()
        {
            return supplierDB.List().ToList();
        }
        /// <summary>
        /// Hiển thị danh sách các loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> SelectListOfCategory()
        {
            return categoryDB.List().ToList();
        }
        ///// <summary>
        ///// Hiển thị danh sách ảnh sản phẩm
        ///// </summary>
        ///// <returns></returns>
        //public static List<ProductPhoto> ListOfProductPhotos()
        //{
        //    return productPhotoDB.List().ToList();
        //}
        ///// <summary>
        ///// hiển thị attribute sản phẩm
        ///// </summary>
        ///// <returns></returns>
        //public static List<ProductAttribute> ListOfProductAttributes()
        //{
        //    return productAttributeDB.List().ToList();
        //}

        //public static List<Supplier> ListOfSupplier()
        //{
        //    return productDB.List().ToList();
        //}

        //-----------------------Customer-------------------------
        /// <summary>
        /// lấy ra thông tin cụ thể của 1 khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// xóa khách hàng với điều kiện kiểm tra có hóa đơn hay không
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Kiểm tra khách hàng đã có hóa đơn chưa
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        //--------------------END Customer-------------------------




        //-----------------------Category--------------------------
        /// <summary>
        /// lấy ra thông tin cụ thể của 1 loai hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }

        /// <summary>
        /// Thêm loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// Cập nhật loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// Xóa loại hàng thỏa mãn điều kiện có sản phẩm hay không
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }

        /// <summary>
        /// Kiểm tra loại hàng có sản phẩm hay không
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        //--------------------END Category-------------------------




        //---------------------Employee----------------------------
        /// <summary>
        /// Lấy ra thông tin cụ thể của nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        /// <summary>
        /// thêm nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// Xóa nhân viên thỏa mãn điều kiện
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }

        /// <summary>
        /// điều kiện để xóa nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        //-----------------END Employee----------------------------




        //---------------------Supplier----------------------------
        /// <summary>
        /// lấy cchi tiết thông tin nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// thêm thông tin của nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// Xóa nhà cung cấp thỏa mãn điều kiện
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// Điều kiện kiểm tra xem nhà cung cấp có sản phẩm hay không
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        //-----------------End Supplier----------------------------




        //---------------------Shipper----------------------------
        /// <summary>
        /// lấy cchi tiết thông tin người giao hàng
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// Thêm thông tin người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa thông tin người giao hàng có điều kiện
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// Kiểm tra người giao hàng có hóa đơn nào không
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }

        //-----------------End Shipper----------------------------


        //-----------------Product -------------------------------
        ///// <summary>
        ///// Lấy thông tin chi tiết của sản phẩm
        ///// </summary>
        ///// <param name="ProductID"></param>
        ///// <returns></returns>
        //public static Product GetProduct(int productID)
        //{
        //    return productDB.Get(productID);
        //}
        ///// <summary>
        ///// Thêm thông tin sản phẩm
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static int AddProduct(Product data)
        //{
        //    return productDB.Add(data);
        //} 
        ///// <summary>
        ///// Cập nhật thông tin sản phẩm
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static bool UpdateProduct(Product data)
        //{
        //    return productDB.Update(data);
        //}
        ///// <summary>
        ///// kiểm tra trước khi xóa
        ///// </summary>
        ///// <param name="productID"></param>
        ///// <returns></returns>
        //public static bool DeleteProduct(int productID)
        //{
        //    if (productDB.InUsed(productID))
        //        return false;
        //    return productDB.Delete(productID);
        //}
        ///// <summary>
        ///// kiểm tra điều kiện
        ///// </summary>
        ///// <param name="productID"></param>
        ///// <returns></returns>
        //public static bool InUsedProduct(int productID)
        //{
        //    return productDB.InUsed(productID);
        //}
        ////-----------------End Product ---------------------------


        ////-----------------Product Photo-------------------------------
        //public static ProductPhoto GetProductPhoto(int productID)
        //{
        //    return productPhotoDB.Get(productID);
        //}
        ///// <summary>
        ///// Thêm ảnh sản phẩm
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static int AddProductPhoto(ProductPhoto data)
        //{
        //    return productPhotoDB.Add(data);
        //}
        ///// <summary>
        ///// cập nhật ảnh sản phẩm
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static bool UpdateProductPhoto(ProductPhoto data)
        //{
        //    return productPhotoDB.Update(data);
        //}
        ///// <summary>
        ///// xóa ảnh sản phẩm
        ///// </summary>
        ///// <param name="photoID"></param>
        ///// <returns></returns>
        //public static bool DeleteProductPhoto(int photoID)
        //{
        //    return productPhotoDB.Delete(photoID);
        //}
        ////-----------------End Product Photo-------------------------------


        ////-----------------Product Attribute-------------------------------
        //public static ProductAttribute GetProductAttribute(int attributeID)
        //{
        //    return productAttributeDB.Get(attributeID);
        //}
        ///// <summary>
        ///// Thêm ảnh sản phẩm
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static int AddProductAttribute(ProductAttribute data)
        //{
        //    return productAttributeDB.Add(data);
        //}
        ///// <summary>
        ///// cập nhật ảnh sản phẩm
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static bool UpdateProductAttribute(ProductAttribute data)
        //{
        //    return productAttributeDB.Update(data);
        //}
        ///// <summary>
        ///// xóa ảnh sản phẩm
        ///// </summary>
        ///// <param name="photoID"></param>
        ///// <returns></returns>
        //public static bool DeleteProductAttribute(int attributeID)
        //{
        //    return productAttributeDB.Delete(attributeID);
        //}
        ////-----------------End Product Attribute-------------------------------
    }
}
