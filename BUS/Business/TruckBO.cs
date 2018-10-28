using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class TruckBO:Responsity<Truck>,ITruck
    {
        public int GetIDByNameLicence(string Name)
        {
            using(var db=new PlasticFactoryEntities())
            {
                var list = db.Trucks;
                int ID = list.FirstOrDefault(u => u.LicencePlate == Name).ID;
                return ID;
            }
        }

        public List<string> GetLicencePlateByIDofCustomer(int MSKH)
        {
            List<string> list=GetData(u => u.isDelete == false && u.MSKH == MSKH).Select(u=>u.LicencePlate).ToList();
            return list;
        }

        public bool isExistLicencePlate(string Name)
        {
            using (var db = new PlasticFactoryEntities())
            {
                var list = db.Trucks;
                if (list.Count() == 0)
                {
                    return true;
                }
                else
                {
                    int count = list.Count(u => u.LicencePlate == Name);
                    if (count == 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
