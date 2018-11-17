using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class ProductOutputBO:Responsity<ProductOutput>,IProductOutput
    {
        public int GetID()
        {
            sqlQuery("DBCC CHECKIDENT ('ProductOutput', RESEED, 1)");
            using (var db = new PlasticFactoryEntities())
            {
                var list = db.ProductOutputs.ToList();
                if (list.Count() != 0)
                {
                    return list.Last().ID + 1;
                }
                return 1;
            }
        }

        public List<int> GetIDByCustomerName(string Name)
        {
            using (var db = new PlasticFactoryEntities())
            {
                var dbSet = db.Customers;
                List<int> list = dbSet.AsNoTracking()
                    .Where(u => u.Name == Name)
                    .Select(u => u.ID)
                    .ToList();
                return list;
            }
        }

        public bool isDelete(int ID)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    var obj = db.ProductOutputs.Find(ID);
                    obj.isDelete = true;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
