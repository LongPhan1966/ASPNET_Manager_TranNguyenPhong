using SV18T1021172.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021172.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductDAL 
    {
        //product
        /// <summary>
        /// tìm kiếm và lọc phân trang sản phẩm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="supplierID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int supplierID = 0, int categoryID = 0);
        /// <summary>
        /// đếm số lượng sản phẩm hiển thị theo kết quả tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="supplierID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        int Count(string searchValue, int supplierID, int categoryID);
        /// <summary>
        /// Lấy thông tin chi tiết của 1 sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Product Get(int productID);
        /// <summary>
        /// Thêm mới sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        bool Delete(int productID);
        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// Kiểm tra sản phẩm có liên kết không
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        bool InUsed(int productID);


        //ProductPhoto
        /// <summary>
        /// Hiển thị danh sách ảnh của sản ph
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        IList<ProductPhoto> ListPhotos(int productID);
        /// <summary>
        /// lấy thông tin chi tiết ảnh của sản phẩm
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        ProductPhoto GetPhoto(int photoID);
        /// <summary>
        /// thêm ảnh sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int AddPhoto(ProductPhoto data);
        /// <summary>
        /// Cập nhật ảnh sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdatePhoto(ProductPhoto data);
        /// <summary>
        /// Xóa ảnh của sản phẩm
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        bool DeletePhoto(int photoID);

        //ProductAttribute
        /// <summary>
        /// Hiển thị danh sách các thuộc tính của sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        IList<ProductAttribute> ListAttributes(int productID);
        /// <summary>
        /// Lấy thông tin chi tiết thuộc tính của sản phẩm
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(int attributeID);
        /// <summary>
        /// Thêm mới thuộc tính của sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int AddAttribute(ProductAttribute data);
        /// <summary>
        /// Cập nhật thông tin thuộc tính của sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// Xóa thuộc tính sản phẩm
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        bool DeleteAttribute(int attributeID);

    }
}
