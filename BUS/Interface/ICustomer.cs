using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    interface ICustomer
    {
        List<string> GetTypeofCustomer();
        int GetIDByType(string type);
        string GetID();
        bool isDelete(int ID);
        string GetNameByID(int ID);
        List<int> GetIDByName(string Name);
        int GetIDByTypeOfCurrently(string Type);
    }
}
