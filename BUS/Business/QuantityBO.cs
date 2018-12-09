using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class QuantityBO:Responsity<Quantity>,IQuantity
    {
        public bool Update(Quantity quantity)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    Quantity obj = new Quantity();
                    DateTime date = quantity.Date.Value;
                    int ID = GetData(u => u.Date.Value.Day==date.Day&&u.Date.Value.Month==date.Month&&u.Date.Value.Year==date.Year).First().Id;
                    obj = db.Quantities.Find(ID);
                    obj.IsEdit = quantity.IsEdit;
                    obj.Note = quantity.Note;
                    obj.InventoryNotMonth = quantity.InventoryNotMonth;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateInventory(Quantity quantity)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    Quantity obj = new Quantity();
                    DateTime date = quantity.Date.Value;
                    int ID = GetData(u => u.Date.Value.Day == date.Day && u.Date.Value.Month == date.Month && u.Date.Value.Year == date.Year).First().Id;
                    obj = db.Quantities.Find(ID);
                    obj.TotalInventory = quantity.TotalInventory;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
