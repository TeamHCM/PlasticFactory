using BUS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCEmployee
{
    public partial class MCPaymentEmployee : UserControl
    {
        #region Generate Field

        private TimekeepingBO timekeepingBO = new TimekeepingBO();
        private EmployeePaymentBO employeePaymentBO = new EmployeePaymentBO();
        private PreferceProductPriceBO preferceProductPriceBO = new PreferceProductPriceBO();
        private Data.EmployeePayment employeePayment = new Data.EmployeePayment();
        private EmployeeBO employeeBO = new EmployeeBO();
        private List<Data.Timekeeping> listTime = new List<Data.Timekeeping>();
        private List<Data.EmployeePayment> listEpay = new List<Data.EmployeePayment>();
        private int Month = 0;
        private int Year = 0;

        private struct Payment
        {
            public double pay { get; set; }
            public double payed { get; set; }
            public double wage { get; set; }
            public double cash { get; set; }

            //Nợ tháng trước
            public double debtAgo { get; set; }

            public double debtNow { get; set; }
        }

        private Payment payment = new Payment();
        private string MSNV = "";
        private int btnDoubleGrid = 0;
        private int removeMSTT = 0;
        private string TimeKeepDate = "";
        private int MonthDS = 0;
        private int YearDS = 0;

        #endregion Generate Field

        #region Support

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

        private void loadPrice()
        {
            var listDB = preferceProductPriceBO.GetData(u => u.Name.Trim() == "Sản phẩm");
            foreach (var item in listDB)
            {
                txtMoneyOfProduct.Items.Add(item.Price);
            }
            txtMoneyOfProduct.Text = preferceProductPriceBO.GetData(u => u.Name.Trim() == "Sản phẩm").First().Price.ToString();

            listDB = preferceProductPriceBO.GetData(u => u.Name.Trim() == "Thời gian");
            foreach (var item in listDB)
            {
                txtMoneyOfTime.Items.Add(item.Price);
            }
            txtMoneyOfTime.Text = preferceProductPriceBO.GetData(u => u.Name.Trim() == "Thời gian").First().Price.ToString();
        }

        private void loadEmployeeName()
        {
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            txtEmployeeName.Properties.Items.Clear();
            int i = 0;
            foreach (var item in listDB)
            {
                int count = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV && u.MonthOfPay == Month && u.YearOfPay == Year).Count;
                if (count != 0)
                {
                    bool isPayed = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV && u.MonthOfPay == Month && u.YearOfPay == Year).First().isPayed;
                    if (isPayed == true)
                    {
                        txtEmployeeName.Properties.Items.Add(item.Hoten, item.Hoten, 0);
                    }
                    else
                    {
                        txtEmployeeName.Properties.Items.Add(item.Hoten, item.Hoten, -1);
                    }
                }
                else
                {
                    txtEmployeeName.Properties.Items.Add(item.Hoten, item.Hoten, -1);
                }
                i++;
            }
        }

        private void loadTotalOFDetail()
        {
            payment = new Payment();
            double Time = (double)listTime.Sum(u => u.Time);
            double Product = (double)listTime.Sum(u => long.Parse(u.TotalWeight.ToString()));
            payment.cash = (int)listTime.Sum(u => u.AdvancePayment);
            payment.payed = listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Sum(u => u.PAY);
            txtTime.Text = Time.ToString();
            txtProduct.Text = Product.ToString();
            txtCash.Text = payment.cash.ToString();
            //GetTime month ago =>Debt
            if (Month == 1)
            {
                int count = listEpay.Where(u => u.MonthOfPay == 12 && u.YearOfPay == Year - 1).Count();
                if (count != 0)
                {
                    payment.debtAgo = listEpay.Where(u => u.MonthOfPay == 12 && u.YearOfPay == Year - 1).First().NEBT;
                }
            }
            else
            {
                int count = listEpay.Where(u => u.DATE.Value.Month == Month - 1 && u.DATE.Value.Year == Year).Count();
                if (count != 0)
                {
                    payment.debtAgo = listEpay.Where(u => u.MonthOfPay == Month - 1 && u.YearOfPay == Year).First().NEBT;
                }
            }
            txtDebt.Text = payment.debtAgo.ToString();
            payment.pay = (int)(Time * double.Parse(txtMoneyOfTime.Text) + Product * double.Parse(txtMoneyOfProduct.Text) - payment.cash - payment.debtAgo - payment.payed);
            //MessageBox.Show("Pay dau :" + payment.pay);
            if (payment.pay > 0)
            {
                txtNoteMoney.Text = payment.pay.ToString();
                txtPay.Text = payment.pay.ToString();
            }
            if (payment.pay < 0)
            {
                //kiem tra da luu hoa dơn chua nợ chưa
                int tCount = listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count();
                payment.debtNow = payment.pay * (-1);
                txtPay.Text = "0";
                txtNoteMoney.Text = "0";
                if (tCount == 0)
                {
                    MessageBox.Show("Nhân viên nợ công ty với số tiền là " + payment.debtNow + "nên số tiền thành toán là 0 đồng");
                }
                //else
                //{
                //    MessageBox.Show("Nhân viên này đã thanh toán rồi");
                //}
            }
            //if(payment.pay==0&&listEpay.Where(u=>u.DATE.Value.Month==Month&&u.DATE.Value.Year==Year).Count()!=0)
            //{
            //    MessageBox.Show("Nhân viên này đã thanh toán rồi");
            //    txtPay.Text = "0";
            //}
        }

        private void loadDetail()
        {
            //Load for DataGrid
            string MSNV = txtMSNV.Text;
            var listDB = timekeepingBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month && u.Date.Value.Year == Year && u.MSNV == MSNV).ToList();
            dataDetailWork.Rows.Clear();
            for (int i = 0; i < listDB.Count(); i++)
            {
                dataDetailWork.Rows.Add();
                //color
                if (payment.pay > 0 && listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count() != 0)
                {
                    dataDetailWork.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Orange;
                }
                if (payment.pay < 0)
                {
                    //kiem tra da luu hoa dơn chua nợ chưa
                    int tCount = listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count();
                    if (tCount != 0)
                    {
                        dataDetailWork.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Green;
                    }
                }
                if (payment.pay == 0 && listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count() != 0)
                {
                    dataDetailWork.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Green;
                }
                //.endcolor
                dataDetailWork.Rows[i].Cells[0].Value = listDB[i].Date.Value.Day + "/" + listDB[i].Date.Value.Month;
                dataDetailWork.Rows[i].Cells[1].Value = listDB[i].TimeStart + " - " + listDB[i].TimeEnd;
                dataDetailWork.Rows[i].Cells[2].Value = listDB[i].Time;
                dataDetailWork.Rows[i].Cells[3].Value = listDB[i].TotalWeight;
                dataDetailWork.Rows[i].Cells[4].Value = listDB[i].AdvancePayment;
                dataDetailWork.Rows[i].Cells[5].Value = listDB[i].Note;
            }
            txtDayWork.Text = listDB.Count().ToString();
        }

        private void loadDG()
        {
            dataDS.Rows.Clear();
            var listDB = employeePaymentBO.GetData(u => u.isDelete == false && u.DATE.Value.Month == MonthDS && u.DATE.Value.Year == YearDS);
            int i = 0;
            foreach (var item in listDB)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = "TT" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = item.DATE.Value.ToShortDateString();
                dataDS.Rows[i].Cells[2].Value = item.MSNV;
                dataDS.Rows[i].Cells[3].Value = employeeBO.GetNameByID(item.MSNV);
                dataDS.Rows[i].Cells[4].Value = item.Wage;
                dataDS.Rows[i].Cells[5].Value = item.Cash;
                dataDS.Rows[i].Cells[6].Value = item.PAY;
                dataDS.Rows[i].Cells[7].Value = item.NEBT;
                dataDS.Rows[i].Cells[8].Value = item.MonthOfPay + "/" + item.YearOfPay;
                i++;
            }
        }

        private Data.EmployeePayment InputEmployeePayment()
        {
            double Time = double.Parse(txtTime.Text);
            int Product = int.Parse(txtProduct.Text);
            employeePayment = new Data.EmployeePayment();
            employeePayment.ID = int.Parse(txtID.Text.ToString().Substring(2));
            employeePayment.DATE = txtDate.Value;
            employeePayment.MSNV = txtMSNV.Text;
            employeePayment.PAY = int.Parse(txtPay.Text);
            employeePayment.NEBT = payment.debtNow;
            employeePayment.ProductPrice = int.Parse(txtMoneyOfProduct.Text);
            employeePayment.TimePrice = int.Parse(txtMoneyOfTime.Text);
            employeePayment.Wage = (int)(Time * int.Parse(txtMoneyOfTime.Text) + Product * int.Parse(txtMoneyOfProduct.Text));
            employeePayment.Cash = payment.cash;
            employeePayment.MonthOfPay = Month;
            employeePayment.YearOfPay = Year;
            return employeePayment;
        }

        private void AddPayment()
        {
            employeePaymentBO.Add(InputEmployeePayment());
            loadDG();
            ResetForm();
        }

        private void ResetForm()
        {
            txtID.Text = "TT" + employeePaymentBO.GetID().ToString("D6");
            txtMSNV.Text = "";
            txtEmployeeName.Text = "";
            txtPay.Text = "0";
            txtNoteMoney.Text = "0";
            txtTime.Text = "0";
            txtProduct.Text = "0";
            txtDebt.Text = "0";
            txtCash.Text = "0";
            MSNV = "";
            txtDayWork.Text = "0";
            loadEmployeeName();
            dataDetailWork.Rows.Clear();
        }

        #endregion Support

        public MCPaymentEmployee()
        {
            InitializeComponent();
        }

        private void MCPaymentEmployee_Load(object sender, EventArgs e)
        {
            //lấy thời gian cho dataDS
            MonthDS = txtDate.Value.Month;
            YearDS = txtDate.Value.Year;
            //
            txtID.Text = "TT" + employeePaymentBO.GetID().ToString("D6");
            loadMonth();
            loadYear();
            loadPrice();
            loadEmployeeName();
            loadDG();
            //loadDetail();
        }

        #region Event GetInformation

        private void txtEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadEmployeeName();
                string Name = txtEmployeeName.Text;
                txtMSNV.Text = employeeBO.GetData(u => u.isDelete == false && u.Hoten.Trim() == Name.Trim()).Select(u => u.MSNV).ToList()[0];
                MSNV = txtMSNV.Text;
                listTime = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == MSNV && u.Date.Value.Month == Month && u.Date.Value.Year == Year).ToList();
                listEpay = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == MSNV).ToList();
                loadTotalOFDetail();
                loadDetail();
            }
            catch { }
        }

        #endregion Event GetInformation

        #region Month,Year

        private void txtMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Month = int.Parse(txtMonth.Text);
                txtEmployeeName_SelectedIndexChanged(sender, e);
                loadDG();
            }
            catch
            {
            }
        }

        private void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Year = int.Parse(txtYear.Text);
            txtEmployeeName_SelectedIndexChanged(sender, e);
            loadDG();
        }

        #endregion Month,Year

        #region Button

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMSNV.Text != "" && txtPay.Text != string.Empty && listTime.Count() != 0)
            {
                if (payment.pay != 0)
                {
                    if (payment.pay < 0 && listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count() == 0)
                    {
                        //cho thanh toan
                        int intPay = int.Parse(txtPay.Text);
                        if (intPay != 0)
                        {
                            MessageBox.Show("Nhân viên nợ công ty với số tiền là " + payment.debtNow + "nên số tiền thành toán là 0 đồng");
                            txtPay.Text = "0";
                        }
                        else
                        {
                            //cho thanh toan
                            AddPayment();
                        }
                    }
                    if (payment.pay < 0 && listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count() != 0)
                    {
                        //da thanh toan roi
                        MessageBox.Show("Nhân viên này đã thanh toán rồi. Vui lòng kiểm tra lại");
                        ResetForm();
                    }
                    if (payment.pay > 0)
                    {
                        //cho thanh toan
                        int intPay = int.Parse(txtPay.Text);
                        if (intPay > payment.pay)
                        {
                            MessageBox.Show("Bạn chỉ cần thanh toán" + payment.pay + "là đủ");
                            txtPay.Text = payment.pay.ToString();
                        }
                        if (intPay == 0)
                        {
                            MessageBox.Show("Vui lòng điền giá tiền");
                        }
                        if (intPay <= payment.pay)
                        {
                            //cho thanh toan
                            AddPayment();
                        }
                    }
                }
                if (payment.pay == 0)
                {
                    if (listEpay.Where(u => u.MonthOfPay == Month && u.YearOfPay == Year).Count() != 0)
                    {
                        MessageBox.Show("Nhân viên này đã thanh toán rồi. Vui lòng kiểm tra lại");
                        ResetForm();
                    }
                    else
                    {
                        //cho thanh toan
                        int intPay = int.Parse(txtPay.Text);
                        if (intPay != 0)
                        {
                            MessageBox.Show("Bạn chỉ cần thanh toán" + payment.pay + "là đủ");
                            txtPay.Text = payment.pay.ToString();
                        }
                        else
                        {
                            //cho thanh toan
                            AddPayment();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (removeMSTT != 0)
            {
                string masseage = "Bạn có muốn xóa  TT" + removeMSTT.ToString("D6") + "không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    employeePaymentBO.isDelete(removeMSTT);
                    loadDG();
                    loadEmployeeName();
                    removeMSTT = 0;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtID.Text.Substring(2));
            Payment Editpayment = new Payment();
            double Time = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == MSNV && u.Date.Value.Month == DateTime.Parse(TimeKeepDate).Month && u.Date.Value.Year == DateTime.Parse(TimeKeepDate).Year).Sum(u => u.Time);
            int Product = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == MSNV && u.Date.Value.Month == DateTime.Parse(TimeKeepDate).Month && u.Date.Value.Year == DateTime.Parse(TimeKeepDate).Year).Sum(u => u.TotalWeight).Value;
            Editpayment.cash = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == MSNV && u.Date.Value.Month == DateTime.Parse(TimeKeepDate).Month && u.Date.Value.Year == DateTime.Parse(TimeKeepDate).Year).Sum(u => u.AdvancePayment).Value;
            Editpayment.payed = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text).Where(u => u.DATE.Value.Month == Month && u.DATE.Value.Year == Year && u.MonthOfPay == DateTime.Parse(TimeKeepDate).Month && u.YearOfPay == DateTime.Parse(TimeKeepDate).Year && u.ID != ID).Sum(u => u.PAY);

            //GetTime month ago =>Debt
            if (Month == 1)
            {
                int count = listEpay.Where(u => u.MonthOfPay == 12 && u.YearOfPay == Year - 1).Count();
                if (count != 0)
                {
                    Editpayment.debtAgo = listEpay.Where(u => u.MonthOfPay == 12 && u.YearOfPay == Year - 1).First().NEBT;
                }
            }
            else
            {
                int count = listEpay.Where(u => u.MonthOfPay == Month - 1 && u.YearOfPay == Year).Count();
                if (count != 0)
                {
                    Editpayment.debtAgo = listEpay.Where(u => u.MonthOfPay == Month - 1 && u.YearOfPay == Year).First().NEBT;
                }
            }
            Editpayment.pay = Time * int.Parse(txtMoneyOfTime.Text) + Product * int.Parse(txtMoneyOfProduct.Text) - Editpayment.cash - Editpayment.debtAgo - Editpayment.payed;

            //MessageBox.Show("Pay dau :" + payment.pay);
            double intPay = double.Parse(txtPay.Text);
            if (Editpayment.pay < 0)
            {
                Editpayment.pay = 0;
            }
            if (intPay > Editpayment.pay)
            {
                MessageBox.Show("Bạn chỉ cần chỉ cần thanh toán " + Editpayment.pay + " là đủ");
                txtPay.Text = Editpayment.pay.ToString();
            }
            else
            {
                employeePaymentBO.Update(InputEmployeePayment());
                loadDG();
                loadDetail();
                ResetForm();
                btnRemove.Enabled = true;
                btnSave.Enabled = true;
                btnEdit.Visible = false;
                btnThem.Visible = true;
                TimeKeepDate = "";
            }
        }

        #endregion Button

        #region DataDS

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataDS.Rows.Count - 1)
            {
                switch (btnDoubleGrid)
                {
                    case 0:
                        try
                        {
                            btnEdit.Visible = true;
                            int ID = int.Parse(dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(2));
                            string strMSNV = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
                            txtID.Text = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                            txtDate.Text = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
                            txtMSNV.Text = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
                            txtEmployeeName.Text = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                            txtTime.Text = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == strMSNV && u.Date.Value.Month == Month && u.Date.Value.Year == Year).Sum(u => u.Time).ToString();
                            txtProduct.Text = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == strMSNV && u.Date.Value.Month == Month && u.Date.Value.Year == Year).Sum(u => u.TotalWeight).ToString();
                            txtMoneyOfTime.Text = employeePaymentBO.GetData(u => u.isDelete == false && u.ID == ID).First().TimePrice.ToString();
                            txtMoneyOfProduct.Text = employeePaymentBO.GetData(u => u.isDelete == false && u.ID == ID).First().ProductPrice.ToString();
                            TimeKeepDate = dataDS.Rows[e.RowIndex].Cells[8].Value.ToString();
                            double tempDebt = 0;
                            if (Month == 1)
                            {
                                int count = listEpay.Where(u => u.MonthOfPay == 12 && u.YearOfPay == Year - 1).Count();
                                if (count != 0)
                                {
                                    tempDebt = listEpay.Where(u => u.MonthOfPay == 12 && u.YearOfPay == Year - 1).First().NEBT;
                                }
                            }
                            else
                            {
                                int count = listEpay.Where(u => u.MonthOfPay == Month - 1 && u.YearOfPay == Year).Count();
                                if (count != 0)
                                {
                                    tempDebt = listEpay.Where(u => u.MonthOfPay == Month - 1 && u.YearOfPay == Year).First().NEBT;
                                }
                            }
                            txtDebt.Text = tempDebt.ToString();
                            txtCash.Text = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == strMSNV && u.Date.Value.Month == Month && u.Date.Value.Year == Year).Sum(u => u.AdvancePayment).ToString();
                            txtPay.Text = employeePaymentBO.GetData(u => u.isDelete == false && u.ID == ID).First().PAY.ToString();
                            txtNoteMoney.Text = employeePaymentBO.GetData(u => u.isDelete == false && u.ID == ID).First().PAY.ToString();
                            txtMonth.Text = dataDS.Rows[e.RowIndex].Cells[8].Value.ToString().Split('/')[0];
                            btnRemove.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        catch
                        {
                        }

                        btnDoubleGrid = 1;
                        break;

                    case 1:

                        btnRemove.Enabled = true;
                        btnSave.Enabled = true;
                        ResetForm();
                        btnEdit.Visible = false;
                        btnDoubleGrid = 0;
                        btnThem.Visible = true;
                        TimeKeepDate = "";
                        break;
                }
            }
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataDS.Rows.Count - 1)
                {
                    removeMSTT = int.Parse(dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(2));
                }
            }
            catch { }
        }

        #endregion DataDS

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MonthDS = txtDate.Value.Month;
                YearDS = txtDate.Value.Year;
                loadDG();
            }
            catch { }
        }
    }
}