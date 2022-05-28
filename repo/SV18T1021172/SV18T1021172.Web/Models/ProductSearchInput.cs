using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021172.Web.Models
{
    public class ProductSearchInput : PaginationSearchInput
    {
        //TODO: đôi ID thành name
        //public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        //public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}