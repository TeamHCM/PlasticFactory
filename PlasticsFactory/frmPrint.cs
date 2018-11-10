using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS.Business;
namespace PlasticsFactory
{
    public partial class frmPrint : Form
    {
        List<pTimekeeping> _list;
        EmployeePaymentBO employeePaymentBO = new EmployeePaymentBO();
        EmployeeBO EmployeeBO = new EmployeeBO();
        int _month = 0;
        int _year = 0;
        string _msnv = "";
        public frmPrint(List<pTimekeeping> list,string msnv,int month,int year)
        {
            InitializeComponent();
            _list = list;
            _msnv = msnv;
            _month = month;
            _year = year;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        { 
           
            var listTT = _list;
            var _detail = employeePaymentBO.GetData(u => u.isDelete == false &&u.MSNV==_msnv&& u.MonthOfPay == _month && u.YearOfPay == _year).First();
            double debtAgo = 0;
            if (_month == 1)
            {
                int count = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv).Where(u => u.MonthOfPay == 12 && u.YearOfPay == _year - 1).Count();
                if (count != 0)
                {
                    debtAgo = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv).Where(u => u.MonthOfPay == 12 && u.YearOfPay == _year - 1).First().NEBT;
                }
            }
            else
            {
                int count = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv).Where(u => u.DATE.Value.Month == _month - 1 && u.DATE.Value.Year == _year).Count();
                if (count != 0)
                {
                    debtAgo = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == _msnv).Where(u => u.MonthOfPay == _month - 1 && u.YearOfPay == _year).First().NEBT;
                }
            }
            printEmployee1.SetDataSource(_list);
            printEmployee1.SetParameterValue("pEmployeeName",EmployeeBO.GetData(u=>u.isDelete==false&&u.MSNV==_msnv).First().Hoten);
            printEmployee1.SetParameterValue("pMSTT", "TT"+_detail.ID.ToString("d6"));
            printEmployee1.SetParameterValue("pDate", _detail.DATE);
            printEmployee1.SetParameterValue("pTimekeeping", _detail.MonthOfPay+"/"+_detail.YearOfPay);
            printEmployee1.SetParameterValue("pWage", _detail.Wage);
            printEmployee1.SetParameterValue("pCash", _detail.Cash);
            printEmployee1.SetParameterValue("pFood", _list.Sum(u=>u.Food));
            printEmployee1.SetParameterValue("pBonus", _list.Sum(u=>u.Bunus));
            printEmployee1.SetParameterValue("pPunish", _list.Sum(u=>u.Punish));
            printEmployee1.SetParameterValue("pPayment", _detail.PAY);
            printEmployee1.SetParameterValue("pDept", debtAgo);
            printEmployee1.SetParameterValue("pMSNV", _msnv);
            crystalReportViewer.ReportSource = printEmployee1;
        }
    }
}
