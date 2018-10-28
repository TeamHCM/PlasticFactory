using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    interface ITruck
    {
        int GetIDByNameLicence(string Name);
        bool isExistLicencePlate(string Name);
        List<string> GetLicencePlateByIDofCustomer(int MSKH);
    }
}
