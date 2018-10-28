using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    interface IProductInput
    {
        List<int> GetIDByCustomerName(string Name);
        int GetID();
        bool isDelete(int ID);
    }
}
