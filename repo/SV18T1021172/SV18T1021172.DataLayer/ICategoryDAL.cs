using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021172.DomainModel;

namespace SV18T1021172.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu liên quan đến loại hàng
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Lấy danh sách các loại hàng
        /// </summary>
        /// <returns></returns>
        IList<Category> List(int page, int paseSize, string searchValue);

        /// <summary>
        /// Lấy thông tin 1 loại hàng dựa vào mã loại hàng
        /// </summary>
        /// <param name="categoryID">Mã loại hàng cần lấy</param>
        /// <returns></returns>
        Category Get(int categoryID);

        /// <summary>
        /// đếm loại hàng thỏa điều kiện cần tìm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Bổ sung 1 loại hàng mới. hàm trả về mã của 1 loại hàng bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);

        /// <summary>
        /// Cập nhật thông tin của 1 loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);


        /// <summary>
        /// Xóa một loại hàng dựa vào mã loại hàng.
        /// Lưu Ý : không được xóa nếu loại hàng đã được sử dụng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int categoryID);

        bool IsUsed(int categoryID);
    }
}
