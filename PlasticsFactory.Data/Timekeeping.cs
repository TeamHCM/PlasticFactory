//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlasticsFactory.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timekeeping
    {
        public string MSNV { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public double Time { get; set; }
        public Nullable<int> Weight { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> TotalWeight { get; set; }
        public Nullable<double> AdvancePayment { get; set; }
        public string Note { get; set; }
        public int Id { get; set; }
        public bool isDelete { get; set; }
        public double Food { get; set; }
        public double Punish { get; set; }
        public double Bunus { get; set; }
        public bool isRest { get; set; }
    }
}
