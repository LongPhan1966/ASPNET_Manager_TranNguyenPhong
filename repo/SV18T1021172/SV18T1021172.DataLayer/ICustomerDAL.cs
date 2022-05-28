using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021172.DomainModel;

namespace SV18T1021172.DataLayer
{
    /// <summary>
    /// Định nghĩa các phếp xử lý dữ liệu liên quan đến khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang dữ liệu
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="paseSize">Số dòng hiển thị tối đa</param>
        /// <param name="searchValue">
        ///     Giá trị tìm kiếm (Tên hoặc địa chỉ cần tìm, tìm kiếm tương đối)
        ///     nếu không thì hiển thị tất cả
        /// </param>
        /// <returns></returns>
        IList<Customer> List(int page, int paseSize, string searchValue);

        /// <summary>
        /// Đếm khách hàng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        /// 
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin của 1 khách hàng theo ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Customer Get(int customerID);

        /// <summary>
        /// Bổ sung thông tin khách hàng hàm trả về mã của khách hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);

        /// <summary>
        /// Xóa khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool Delete(int customerID);

        /// <summary>
        /// Kiểm tra xem khách hàng đã có sản phẩm chưa
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool InUsed(int customerID);
    }
}
