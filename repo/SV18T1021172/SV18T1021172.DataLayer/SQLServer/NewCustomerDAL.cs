using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021172.DomainModel;

namespace SV18T1021172.DataLayer.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class NewCustomerDAL : _BaseDAL, ICommonDAL<Customer>
    {
        public NewCustomerDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Customer data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer data)
        {
            throw new NotImplementedException();
        }
    }
}
