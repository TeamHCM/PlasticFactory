using BUS.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmPrintTimekeeping : Form
    {
        List<pTimekeeping> _list;
        EmployeePaymentBO employeePaymentBO = new EmployeePaymentBO();
        EmployeeBO EmployeeBO = new EmployeeBO();
        PreferceProductPriceBO preferceProductPriceBO = new PreferceProductPriceBO();
        string _msnv = "";
        public frmPrintTimekeeping(List<pTimekeeping> list, string msnv)
        {
            InitializeComponent();
            _list = list;
            _msnv = msnv;
        }

        private void frmPrintTimekeeping_Load(object sender, EventArgs e)
        {

            var listTT = _list;
            int month = listTT.First().Date.Month;
            int year = listTT.First().Date.Year;
            int Moneyproduct = preferceProductPriceBO.GetData(u => u.isDelete == false && u.ID == 1).First().Price.Value;
            int Moneytime = preferceProductPriceBO.GetData(u => u.isDelete == false && u.ID == 2).First().Price.Value;
            double Debt = 0;    
            if (month != 1)
            {
                int count = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv && u.MonthOfPay == month - 1 && u.YearOfPay == year).Count();
                if (count != 0)
                {
                    Debt = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv && u.MonthOfPay == month - 1 && u.YearOfPay == year).Single().NEBT;
                }
            }
            if (month == 1)
            {
                int count =employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv && u.MonthOfPay == 12 && u.YearOfPay == year - 1).Count;
                if (count != 0)
                {
                    Debt = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv && u.MonthOfPay == 12 && u.YearOfPay == year - 1).Single().NEBT;
                }
            }
            Double Pay = 0;
            printTimekeeping1.SetDataSource(_list);
            printTimekeeping1.SetParameterValue("pEmployeeName", EmployeeBO.GetData(u => u.isDelete == false && u.MSNV == _msnv).First().Hoten);
            printTimekeeping1.SetParameterValue("pTimekeeping", listTT.First().Date.Month + "/" + listTT.First().Date.Year);
            printTimekeeping1.SetParameterValue("pCash", listTT.Sum(u => u.AdvancePayment));
            printTimekeeping1.SetParameterValue("pFood", listTT.Sum(u => u.Food));
            printTimekeeping1.SetParameterValue("pBonus", _list.Sum(u => u.Bunus));
            printTimekeeping1.SetParameterValue("pPunish", _list.Sum(u => u.Punish));
            printTimekeeping1.SetParameterValue("pProduct", _list.Sum(u => u.TotalWeight));
            printTimekeeping1.SetParameterValue("pTime", _list.Sum(u => u.Time));
            printTimekeeping1.SetParameterValue("pMSNV", _msnv);
            printTimekeeping1.SetParameterValue("pDebt", Debt);
            printTimekeeping1.SetParameterValue("pMoneyProduct", Moneyproduct);
            printTimekeeping1.SetParameterValue("pMoneyTime", Moneytime);
            Pay = _list.Sum(u => u.TotalWeight) * Moneyproduct + _list.Sum(u => u.Time) * Moneytime + _list.Sum(u => u.Bunus)
                - _list.Sum(u => u.Punish) - listTT.Sum(u => u.Food) - listTT.Sum(u => u.AdvancePayment) - Debt;
            printTimekeeping1.SetParameterValue("pWage", Pay);
            crystalReportViewer.ReportSource = printTimekeeping1;
        }
    }
}
