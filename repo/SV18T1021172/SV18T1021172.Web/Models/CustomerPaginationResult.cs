using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021172.DomainModel;

namespace SV18T1021172.Web.Models
{
    /// <summary>
    /// Lưu kết quả tìm kiếm phân trang cho khách hàng
    /// </summary>
    public class CustomerPaginationResult : BasePaginationResult
    {

        public List<Customer> Data { get; set; }
    }
}