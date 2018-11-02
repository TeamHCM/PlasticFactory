using BUS.Business;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCEmployee
{
    public partial class MCEmployeeManagement : UserControl
    {
        #region Generate Field
        TimekeepingBO timekeepingBO = new TimekeepingBO();
        EmployeePaymentBO employeePaymentBO = new EmployeePaymentBO();
        PreferceProductPriceBO preferceProductPriceBO = new PreferceProductPriceBO();
        Data.EmployeePayment employeePayment = new Data.EmployeePayment();
        EmployeeBO employeeBO = new EmployeeBO();
        List<Data.Timekeeping> listTime = new List<Data.Timekeeping>();
        List<Data.EmployeePayment> listEpay = new List<Data.EmployeePayment>();
        int Month = 0;
        int Year = 0;
        struct Payment
        {
            public double pay { get; set; }
            public double payed { get; set; }
            public double wage { get; set; }
            public double cash { get; set; }
            //Nợ tháng trước
            public double debtAgo { get; set; }
            public double debtNow { get; set; }
        }
        //Save old Location
        Point PlbWage;
        Point PlbCash;
        Point PlbPayed;
        Point PlbPay;
        Point PlbDebt;

        Point PtxtWage;
        Point PtxtCash;
        Point PtxtPayed;
        Point PtxtPay;
        Point PtxtDebt;
        Payment payment = new Payment();
        private string msnv = "";
        #endregion

        #region Support
        private void ResetForm()
        {
            txtWage.Text = "0";
            txtPayed.Text = "0";
            txtDebtNow.Text = "0";
            txtPay.Text = "0";
            txtPay.Text = "0";
            txtCash.Text = "0";
        }

        private void loadMonth()
        {
            txtMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                txtMonth.Items.Add(i);
            }
            Month = DateTime.Now.Month;
            txtMonth.Text = Month.ToString();
        }

        private void loadYear()
        {
            int curYear = DateTime.Now.Year;
            txtYear.Items.Clear();
            for (int i = curYear - 10; i <= curYear; i++)
            {
                txtYear.Items.Add(i);
            }
            Year = DateTime.Now.Year;
            txtYear.Text = Year.ToString();
        }

        private void loadEmployeeName()
        {
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            txtEmployeeName.Items.Clear();
            int i = 0;
            foreach (var item in listDB)
            {
                txtEmployeeName.Items.Add(item.Hoten);
            }
            txtEmployeeName.Items.Add("All");
            txtEmployeeName.Text = "All";
        }

        private void loadMSNV()
        {
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            txtMSNV.Items.Clear();
            int i = 0;
            foreach (var item in listDB)
            {
                txtMSNV.Items.Add(item.MSNV);
            }
            txtMSNV.Items.Add("All");
            txtMSNV.Text = "All";
        }

        private void loadDG()
        {
            ResetForm();
            dataDS.Rows.Clear();
            if (txtMSNV.Text == "All")
            {
                var listDB = employeePaymentBO.GetData(u => u.isDelete == false && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year);
                int i = 0;
                txtWage.Visible = false;
                lbWage.Visible = false;
                txtPay.Visible = false;
                lbPay.Visible = false;

                lbCash.Location = PlbWage;
                lbDebt.Location = PlbPay;
                lbPayed.Location = PlbCash;

                txtCash.Location = PtxtWage;
                txtDebtNow.Location = PtxtPay;
                txtPayed.Location = PtxtCash;
                

                foreach (var item in listDB)
                {
                    dataDS.Rows.Add();
                    double debtAgo = 0;
                    var ListDB = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV && u.Date.Value.Month == Month && u.Date.Value.Year == Year);
                    dataDS.Rows[i].Cells[0].Value = "TT" + item.ID.ToString("D6");
                    dataDS.Rows[i].Cells[1].Value = item.DATE.Value.ToShortDateString();
                    dataDS.Rows[i].Cells[2].Value = item.MSNV;
                    dataDS.Rows[i].Cells[3].Value = employeeBO.GetNameByID(item.MSNV);
                    dataDS.Rows[i].Cells[4].Value = ListDB.Sum(u => u.Time);
                    dataDS.Rows[i].Cells[5].Value = ListDB.Sum(u => u.TotalWeight);
                    //GetTime month ago =>Debt
                    if (Month == 1)
                    {
                        int count = employeePaymentBO.GetData(u=>u.isDelete==false&&u.MSNV==item.MSNV).Where(u => u.DATE.Value.Month == 12 && u.DATE.Value.Year == Year - 1).Count();
                        if (count != 0)
                        {
                            debtAgo = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV).Where(u => u.DATE.Value.Month == 12 && u.DATE.Value.Year == Year - 1).First().NEBT;
                        }
                    }
                    else
                    {
                        int count = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV).Where(u => u.DATE.Value.Month == Month - 1 && u.DATE.Value.Year == Year).Count();
                        if (count != 0)
                        {
                            debtAgo = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV).Where(u => u.DATE.Value.Month == Month - 1 && u.DATE.Value.Year == Year).First().NEBT;
                        }
                    }
                    dataDS.Rows[i].Cells[6].Value = debtAgo;
                    dataDS.Rows[i].Cells[7].Value = item.Cash;
                    dataDS.Rows[i].Cells[8].Value = item.PAY;
                    i++;
                }
                var listAllPayment = employeePaymentBO.GetData(u => u.isDelete == false && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year);
                txtPayed.Text = listAllPayment.Sum(u => u.PAY) != 0 ? listAllPayment.Sum(u => u.PAY).ToString("#,###") : "0";
                txtDebtNow.Text = listAllPayment.Sum(u => u.NEBT) != 0 ? listAllPayment.Sum(u => u.NEBT).ToString("#,###") : "0";
                txtCash.Text = listAllPayment.Sum(u => u.Cash) != 0 ? listAllPayment.Sum(u => u.Cash).ToString("#,###") : "0";

            }
            else
            {
                txtWage.Visible = true;
                lbWage.Visible = true;
                txtPay.Visible = true;
                lbPay.Visible = true;

                lbDebt.Location = PlbDebt;
                lbPayed.Location = PlbPayed;
                lbCash.Location = PlbCash;

                txtDebtNow.Location = PtxtDebt;
                txtPayed.Location = PtxtPayed;
                txtCash.Location = PtxtCash;
                payment = new Payment();
                int Time = (int)listTime.Sum(u => u.Time);
                int Product = (int)listTime.Sum(u => long.Parse(u.TotalWeight.ToString()));
                int ProductPrice = 0;
                int TimePrice = 0;
                int priceCount = listEpay.Where(u => u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).Count();
                if (priceCount != 0)
                {
                    ProductPrice = listEpay.Where(u => u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).First().ProductPrice;
                    TimePrice = listEpay.Where(u => u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).First().TimePrice;
                }
                payment.cash = (int)listTime.Sum(u => u.AdvancePayment);
                payment.payed = listEpay.Where(u => u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).Sum(u => u.PAY);
                //GetTime month ago =>Debt
                if (Month == 1)
                {
                    int count = listEpay.Where(u => u.DATE.Value.Month == 12 && u.DATE.Value.Year == Year - 1).Count();
                    if (count != 0)
                    {
                        payment.debtAgo = listEpay.Where(u => u.DATE.Value.Month == 12 && u.DATE.Value.Year == Year - 1).First().NEBT;
                    }
                }
                else
                {
                    int count = listEpay.Where(u => u.DATE.Value.Month == Month - 1 && u.DATE.Value.Year == Year).Count();
                    if (count != 0)
                    {
                        payment.debtAgo = listEpay.Where(u => u.DATE.Value.Month == Month - 1 && u.DATE.Value.Year == Year).First().NEBT;
                    }
                }
                payment.pay = Time * TimePrice + Product * ProductPrice - payment.cash - payment.debtAgo - payment.payed;
                var listDB = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == msnv && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year);
                int i = 0;
                foreach (var item in listDB)
                {
                    dataDS.Rows.Add();
                    dataDS.Rows[i].Cells[0].Value = "TT" + item.ID.ToString("D6");
                    dataDS.Rows[i].Cells[1].Value = item.DATE.Value.ToShortDateString();
                    dataDS.Rows[i].Cells[2].Value = item.MSNV;
                    dataDS.Rows[i].Cells[3].Value = employeeBO.GetNameByID(item.MSNV);
                    dataDS.Rows[i].Cells[4].Value = Time;
                    dataDS.Rows[i].Cells[5].Value = Product;
                    dataDS.Rows[i].Cells[6].Value = payment.debtAgo;
                    dataDS.Rows[i].Cells[7].Value = item.Cash;
                    dataDS.Rows[i].Cells[8].Value = item.PAY;
                    i++;
                }
                txtWage.Text = (Time * TimePrice + Product * ProductPrice) != 0 ? (Time * TimePrice + Product * ProductPrice).ToString("#,###") : "0";
                txtPayed.Text = payment.payed != 0 ? payment.payed.ToString("#,###") : "0";
                txtDebtNow.Text = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == msnv && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).Select(u => u.NEBT).ToList()[0] != 0 ? employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == msnv && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).Select(u => u.NEBT).ToList()[0].ToString("#,###") : "0";
                if (payment.pay < 0)
                {
                    txtPay.Text = "0";
                }
                else
                {
                    txtPay.Text = (Time * TimePrice + Product * ProductPrice - payment.cash - payment.debtAgo) != 0 ? (Time * TimePrice + Product * ProductPrice - payment.cash - payment.debtAgo).ToString("#,###") : "0";
                }
                txtCash.Text = payment.cash != 0 ? payment.cash.ToString("#,###") : "0";
            }
        }
        #endregion
        public MCEmployeeManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void MCEmployeeManagement_Load(object sender, EventArgs e)
        {
            PlbWage = new Point(lbWage.Location.X, lbWage.Location.Y);
            PlbCash = new Point(lbCash.Location.X, lbCash.Location.Y);
            PlbPayed = new Point(lbPayed.Location.X, lbPayed.Location.Y);
            PlbPay = new Point(lbPay.Location.X, lbPay.Location.Y);
            PlbDebt = new Point(lbDebt.Location.X, lbDebt.Location.Y);

            PtxtWage = new Point(txtWage.Location.X, txtWage.Location.Y);
            PtxtCash = new Point(txtCash.Location.X, txtCash.Location.Y);
            PtxtPayed = new Point(txtPayed.Location.X, txtPayed.Location.Y);
            PtxtPay = new Point(txtPay.Location.X, txtPay.Location.Y);
            PtxtDebt = new Point(txtDebtNow.Location.X, txtDebtNow.Location.Y);
            loadMonth();
            loadYear();
            loadMSNV();
            loadEmployeeName();
            loadDG();
        }

        private void txtMSNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMSNV.Text == "All")
                {
                    txtEmployeeName.Text = "All";
                }
                else
                {
                    txtEmployeeName.Text = employeeBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text).Select(u => u.Hoten).ToList()[0];
                    msnv = txtMSNV.Text;
                    listTime = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == msnv && u.Date.Value.Month == Month && u.Date.Value.Year == Year).ToList();
                    listEpay = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == msnv).ToList();
                }
                loadDG();
            }
            catch { }
        }

        private void txtEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeName.Text == "All")
                {
                    txtMSNV.Text = "All";
                }
                else
                {
                    txtMSNV.Text = employeeBO.GetData(u => u.isDelete == false && u.Hoten == txtEmployeeName.Text).Select(u => u.MSNV).ToList()[0];
                }
            }
            catch { }
        }

        private void txtMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Month = int.Parse(txtMonth.Text);
            txtMSNV_SelectedIndexChanged(sender, e);
        }

        private void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Year = int.Parse(txtYear.Text);
            txtMSNV_SelectedIndexChanged(sender, e);
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (txtMSNV.Text != "All")
            {
                MCChamcong.Management management = new MCChamcong.Management();
                PreferenceMenu.PMChamcong pmChamcong = new PreferenceMenu.PMChamcong();
                management.Sender(txtEmployeeName.Text, txtMSNV.Text, Month, Year);
                frmLayout.panelContents.Controls.Clear();
                frmLayout.panelPreference.Controls.Clear();
                frmLayout.panelPreference.Controls.Add(pmChamcong);
                frmLayout.panelContents.Controls.Add(management);
            }
        }
    }
}