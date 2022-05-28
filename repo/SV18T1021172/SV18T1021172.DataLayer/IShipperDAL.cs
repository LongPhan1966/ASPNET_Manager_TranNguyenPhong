using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021172.DomainModel;

namespace SV18T1021172.DataLayer
{
    public interface IShipperDAL
    {
        /// <summary>
        /// Tìn kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        IList<Shipper> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm nhà cung cấp thỏa điều kiện cần tìm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin của 1 shipper
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        Shipper Get(int shipperID);

        /// <summary>
        /// Bổ sung thông tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);
        /// <summary>
        /// Chỉnh sửa thông tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);

        /// <summary>
        /// Xóa nhà cung cấp theo ID
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        bool Delete(int shipperID);

        /// <summary>
        /// Kiểm tra xem nhà cung cấp đã có mặt hàng chưa
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        bool InUsed(int shipperID);
    }
}
