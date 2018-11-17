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
        public MCChamcong.Management management = new MCChamcong.Management();
        PreferenceMenu.PMChamcong pmChamcong = new PreferenceMenu.PMChamcong();
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

        Payment payment = new Payment();
        private string msnv = "";
        #endregion

        #region Support
        private void loadEmployee()
        {
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            dataEmployee.Rows.Clear();
            int i = 0;
            foreach (var item in listDB)
            {
                dataEmployee.Rows.Add();
                dataEmployee.Rows[i].Cells[0].Value = item.MSNV;
                dataEmployee.Rows[i].Cells[1].Value = item.Hoten;
                dataEmployee.Rows[i].Cells[2].Value = item.Gioitinh;
                dataEmployee.Rows[i].Cells[3].Value = item.Ngaysinh;
                dataEmployee.Rows[i].Cells[4].Value = item.Diachi;
                dataEmployee.Rows[i].Cells[5].Value = item.CMND;
                dataEmployee.Rows[i].Cells[6].Value = item.SDT;
                i++;
            }
        }

        private void ResetFormEmployee()
        {
            loadEmployee();
            txtnvMSNV.ResetText();
            txtEmployeeNam.ResetText();
            txtBirthDay.ResetText();
            txtDiachi.ResetText();
            txtCMND.ResetText();
            txtSDT.ResetText();
            rdNam.Checked = true;
        }

        private void ResetForm()
        {
            txtPayed.Text = "0";
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
                dataDS.Columns.Clear();
                #region Columns DataGridView
                dataDS.ColumnCount = 4;
                dataDS.Columns[0].Name = "MSNV";
                dataDS.Columns[1].Name = "Họ và tên";
                dataDS.Columns[2].Name = "Tiền đã thanh toán";
                dataDS.Columns[3].Name = "Chấm công";

                dataDS.Columns[0].Width = 100;
                dataDS.Columns[1].Width = 150;
                dataDS.Columns[2].Width = 900;
                dataDS.Columns[3].Width = 100;
                #endregion
                var listEmployee = employeeBO.GetData(u => u.isDelete == false);
                int i = 0;
                foreach (var item in listEmployee)
                {
                    dataDS.Rows.Add();
                    if (i % 2 == 0)
                    {
                        dataDS.Rows[i].Cells[0].Style.BackColor = Color.Gainsboro;
                        dataDS.Rows[i].Cells[1].Style.BackColor = Color.Gainsboro;
                        dataDS.Rows[i].Cells[2].Style.BackColor = Color.Gainsboro;
                        dataDS.Rows[i].Cells[3].Style.BackColor = Color.Gainsboro;
                    }
                    else
                    {
                        dataDS.Rows[i].Cells[0].Style.BackColor = Color.WhiteSmoke;
                        dataDS.Rows[i].Cells[1].Style.BackColor = Color.WhiteSmoke;
                        dataDS.Rows[i].Cells[2].Style.BackColor = Color.WhiteSmoke;
                        dataDS.Rows[i].Cells[3].Style.BackColor = Color.WhiteSmoke;
                    }
                    var ListDB = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV && u.DATE.Value.Month == int.Parse(txtMonth.Text) && u.DATE.Value.Year == int.Parse(txtYear.Text));
                    dataDS.Rows[i].Cells[0].Value = item.MSNV;
                    dataDS.Rows[i].Cells[1].Value = item.Hoten;
                    dataDS.Rows[i].Cells[2].Value = ListDB.Sum(u => u.PAY)!=0? ListDB.Sum(u => u.PAY).ToString("#,###"):"0";
                    string timekeeping = "";
                    int t = 0;
                    foreach (var itemTime in employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV).Select(u => u.MonthOfPay).Distinct())
                    {
                        if (t == 0)
                        {
                            timekeeping = itemTime.ToString();
                        }
                        if (t > 0)
                        {
                            timekeeping += "+" + itemTime.ToString();
                        }
                        t++;
                    }
                    dataDS.Rows[i].Cells[3].Value = timekeeping;
                    dataDS.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    i++;
                }
                var listAllPayment = employeePaymentBO.GetData(u => u.isDelete == false && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year);
                txtPayed.Text = listAllPayment.Sum(u => u.PAY) != 0 ? listAllPayment.Sum(u => u.PAY).ToString("#,###") : "0";
            }
            else
            {
                dataDS.Columns.Clear();
                #region Columns DataGridView
                dataDS.ColumnCount = 13;
                dataDS.Columns[0].Name = "MSTT";
                dataDS.Columns[1].Name = "Ngày lập";
                dataDS.Columns[2].Name = "MSNV";
                dataDS.Columns[3].Name = "Họ và tên";
                dataDS.Columns[4].Name = "Thời gian";
                dataDS.Columns[5].Name = "Sản phẩm";
                dataDS.Columns[6].Name = "Tiền nợ trước";
                dataDS.Columns[7].Name = "Tiền ứng";
                dataDS.Columns[8].Name = "Tiền cơm";
                dataDS.Columns[9].Name = "Tiền thưởng";
                dataDS.Columns[10].Name = "Tiền phạt";
                dataDS.Columns[11].Name = "Tiền đã thanh toán";
                dataDS.Columns[12].Name = "Chấm công";

                dataDS.Columns[0].Width = 100;
                dataDS.Columns[1].Width = 150;
                dataDS.Columns[2].Width = 100;
                dataDS.Columns[3].Width = 150;
                dataDS.Columns[4].Width = 100;
                dataDS.Columns[5].Width = 100;
                dataDS.Columns[6].Width = 150;
                dataDS.Columns[7].Width = 150;
                dataDS.Columns[8].Width = 100;
                dataDS.Columns[9].Width = 100;
                dataDS.Columns[10].Width = 100;
                dataDS.Columns[11].Width = 150;
                dataDS.Columns[12].Width = 100;
                #endregion
                payment = new Payment();
                double debtAgo = 0;
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
                payment.cash = (double)listTime.Sum(u => u.AdvancePayment);
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
                var listDB = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == msnv && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year).ToList();
                int i = 0;
                foreach (var item in listDB)
                {
                    dataDS.Rows.Add();
                    var ListDB = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV && u.Date.Value.Month == item.MonthOfPay && u.Date.Value.Year == item.YearOfPay);
                    dataDS.Rows[i].Cells[0].Value = "TT" + item.ID.ToString("D6");
                    dataDS.Rows[i].Cells[1].Value = item.DATE.Value.ToShortDateString();
                    dataDS.Rows[i].Cells[2].Value = item.MSNV;
                    dataDS.Rows[i].Cells[3].Value = employeeBO.GetNameByID(item.MSNV);
                    dataDS.Rows[i].Cells[4].Value = Math.Round(ListDB.Sum(u => u.Time), 1);
                    dataDS.Rows[i].Cells[5].Value = ListDB.Sum(u => u.TotalWeight);
                    //GetTime month ago =>Debt
                    if (Month == 1)
                    {
                        int count = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV).Where(u => u.DATE.Value.Month == 12 && u.DATE.Value.Year == Year - 1).Count();
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
                    dataDS.Rows[i].Cells[7].Value = item.Cash!=0? item.Cash.ToString("#,###"):"0";
                    dataDS.Rows[i].Cells[8].Value = ListDB.Sum(u => u.Food)!=0? ListDB.Sum(u => u.Food).ToString("#,###"):"0";
                    dataDS.Rows[i].Cells[9].Value = ListDB.Sum(u => u.Bunus)!=0? ListDB.Sum(u => u.Bunus).ToString("#,###"):"0";
                    dataDS.Rows[i].Cells[10].Value = ListDB.Sum(u => u.Punish)!=0? ListDB.Sum(u => u.Punish).ToString("#,###"):"0";
                    dataDS.Rows[i].Cells[11].Value = item.PAY!=0? item.PAY.ToString("#,###"):"0";
                    dataDS.Rows[i].Cells[12].Value = item.MonthOfPay + "/" + item.YearOfPay;
                    i++;
                }
                var listAllPayment = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year);
                txtPayed.Text = listAllPayment.Sum(u => u.PAY) != 0 ? listAllPayment.Sum(u => u.PAY).ToString("#,###") : "0";
            }
        }
        #endregion
        public MCEmployeeManagement()
        {
            InitializeComponent();
        }

        private void MCEmployeeManagement_Load(object sender, EventArgs e)
        {
            loadMonth();
            loadYear();
            loadMSNV();
            loadEmployeeName();
            loadDG();
            loadEmployee();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ManageEmployee frmMGEMployee = new ManageEmployee();
            frmMGEMployee.ShowDialog();
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
                    listTime = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == msnv).ToList();
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
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelPreference.Controls.Clear();
            frmLayout.panelPreference.Controls.Add(pmChamcong);
            frmLayout.panelContents.Controls.Add(management);
        }

        private void dataDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataDS.Rows.Count - 1)
                {
                    DateTime date = DateTime.Parse(dataDS.Rows[e.RowIndex].Cells[9].Value.ToString());
                    string MSNV = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string EmployeeName = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                    management = new MCChamcong.Management();
                    management.Sender(EmployeeName, MSNV, date);
                }
            }
            catch { }
        }

        private void dataEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataEmployee.Rows.Count - 1)
            {
                txtnvMSNV.Text = dataEmployee.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEmployeeNam.Text = dataEmployee.Rows[e.RowIndex].Cells[1].Value.ToString();
                string sex = dataEmployee.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (sex == "Nam")
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
                txtBirthDay.Text = dataEmployee.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCMND.Text = dataEmployee.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtDiachi.Text = dataEmployee.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtSDT.Text = dataEmployee.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (txtnvMSNV.Text != string.Empty)
            {
                employeeBO.isDelete(txtnvMSNV.Text);
                ResetFormEmployee();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Data.Employee employee = new Data.Employee();
            employee.Hoten = txtEmployeeNam.Text;
            if (rdNam.Checked)
            {
                employee.Gioitinh = "Nam";
            }
            else
            {
                employee.Gioitinh = "Nữ";
            }
            employee.Ngaysinh = DateTime.Parse(txtBirthDay.Text);
            employee.Diachi = txtDiachi.Text;
            employee.CMND = txtCMND.Text;
            employee.SDT = txtSDT.Text;
            employee.isDelete = false;
            employee.MSNV = txtnvMSNV.Text;
            employeeBO.Update(employee);
            ResetFormEmployee();
        }
    }
}