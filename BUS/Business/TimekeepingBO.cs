using BUS.Interface;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Business
{
    public class TimekeepingBO : Responsity<Timekeeping>, ITimekeeping
    {
        public List<string> GetNameEmployee()
        {
            using (var db = new PlasticFactoryEntities())
            {
                List<string> list;
                var select = db.Employees.Where(u=>u.isDelete==false).Select(u => u.Hoten);
                list = select.ToList();
                return list;
            }
        }
        public string GetNameEmployee(string MSNV)
        {
            using (var db = new PlasticFactoryEntities())
            {
                return db.Employees
                    .Where(u => u.MSNV == MSNV)
                    .Select(u => u.Hoten).FirstOrDefault();
            }
        }
        public List<string> GetIdByName(string Name)
        {
            using (var db = new PlasticFactoryEntities())
            {
                return db.Employees.
                    Where(u=>u.Hoten==Name).
                    Select(u => u.MSNV).
                    ToList();
            }
        }
        public bool checkNameEmployee(string name)
        {
            using (var db = new PlasticFactoryEntities())
            {
                object check = db.Employees.FirstOrDefault(u => u.Hoten == name);
                if(check!=null)
                {
                    return true;
                }
                return false;
            }
        }
        #region Pendding Time
        public static IEnumerable<int> Month(int day)
        {
            if (day <= 29)
            {
                return new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            }
            if (day<=30)
            {
                return new int[11] { 1,3,4,5,6,7,8, 9, 10,11,12 };
            }
            if(day==31)
            {
                return new int[7] { 1, 3, 5, 7, 8, 10, 12 };
            }
         
            return new int[1] {0};
        }
        public static int Day(int month, int year)
        {
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                return 30;
            }
            if (month == 2)
            {
                if (year % 4 == 0 & year % 100 == 0 || year % 400 == 0)
                {
                    return 28;
                }
                return 29;
            }
            return 0;
        }
        public static IEnumerable<int> Year()
        {
            DateTime now = new DateTime();
            int Year = now.Year;
            for(int i=Year-10;i<=Year;i++)
            {
                yield return i;
            }
        }
        #endregion
        public List<TypeWeight> GetWeight()
        {
            using (var db = new PlasticFactoryEntities())
            {
                return db.TypeWeights.ToList();
            }
        }
        public bool IsEmployeeByDateDB(string MSNV, string Date)
        {
            using (var db = new PlasticFactoryEntities())
            {
                DateTime dateTime = DateTime.Parse(Date);
                int result = db.Timekeepings.Count(u => u.MSNV == MSNV && u.Date == dateTime);
                if(result==1)
                {
                    return true;
                }
                return false;
            }
        }
        public int GetIdByMSNVDate(string MSNV, DateTime date)
        {
            try
            {
                using (var db = new PlasticFactoryEntities())
                {
                    return db.Timekeepings.First(u => u.MSNV == MSNV && u.Date == date).Id;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
