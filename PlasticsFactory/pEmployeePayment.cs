using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticsFactory
{
    public class pEmployeePayment
    {
        public int ID { get; set; }
        public DateTime DATE { get; set; }
        public string EmployeeName { get; set; }
        public string MSNV { get; set; }
        public double PAY { get; set; }
        public double NEBT { get; set; }
        public bool isDelete { get; set; }
        public int ProductPrice { get; set; }
        public int TimePrice { get; set; }
        public double Wage { get; set; }
        public double Cash { get; set; }
        public bool isPayed { get; set; }
        public int MonthOfPay { get; set; }
        public int YearOfPay { get; set; }
    }
}
