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
    }
}
