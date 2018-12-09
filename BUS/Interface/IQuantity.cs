﻿using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    interface IQuantity:IResponsity<Quantity>
    {
        bool Update(Quantity quantity);
    }
}
