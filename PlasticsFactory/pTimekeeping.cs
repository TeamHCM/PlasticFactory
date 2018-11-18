using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticsFactory
{
    public class pTimekeeping
    {
        public string MSNV { get; set; }
        public DateTime Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public double Overtime { get; set; }
        public double Time { get; set; }
        public int Weight { get; set; }
        public int Type { get; set; }
        public double TotalWeight { get; set; }
        public double AdvancePayment { get; set; }
        public string Note { get; set; }
        public int Id { get; set; }
        public bool isDelete { get; set; }
        public double Food { get; set; }
        public double Punish { get; set; }
        public double Bunus { get; set; }
        public string isRest { get; set; }
    }
}