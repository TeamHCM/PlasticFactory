using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    interface IProduct
    {
        int GetPriceByName(string Name);
        int GetID();
        int GetIDByName(string Name);
        bool isExistName(string Name);
    }
}
