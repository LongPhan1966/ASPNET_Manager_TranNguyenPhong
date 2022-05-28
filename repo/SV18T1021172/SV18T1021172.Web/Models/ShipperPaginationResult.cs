using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021172.DomainModel;

namespace SV18T1021172.Web.Models
{
    public class ShipperPaginationResult : BasePaginationResult
    {
        public List<Shipper> Data { get; set; }
    }
}