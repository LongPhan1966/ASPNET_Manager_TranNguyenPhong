using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021172.Web.Models
{
    /// <summary>
    /// Lớp cơ sở của các lớp dùng để chưa dữ liệu truy vấn
    /// dưới dạng phân trang, tìm kiếm
    /// </summary>
    public abstract class BasePaginationResult
    {
        /// <summary>
        /// Trang hiện tại (đang xem)
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// số dòng hiển thị trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Tổng số dòng
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }
        /// <summary>
        /// Giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
    }
}