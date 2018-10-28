using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class CustomerBO : Responsity<Customer>, ICustomer
    {
        public string GetID()
        {
            var list = GetData(u => u.isDelete == false)
                    .ToList();
            object obj = list.FirstOrDefault();
            int ID = 0;
            if (obj != null)
            {
                ID = list.Last().ID + 1;
            }
            return "KH"+ID.ToString("d6");
        }

        public List<int> GetIDByName(string Name)
        {
            return GetData(u => u.isDelete == false && u.Name == Name).Select(u=>u.ID).ToList();
        }

        public int GetIDByType(string type)
        {
            using (var db = new PlasticFactoryEntities())
            {
                var list = db.TypeofCustomers.ToList();
                int ID = list.First(u => u.isDelete == false && u.Type == type).ID;
                return ID;
            }
        }

        public string GetNameByID(int ID)
        {
            var list = GetData(u => u.ID == ID && u.isDelete == false);
            return list.Single().Name;
        }

        public List<string> GetTypeofCustomer()
        {
            using (var db = new PlasticFactoryEntities())
            {
                List<string> listDB = db.TypeofCustomers
                    .AsNoTracking()
                    .Where(u => u.isDelete == false)
                    .Select(u => u.Type)
                    .ToList();
                return listDB;
            }
        }

        public bool isDelete(int id)
        {
            using (var db = new PlasticFactoryEntities())
            {
                try
                {
                    var dbSet = db.Customers;
                    Customer obj = dbSet.Find(id);
                    obj.isDelete = true;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        
        public int GetIDByTypeOfCurrently(string Type)
        {
            using (var db = new PlasticFactoryEntities())
            {
                var list = db.TypeofCustomers.ToList();
                int ID = list.Last(u => u.isDelete == false && u.Type == Type).ID;
                return ID+1;
            }
        }
    }
}
