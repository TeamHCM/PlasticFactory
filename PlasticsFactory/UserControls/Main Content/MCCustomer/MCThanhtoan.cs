using BUS.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCCustomer
{
    public partial class MCThanhtoan : UserControl
    {
        #region Generate Field

        private string IDOfThanhToan = string.Empty;
        public ProductBO productIpBO = new ProductBO();
        public ProductInputBO productInputBO = new ProductInputBO();
        public ProductOBO productOBO = new ProductOBO();
        public ProductOutputBO productOutputBO = new ProductOutputBO();
        public CustomerBO customerBO = new CustomerBO();
        public CustomerTypeBO customerTypeBO = new CustomerTypeBO();
        private List<Data.PaymentInput> listInput = new List<Data.PaymentInput>();
        private List<Data.PaymentOutput> listOutput = new List<Data.PaymentOutput>();
        private PaymentInputBO paymentInputBO = new PaymentInputBO();
        private Data.PaymentInput paymentInput = new Data.PaymentInput();
        private PaymentOutputBO paymentOutputBO = new PaymentOutputBO();
        private Data.PaymentOutput paymentOutput = new Data.PaymentOutput();
        public static int MSHD = 0;
        public static bool Input = false;
        private int tempClickDS = 0;

        #endregion Generate Field

        #region Support

        private int CheckPayment()
        {
            int Amount = 0;
            if (txtCustomerType.Text == "Nhập hàng")
            {
                Amount = productInputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().TotalAmount.Value - productInputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().Payed;
                if (listInput.Where(u => u.MSDH == GetMSDH(txtMSDH.Text)).FirstOrDefault() != null)
                {
                    Amount = productInputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().TotalAmount.Value - productInputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().Payed - listInput.Where(u => u.MSDH == GetMSDH(txtMSDH.Text)).Sum(u => u.Payment).Value;
                }
                return Amount;
            }
            else
            {
                Amount = productOutputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().TotalAmount.Value - productOutputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().Payed;
                if (listOutput.Where(u => u.MSDH == GetMSDH(txtMSDH.Text)).FirstOrDefault() != null)
                {
                    Amount = productOutputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().TotalAmount.Value - productOutputBO.GetData(u => u.isDelete == false && u.ID == GetMSDH(txtMSDH.Text)).First().Payed - listOutput.Where(u => u.MSDH == GetMSDH(txtMSDH.Text)).Sum(u => u.Payment).Value;
                }
                return Amount;
            }
        }

        private int GetMSDH(string Name)
        {
            return int.Parse(Name.Trim().Split('D')[1]);
        }

        private int GetMSTTofInput()
        {
            int result = paymentInputBO.GetID();
            if (listInput.Count != 0)
            {
                int iFirst = listInput.First().ID;
                int iLast = listInput.Last().ID;
                foreach (var item in listInput)
                {
                    if (item.ID != iFirst)
                    {
                        return iFirst;
                    }
                    if (item.ID == iLast)
                    {
                        //++iFirst là tăng lúc gọi iFirst++ thì gọi xong thì mới tăng
                        return ++iFirst;
                    }
                    iFirst++;
                }
            }
            return result;
        }

        private int GetMSTTofOutput()
        {
            int result = paymentOutputBO.GetID();
            if (listOutput.Count != 0)
            {
                int iFirst = listOutput.First().ID;
                int iLast = listOutput.Last().ID;
                foreach (var item in listOutput)
                {
                    if (item.ID != iFirst)
                    {
                        return iFirst;
                    }
                    if (item.ID == iLast)
                    {
                        //++iFirst là tăng lúc gọi iFirst++ thì gọi xong thì mới tăng
                        return ++iFirst;
                    }
                    iFirst++;
                }
            }
            return result;
        }

        private int GetIDOfCustomer(string ID)
        {
            return int.Parse(ID.Split('H')[1]);
        }

        private int ConvertStringToInt(string value)
        {
            string[] str = value.Trim().Split(',');
            string temp = "";
            int result;
            for (int i = 0; i < str.Length; i++)
            {
                temp += str[i];
            }
            result = int.Parse(temp);
            return result;
        }

        private void RefreshPayment()
        {
            txtMSDH.ResetText();
            txtPay.Text = "0";
            txtNoteMoney.Text = "0";
        }

        private void RefreshALL()
        {
            dataDS.Rows.Clear();
            dataHD.Rows.Clear();
            listInput.Clear();
            listOutput.Clear();
            txtCustomerName.Text = string.Empty;
            txtMSKH.Items.Clear();
            txtMSKH.Text = string.Empty;
            RefreshPayment();
        }

        private void RefreshOfUpdate()
        {
            loadHD();
            if (txtCustomerType.Text == "Nhập hàng")
            {
                txtID.Text = "NH" + GetMSTTofInput().ToString("d6");
            }
            else
            {
                txtID.Text = "XH" + GetMSTTofOutput().ToString("d6");
            }
            txtCustomerName.Text = string.Empty;
            txtMSKH.ResetText();
            txtPay.Text = "0";
            txtNoteMoney.Text = "0";
            txtDate.Value = DateTime.Now.Date;
            loadRefreshOfHD();
            btnEdit.Visible = false;
            tempClickDS = 0;
            btnRemove.Enabled = true;
            btnSave.Enabled = true;
        }

        private void loadCustomerType()
        {
            txtCustomerType.Items.Clear();
            var listDB = customerTypeBO.GetData(u => u.isDelete == false);
            foreach (var item in listDB)
            {
                txtCustomerType.Items.Add(item.Type);
            }
            txtCustomerType.Text = listDB.First().Type;
        }

        private void loadCustomerName()
        {
            txtCustomerName.Items.Clear();
            var listDB = customerBO.GetData(u => u.isDelete == false && u.TypeofCustomer.Type == txtCustomerType.Text.Trim(), u => u.TypeofCustomer);
            foreach (var item in listDB)
            {
                txtCustomerName.Items.Add(item.Name);
            }
        }

        private void loadDay(int month, int year)
        {
            int day = DateTime.DaysInMonth(year, month);
            int maxDay = DateTime.DaysInMonth(year, month);
            int txtDay = int.Parse(txtNgay.Text);
            txtNgay.Items.Clear();
            for (int i = 1; i <= day; i++)
            {
                txtNgay.Items.Add(i);
            }
            txtNgay.Items.Add(string.Empty);
            if (maxDay < txtDay)
            {
                txtNgay.Text = "";
            }
        }

        private void loadMonth()
        {
            txtThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                txtThang.Items.Add(i);
            }
        }

        private void loadYear()
        {
            int Year = DateTime.Now.Year;
            for (int i = Year - 10; i <= Year; i++)
            {
                txtNam.Items.Add(i);
            }
        }

        private void loadDayMonthYear()
        {
            txtNgay.Text = DateTime.Now.Day.ToString();
            txtThang.Text = DateTime.Now.Month.ToString();
            txtNam.Text = DateTime.Now.Year.ToString();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            loadDay(month, year);
            loadMonth();
            loadYear();
        }

        private void loadRefreshOfHD()
        {
            txtMSDH.ResetText();
            txtNoteMoney.Text = "0";
            txtPay.Text = "0";
        }

        private void loadHD()
        {
            try
            {
                loadRefreshOfHD();
                dataHD.Rows.Clear();
                if (txtCustomerType.Text == "Nhập hàng")
                {
                    int ID = int.Parse(txtMSKH.Text.Trim().Split('H')[1]);
                    int i = 0;
                    List<Data.ProductInput> listDB = new List<Data.ProductInput>();
                    if (txtNgay.Text != string.Empty)
                    {
                        int Day = int.Parse(txtNgay.Text);
                        int Month = int.Parse(txtThang.Text);
                        int year = int.Parse(txtNam.Text);
                        listDB = productInputBO.GetData(u => u.isDelete == false && u.MSKH == ID && u.Date.Value.Day == Day && u.Date.Value.Month == Month && u.Date.Value.Year == year && u.Paid == false).ToList();
                    }
                    else
                    {
                        int Month = int.Parse(txtThang.Text);
                        int year = int.Parse(txtNam.Text);
                        listDB = productInputBO.GetData(u => u.isDelete == false && u.MSKH == ID && u.Date.Value.Month == Month && u.Date.Value.Year == year && u.Paid == false).ToList();
                    }
                    foreach (var item in listDB)
                    {
                        dataHD.Rows.Add();
                        dataHD.Rows[i].Cells[0].Value = "HD" + item.ID.ToString("d6");
                        //color
                        if (listInput.Count != 0)
                        {
                            int Payment = listInput.Where(u => u.MSDH == item.ID).Sum(u => u.Payment).Value;
                            if (Payment == item.TotalAmount - item.Payed)
                            {
                                dataHD.Rows[i].Cells[0].Style.BackColor = Color.Green;
                            }
                            if (Payment != 0 && Payment < item.TotalAmount - item.Payed)
                            {
                                dataHD.Rows[i].Cells[0].Style.BackColor = Color.Orange;
                            }
                        }
                        //endColor
                        dataHD.Rows[i].Cells[1].Value = item.Date.Value;
                        dataHD.Rows[i].Cells[2].Value = item.MSXE;
                        dataHD.Rows[i].Cells[3].Value = item.ProductName;
                        dataHD.Rows[i].Cells[4].Value = item.ProductWeight;
                        dataHD.Rows[i].Cells[5].Value = item.ProductPrice;
                        dataHD.Rows[i].Cells[6].Value = item.TotalAmount - item.Payed;
                        i++;
                    }
                }
                else
                {
                    int ID = int.Parse(txtMSKH.Text.Trim().Split('H')[1]);
                    int i = 0;
                    List<Data.ProductOutput> listDB = new List<Data.ProductOutput>();
                    if (txtNgay.Text != string.Empty)
                    {
                        int Day = int.Parse(txtNgay.Text);
                        int Month = int.Parse(txtThang.Text);
                        int year = int.Parse(txtNam.Text);
                        listDB = productOutputBO.GetData(u => u.isDelete == false && u.MSKH == ID && u.Date.Value.Day == Day && u.Date.Value.Month == Month && u.Date.Value.Year == year && u.Paid == false).ToList();
                    }
                    else
                    {
                        int Month = int.Parse(txtThang.Text);
                        int year = int.Parse(txtNam.Text);
                        listDB = productOutputBO.GetData(u => u.isDelete == false && u.MSKH == ID && u.Date.Value.Month == Month && u.Date.Value.Year == year && u.Paid == false).ToList();
                    }
                    foreach (var item in listDB)
                    {
                        dataHD.Rows.Add();
                        dataHD.Rows[i].Cells[0].Value = "HD" + item.ID.ToString("d6");
                        //color
                        if (listOutput.Count != 0)
                        {
                            int Payment = listOutput.Where(u => u.MSDH == item.ID).Sum(u => u.Payment).Value;
                            if (Payment == item.TotalAmount - item.Payed)
                            {
                                dataHD.Rows[i].Cells[0].Style.BackColor = Color.Green;
                            }
                            if (Payment != 0 && Payment < item.TotalAmount - item.Payed)
                            {
                                dataHD.Rows[i].Cells[0].Style.BackColor = Color.Orange;
                            }
                        }
                        //endColor
                        dataHD.Rows[i].Cells[1].Value = item.Date.Value;
                        dataHD.Rows[i].Cells[2].Value = item.MSXE;
                        dataHD.Rows[i].Cells[3].Value = item.ProductName;
                        dataHD.Rows[i].Cells[4].Value = item.ProductWeight;
                        dataHD.Rows[i].Cells[5].Value = item.ProductPrice;
                        dataHD.Rows[i].Cells[6].Value = item.TotalAmount - item.Payed;
                        i++;
                    }
                }
            }
            catch { }
        }

        private int loadMoney(string msdh)
        {
            int result = 0;
            if (txtCustomerType.Text.Trim() == "Nhập hàng")
            {
                //if (txtMSDH.Text != string.Empty)
                //{
                msdh = msdh.Split('D')[1];
                int mshd = int.Parse(msdh);
                int amount = productInputBO.GetData(u => u.isDelete == false && u.ID == mshd).First().TotalAmount.Value;
                int paid = paymentInputBO.GetData(u => u.isDelete == false && u.MSDH == mshd).ToList().Sum(u => u.Payment).Value;
                return amount - paid;
                //}
            }
            else
            {
                return 123;
            }
            return result;
        }

        private Data.PaymentInput loadListInput()
        {
            paymentInput = new Data.PaymentInput();
            paymentInput.ID = int.Parse(txtID.Text.Substring(2));
            paymentInput.Date = txtDate.Value.Date;
            paymentInput.MSDH = int.Parse(txtMSDH.Text.Trim().Substring(2));
            paymentInput.Payment = ConvertStringToInt(txtPay.Text);
            paymentInput.isDelete = false;

            return paymentInput;
        }

        private Data.PaymentOutput loadListOutput()
        {
            paymentOutput = new Data.PaymentOutput();
            paymentOutput.ID = int.Parse(txtID.Text.Substring(2));
            paymentOutput.Date = txtDate.Value.Date;
            paymentOutput.MSDH = int.Parse(txtMSDH.Text.Trim().Substring(2));
            paymentOutput.Payment = ConvertStringToInt(txtPay.Text);
            paymentOutput.isDelete = false;
            return paymentOutput;
        }

        private void loadDG()
        {
            dataDS.Rows.Clear();
            int i = 0;
            foreach (var item in listInput)
            {
                int mskh = productInputBO.GetData(u => u.ID == item.MSDH).First().MSKH;
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = "NH" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = item.Date.ToString();
                dataDS.Rows[i].Cells[2].Value = "KH" + mskh.ToString("d6");
                dataDS.Rows[i].Cells[3].Value = customerBO.GetNameByID(mskh);
                dataDS.Rows[i].Cells[4].Value = "DH" + item.MSDH.Value.ToString("d6");
                dataDS.Rows[i].Cells[5].Value = item.Payment.ToString();
                i++;
            }

            foreach (var item in listOutput)
            {
                int mskh = productOutputBO.GetData(u => u.ID == item.MSDH).First().MSKH.Value;
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = "XH" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = item.Date.ToString();
                dataDS.Rows[i].Cells[2].Value = "KH" + mskh.ToString("d6");
                dataDS.Rows[i].Cells[3].Value = customerBO.GetNameByID(mskh);
                dataDS.Rows[i].Cells[4].Value = "DH" + item.MSDH.Value.ToString("d6");
                dataDS.Rows[i].Cells[5].Value = item.Payment.ToString();
                i++;
            }
        }

        #endregion Support

        public MCThanhtoan()
        {
            InitializeComponent();
        }

        private void MCThanhtoan_Load(object sender, EventArgs e)
        {
            loadDayMonthYear();
            loadCustomerType();
            loadCustomerName();
            txtNgay.ResetText();
            txtPay.Text = "0";
            txtNoteMoney.Text = "0";
            txtID.Text = "NH" + paymentInputBO.GetID().ToString("d6");
        }

        #region Event Information

        private void txtCustomerType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtCustomerType.Text == "Nhập hàng")
            {
                laBill.Text = "HÓA ĐƠN NHẬP HÀNG";
                txtID.Text = "NH" + GetMSTTofInput().ToString("d6");
            }
            if (txtCustomerType.Text == "Xuất hàng")
            {
                laBill.Text = "HÓA ĐƠN XUẤT HÀNG";
                txtID.Text = "XH" + GetMSTTofOutput().ToString("d6");
            }
            loadCustomerName();
            txtCustomerName.ResetText();
            txtMSDH.ResetText();
            txtMSKH.ResetText();
            dataHD.Rows.Clear();
        }

        private void txtCustomerName_SelectedValueChanged(object sender, EventArgs e)
        {
            string CustomerName = txtCustomerName.Text;
            if (CustomerName != string.Empty)
            {
                txtMSKH.Items.Clear();
                var listDB = customerBO.GetIDByName(CustomerName);
                foreach (var item in listDB)
                {
                    txtMSKH.Items.Add("KH" + item.ToString("d6"));
                }
                txtMSKH.Text = "KH" + listDB.First().ToString("d6");
                loadHD();
            }
        }

        #endregion Event Information

        #region Event Infomation Employee

        private void txtThang_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int month = int.Parse(txtThang.Text);
                int year = int.Parse(txtNam.Text);
                if (e.KeyCode == Keys.Enter)
                {
                    if (month > 12 || month <= 0)
                    {
                        txtThang.Text = "";
                    }
                    else
                    {
                        loadDay(month, year);
                        txtNam.Focus();
                    }
                }
                if (e.KeyCode == Keys.Space)
                {
                    if (month > 12 || month <= 0)
                    {
                        txtThang.Text = "";
                    }
                }
            }
            catch { }
        }

        private void txtThang_SelectedValueChanged(object sender, EventArgs e)
        {
            //Ignore errors when the program compiles for the first time
            try
            {
                int month = int.Parse(txtThang.Text);
                int year = int.Parse(txtNam.Text);
                if (txtNgay.Text == string.Empty)
                {
                    txtNgay.Items.Clear();
                    for (int i = 1; i <= 31; i++)
                    {
                        txtNgay.Items.Add(i);
                    }
                    txtNgay.Items.Add(string.Empty);
                }
                else
                {
                    loadDay(month, year);
                }
                loadHD();
            }
            catch
            {
            }
        }

        private void txtNgay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int day = int.Parse(txtNgay.Text);
                int txtmonth = int.Parse(txtThang.Text);
                int txtYear = int.Parse(txtNam.Text);
                int maxDay = DateTime.DaysInMonth(txtYear, 2);
                IEnumerable<int> month = TimekeepingBO.Month(day);
                txtThang.Items.Clear();
                txtThang.Text = txtThang.Text;
                foreach (var item in month)
                {
                    txtThang.Items.Add(item);
                }
                if (maxDay < int.Parse(txtNgay.Text))
                {
                    txtThang.Items.Remove(2);
                }
            }
            catch { }
        }

        private void txtNam_SelectedValueChanged(object sender, EventArgs e)
        {
            int month = int.Parse(txtThang.Text);
            int year = int.Parse(txtNam.Text);
            loadDay(month, year);
            loadHD();
        }

        private void txtNgay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNgay_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int numDay = int.Parse(txtNgay.Text);
                int numYear = int.Parse(txtNam.Text);
                int numMonth = int.Parse(txtThang.Text);
                int maxDay = DateTime.DaysInMonth(numYear, numMonth);
                if (e.KeyCode == Keys.Enter)
                {
                    if (numDay > maxDay)
                    {
                        txtNgay.Text = "";
                    }
                    txtThang.Focus();
                }
            }
            catch { }
        }

        private void txtThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNam_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                int Year = DateTime.Now.Year;
                int Year10 = Year - 10;
                int numYear = int.Parse(txtNam.Text);
                if (numYear > Year || numYear < Year10)
                {
                    txtNam.Text = Year.ToString();
                }
            }
        }

        private void txtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion Event Infomation Employee

        #region Event DataGridView Bill

        private void dataHD_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataHD.Rows.Count - 1)
                {
                    txtMSDH.Text = dataHD.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNoteMoney.Text = string.Format("{0:0,0 VND}", CheckPayment());
                    txtPay.Text = CheckPayment().ToString();
                }
                else
                {
                    txtMSDH.Text = string.Empty;
                    txtPay.Text = 0.ToString(); ;
                    txtNoteMoney.Text = string.Empty;
                }
            }
            catch { }
        }

        #endregion Event DataGridView Bill

        #region Event BIll

        private void txtMSKH_SelectedValueChanged(object sender, EventArgs e)
        {
            loadHD();
        }

        private void txtNgay_SelectedValueChanged(object sender, EventArgs e)
        {
            loadHD();
        }

        #endregion Event BIll

        #region Event Pay

        private void txtPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtPay.Text.Length > 12)
            //{
            //    e.Handled = true;
            //}
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void txtPay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                List<string> arrList = new List<string>();
                Int64 phannguyen = 0;
                Int64 money = 0;
                bool checkNum = Int64.TryParse(txtPay.Text, out money);
                if (checkNum == false)
                {
                    string[] str = txtPay.Text.Split(',');
                    string resStr = "";
                    foreach (var item in str)
                    {
                        resStr += item;
                    }
                    money = Int64.Parse(resStr);
                }
                do
                {
                    phannguyen = money % 1000;
                    money = money / 1000;
                    if (money != 0)
                    {
                        arrList.Add("," + phannguyen.ToString("d3"));
                    }
                    if (money == 0)
                    {
                        arrList.Add(phannguyen.ToString());
                    }
                }
                while (money != 0);
                for (int i = arrList.Count - 1; i >= 0; i--)
                {
                    result += arrList[i];
                }
                txtPay.Text = result;
                txtPay.SelectionStart = result.Length;
            }
            catch
            { }
        }

        private void txtPay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
            }
        }

        #endregion Event Pay

        #region Button

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtCustomerType.Text == "Nhập hàng")
            {
                if (txtCustomerName.Text != string.Empty && txtMSKH.Text != string.Empty && txtMSDH.Text != string.Empty)
                {
                    if (CheckPayment() - ConvertStringToInt(txtPay.Text) >= 0)
                    {
                        //color datads bill
                        if (CheckPayment() - ConvertStringToInt(txtPay.Text) > 0)
                        {
                            dataHD.Rows[dataHD.CurrentRow.Index].Cells[0].Style.BackColor = Color.Orange;
                        }
                        if (CheckPayment() - ConvertStringToInt(txtPay.Text) == 0)
                        {
                            dataHD.Rows[dataHD.CurrentRow.Index].Cells[0].Style.BackColor = Color.Green;
                        }
                        //
                        if (CheckPayment() == 0)
                        {
                            MessageBox.Show("Hóa đơn đã thanh toán rồi. Vui lòng kiểm tra lại");
                        }
                        else
                        {
                            listInput.Add(loadListInput());
                            loadDG();
                            txtID.Text = "NH" + GetMSTTofInput().ToString("d6");
                            RefreshPayment();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ban chỉ cần thanh toán " + CheckPayment() + "là đủ");
                        txtPay.Text = CheckPayment().ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
            }
            else
            {
                if (txtCustomerName.Text != string.Empty && txtMSKH.Text != string.Empty && txtMSDH.Text != string.Empty)
                {
                    if (CheckPayment() - ConvertStringToInt(txtPay.Text) >= 0)
                    {
                        //color datads bill
                        if (CheckPayment() - ConvertStringToInt(txtPay.Text) > 0)
                        {
                            dataHD.Rows[dataHD.CurrentRow.Index].Cells[0].Style.BackColor = Color.Orange;
                        }
                        if (CheckPayment() - ConvertStringToInt(txtPay.Text) == 0)
                        {
                            dataHD.Rows[dataHD.CurrentRow.Index].Cells[0].Style.BackColor = Color.Green;
                        }
                        //
                        if (CheckPayment() == 0)
                        {
                            MessageBox.Show("Hóa đơn đã thanh toán rồi. Vui lòng kiểm tra lại");
                        }
                        else
                        {
                            listOutput.Add(loadListOutput());
                            loadDG();
                            txtID.Text = "XH" + GetMSTTofOutput().ToString("d6");
                            RefreshPayment();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ban chỉ cần thanh toán " + CheckPayment() + "là đủ");
                        txtPay.Text = CheckPayment().ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
            }
        }

        #endregion Button

        //Tong hop khoa key
        private void txtCustomerType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            paymentInputBO.Add(listInput);
            foreach (var item in listOutput)
            {
                paymentOutputBO.Add(item);
            }
            RefreshALL();
        }

        private void txtCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dataDS.RowCount - 1)
            {
                switch (tempClickDS)
                {
                    case 0:
                        try
                        {
                            string CustomerType = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 2);
                            if (CustomerType == "NH")
                            {
                                txtCustomerType.Text = "Nhập hàng";
                            }
                            else
                            {
                                txtCustomerType.Text = "Xuất hàng";
                            }
                            txtID.Text = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                            txtDate.Value = DateTime.Parse(dataDS.Rows[e.RowIndex].Cells[1].Value.ToString());
                            txtMSKH.Text = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
                            txtCustomerName.Text = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                            txtMSDH.Text = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString();
                            txtPay.Text = dataDS.Rows[e.RowIndex].Cells[5].Value.ToString();
                            btnEdit.Visible = true;
                            btnRemove.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        catch
                        {
                        }
                        tempClickDS = 1;
                        break;

                    case 1:
                        if (txtCustomerType.Text == "Nhập hàng")
                        {
                            txtID.Text = "NH" + GetMSTTofInput().ToString("d6");
                        }
                        else
                        {
                            txtID.Text = "XH" + GetMSTTofOutput().ToString("d6");
                        }
                        btnEdit.Visible = false;
                        tempClickDS = 0;
                        btnRemove.Enabled = true;
                        btnSave.Enabled = true;
                        txtPay.Text = "0";
                        txtNoteMoney.Text = "0";
                        txtMSDH.ResetText();
                        break;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCustomerType.Text == "Nhập hàng")
            {
                paymentInput = new Data.PaymentInput();
                int Id = int.Parse(txtID.Text.Trim().Substring(2));
                paymentInput = listInput.Find(u => u.ID == Id);
                paymentInput.Date = txtDate.Value.Date;
                paymentInput.Payment = ConvertStringToInt(txtPay.Text);
            }
            else
            {
                paymentOutput = new Data.PaymentOutput();
                int Id = int.Parse(txtID.Text.Trim().Substring(2));
                paymentOutput = listOutput.Find(u => u.ID == Id);
                paymentOutput.Date = txtDate.Value.Date;
                paymentOutput.Payment = ConvertStringToInt(txtPay.Text);
            }
            loadDG();
            RefreshOfUpdate();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (IDOfThanhToan != string.Empty)
            {
                string Type = IDOfThanhToan.Substring(0, 2);
                if (Type == "NH")
                {
                    string masseage = "Bạn có muốn xóa hóa đơn thanh toán " + IDOfThanhToan + "không ?";
                    string Title = "Chú ý";
                    DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        int ID = int.Parse(IDOfThanhToan.Substring(2));
                        paymentInput = listInput.Find(u => u.ID == ID);
                        listInput.Remove(paymentInput);
                        loadHD();
                        loadDG();
                    }
                }
                else
                {
                    string masseage = "Bạn có muốn xóa hóa đơn thanh toán " + IDOfThanhToan + "không ?";
                    string Title = "Chú ý";
                    DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        int ID = int.Parse(IDOfThanhToan.Substring(2));
                        paymentOutput = listOutput.Find(u => u.ID == ID);
                        listOutput.Remove(paymentOutput);
                        loadHD();
                        loadDG();
                    }
                }
                IDOfThanhToan = string.Empty;
            }
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataDS.Rows.Count - 1)
                {
                    IDOfThanhToan = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                }
            }
            catch
            {
                IDOfThanhToan = string.Empty;
            }
        }
    }
}