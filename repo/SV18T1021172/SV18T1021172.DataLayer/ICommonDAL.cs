using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021172.DataLayer
{
    /// <summary>
    /// khai báo 1 I tập trung sử lý 1 kiểu duwxlieeuj nào đó với T là 1 class
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="page">SỐ trang</param>
        /// <param name="pageSize">số dòng mỗi trang</param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        IList<T> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> List();

        /// <summary>
        /// sử lấy thông tin của 1 đối tượng T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// bổ sung 1 đối tượng t, hàm trả về Identity của dữ liệu được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Kiểm tra xem dữ liệu có liên quan hay không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
