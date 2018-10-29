using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlasticsFactory.Data;
namespace BUS.Interface
{
    interface IEmployee
    {
        string AutoGetMSNV();
        string GetNameByID(string ID);
    }
}
