using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class ProductOBO:Responsity<ProductOP>,IProductO
    {
        public int GetPriceByName(string Name)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    var list = db.ProductOPs;
                    int ID = list.First(u => u.Name == Name && u.isDelete == false).Price.Value;
                    return ID;
                }
            }
            catch
            {
                return 0;
            }
        }
        public int GetID()
        {
            try
            {
                var list = GetData(u => u.isDelete == false);
                int ID = list.Last().ID;
                return ID;
            }
            catch {
                return 0;
            }
        }
    }
}
