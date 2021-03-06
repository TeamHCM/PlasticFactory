﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlasticsFactory.Data;
using BUS.Interface;
using System.Linq.Expressions;

namespace BUS.Business
{
    public class EmployeeBO : Responsity<Employee>,IEmployee
    {
        public string AutoGetMSNV()
        {
            using (var db = new PlasticFactoryEntities())
            {
                return db.AutoIdEmployee().FirstOrDefault();
            }
        }
        public string GetNameByID(string ID)
        {
            return GetData(u => u.MSNV == ID && (u.isDelete==false || u.isDelete==true)).First().Hoten;
        }
        public bool isDelete(string ID)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    Employee employee=db.Employees.Find(ID);
                    employee.isDelete = true;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
    }
    public class EmployeeComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            if(x==null||y==null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                int tempX = int.Parse((x.MSNV.Split('V'))[1]);
                int tempY = int.Parse((y.MSNV.Split('V'))[1]);
                if(tempX>tempY)
                {
                    return 1;
                }
                if(tempX<tempY)
                {
                    return -1;
                }
                if(tempX==tempY)
                {
                    return 0;
                }
                return 0;
            }
        }
    }
}
