using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021172.DomainModel;

namespace SV18T1021172.DataLayer
{
    /// <summary>
    /// lớp định nghĩa các phép xử lý dữ liệu liên quan đến nhân viên
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Hiển thị và tìm kiếm nhân viên theo dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        IList<Employee> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// đếm số lượng nhân viên thỏa điều kiện cần tìm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Hiển thị nhân viên cụ thể theo ID
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        Employee Get(int EmployeeID);
        /// <summary>
        /// Thêm nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);

        /// <summary>
        /// Chỉnh sửa thông tin nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);

        /// <summary>
        /// Xóa nhân viên theo ID nhân viên
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool Delete(int EmployeeID);
        /// <summary>
        /// Kiểm tra ràng buộc của nhân viên là đơn hàng
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool IsUsed(int EmployeeID);
    }
}
