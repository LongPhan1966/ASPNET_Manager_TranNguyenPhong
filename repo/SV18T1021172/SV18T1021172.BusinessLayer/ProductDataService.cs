using SV18T1021172.DataLayer.SQLServer;
using SV18T1021172.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021172.BusinessLayer
{
    public class ProductDataService
    {
        private static readonly ProductDAL productDB;

        static ProductDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            if (provider == "SQLServer")
            {
                productDB = new DataLayer.SQLServer.ProductDAL(connectionString);
            }
        }

        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, int categoryID, int supplierID, out int rowCount)
        {
            rowCount = productDB.Count(searchValue, supplierID, categoryID);
            return productDB.List(page, pageSize, searchValue, supplierID, categoryID).ToList();
        }
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        public static bool DeleteProduct(int productID)
        {
            if (productDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }
        public static List<ProductPhoto> ListOfProductPhotos(int productID)
        {
            return productDB.ListPhotos(productID).ToList();
        }
        public static ProductPhoto GetProductPhoto(int photoID)
        {
            return productDB.GetPhoto(photoID);
        }
        public static int AddProductPhoto(ProductPhoto data)
        {
            return productDB.AddPhoto(data);
        }
        public static bool UpdateProductPhoto(ProductPhoto data)
        {
            return productDB.UpdatePhoto(data);
        }
        public static bool DeleteProductPhoto(int photoID)
        {
            return productDB.DeletePhoto(photoID);
        }
        public static List<ProductAttribute> ListOfProductAttributes(int productID)
        {
            return productDB.ListAttributes(productID).ToList();
        }
        public static ProductAttribute GetProductAttribute(int attributeID)
        {
            return productDB.GetAttribute(attributeID);
        }
        public static int AddProductAttribute(ProductAttribute data)
        {
            return productDB.AddAttribute(data);
        }
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productDB.UpdateAttribute(data);
        }
        public static bool DeleteProductAttribute(int attributeID)
        {

            return productDB.DeleteAttribute(attributeID);
        }
    }
}
