using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021172.DomainModel;

namespace SV18T1021172.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// danh sach cac quoc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
