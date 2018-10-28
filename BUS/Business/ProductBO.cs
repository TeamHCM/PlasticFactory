using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class ProductBO : Responsity<ProductIP>, IProduct
    {
        public int GetPriceByName(string Name)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    var list = db.ProductIPs;
                    int ID = list.First(u => u.Name == Name && u.isDelete == false).Price.Value;
                    return ID;
                }
            }
            catch {
                return 0;
            }
        }
        public int GetID()
        {
            var list=GetData(u => u.isDelete == false);
            int ID = list.Last().ID;
            return ID;
        }
    }
}
