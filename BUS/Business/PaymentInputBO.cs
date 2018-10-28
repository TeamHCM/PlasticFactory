using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class PaymentInputBO : Responsity<PaymentInput>, IPaymentInput
    {
        public int GetID()
        {
            int count = GetData(u => u.isDelete == false||u.isDelete==true).ToList().Count();
            if(count==0)
            {
                return 1;
            }
            return GetData(u => u.isDelete == false || u.isDelete == true).ToList().Last().ID+1;
        }

        public bool isDelete(int ID)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    var obj = db.PaymentInputs.Find(ID);
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
