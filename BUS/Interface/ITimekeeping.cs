using PlasticsFactory.Data;
using System;
using System.Collections.Generic;

namespace BUS.Interface
{
    interface ITimekeeping
    {
        List<string> GetNameEmployee();

        string GetNameEmployee(string MSNV);

        List<string> GetIdByName(string Name);

        List<TypeWeight> GetWeight();

        bool checkNameEmployee(string Name);

        bool IsEmployeeByDateDB(string MSNV, string Date,string timeStart);

        int GetIdByMSNVDate(string MSNV, DateTime date);
    }
}