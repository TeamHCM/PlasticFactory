using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class EmployeePaymentBO : Responsity<EmployeePayment>, IEmployeePayment
    {
        public int GetID()
        {
            int count=GetData(u => u.isDelete == false || u.isDelete == true).Count();
            if(count==0)
            {
                sqlQuery("DBCC CHECKIDENT (‘EmployeePayment’, RESEED, 1)");
                return 1;
            }
            else
            {
               return GetData(u => u.isDelete == false || u.isDelete == true).Last().ID+1;
            }
        }

        public bool Update(EmployeePayment item)
        {
            using (var db = new PlasticFactoryEntities())
            {
                try
                {
                    EmployeePayment employeePayment = db.EmployeePayments.Find(item.ID);
                    employeePayment.PAY = item.PAY;
                    employeePayment.DATE = item.DATE.Value;
                    db.SaveChanges();
                    return true;
                }
                catch {
                    throw;
                        }
            }
        }

        public bool isDelete(int ID)
        {
            using (var db = new PlasticFactoryEntities())
            {
                try
                {
                    EmployeePayment employeePayment = db.EmployeePayments.Find(ID);
                    employeePayment.isDelete = true;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }

        public bool isExist(string MSNV,int month,int year)
        {
            int count=GetData(u => u.isDelete == false && u.MSNV == MSNV && u.MonthOfPay == month && u.YearOfPay == year).Count();
            if(count!=0)
            {
                return true;
            }
            return false;
        }
    }
}
