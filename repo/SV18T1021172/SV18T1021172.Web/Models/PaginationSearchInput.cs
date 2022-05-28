using SV18T1021172.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021172.Web.Models
{
    /// <summary>
    /// Thông tin đầu vào tìm kiếm phân trang đơn giản
    /// </summary>
    public class PaginationSearchInput 
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }
    }
}