using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    interface IEmployeePayment
    {
        int GetID();
        bool Update(EmployeePayment item);
        bool isDelete(int ID);
    }
}
