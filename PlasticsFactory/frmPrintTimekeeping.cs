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
            printTimekeeping1.SetDataSource(_list);
            printTimekeeping1.SetParameterValue("pEmployeeName", EmployeeBO.GetData(u => u.isDelete == false && u.MSNV == _msnv).First().Hoten);
            printTimekeeping1.SetParameterValue("pTimekeeping", listTT.First().Date.Month + "/" + listTT.First().Date.Year);
            printTimekeeping1.SetParameterValue("pCash", listTT.Sum(u=>u.AdvancePayment));
            printTimekeeping1.SetParameterValue("pFood", listTT.Sum(u=>u.Food));
            printTimekeeping1.SetParameterValue("pBonus", _list.Sum(u => u.Bunus));
            printTimekeeping1.SetParameterValue("pPunish", _list.Sum(u => u.Punish));
            printTimekeeping1.SetParameterValue("pProduct", _list.Sum(u => u.TotalWeight));
            printTimekeeping1.SetParameterValue("pTime", _list.Sum(u => u.Time));
            printTimekeeping1.SetParameterValue("pMSNV", _msnv);
            crystalReportViewer.ReportSource = printTimekeeping1;
        }
    }
}
