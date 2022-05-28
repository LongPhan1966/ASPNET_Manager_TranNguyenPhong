using SV18T1021172.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021172.DataLayer.FakeDB
{
    /// <summary>
    /// cài đặt chức năng xử lý dữ liệu trên loại hàng theo kiểu "fake"
    /// </summary>
    public class CategoryDAL : ICommonDAL<Category>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int categoryID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Category> List()
        {
            //tạo mới danh sách
            List<Category> data = new List<Category>();


            //gán giá trị
            data.Add(new Category()
            {
                CategoryID = 1,
                CategoryName = "Nước hoa",
                Description = "thơm mãi ngàn năm"
            });
            data.Add(new Category()
            {
                CategoryID = 2,
                CategoryName = "bia",
                Description = "đàn ông"
            });

            return data;
        }

        public IList<Category> List(int page, int paseSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
