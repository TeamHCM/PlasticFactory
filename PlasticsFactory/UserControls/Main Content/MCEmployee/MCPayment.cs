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

        private EmployeeBO employeeBO = new EmployeeBO();
        private TimekeepingBO timekeepingBO = new TimekeepingBO();
        private PreferceProductPriceBO preferceProductPriceBO = new PreferceProductPriceBO();
        private EmployeePaymentBO employeePaymentBO = new EmployeePaymentBO();
        private List<Data.EmployeePayment> list = new List<Data.EmployeePayment>();
        private int employeeDebt = 0;
        private Data.EmployeePayment employeePayment = new Data.EmployeePayment();
        #endregion Generate Field

        #region Support
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

        private void loadMSNV()
        {
            txtMSNV.Items.Clear();
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            foreach (var item in listDB)
            {
                txtMSNV.Items.Add(item.MSNV);
            }
        }

        private void loadCustomerName()
        {
            txtEmployeeName.Items.Clear();
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            foreach (var item in listDB)
            {
                txtEmployeeName.Items.Add(item.Hoten);
            }
        }

        private void loadDetailWork()
        {
            if (txtMSNV.Text != string.Empty)
            {
                int i = 0;
                double TotalTime = 0;
                int TotalProductWeight = 0;
                int TotalCash = 0;
                int Debt = 0;
                if (int.Parse(txtMonth.Text) == 1)
                {
                    try
                    {
                        Debt = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text && u.DATE.Value.Month == 12 && u.DATE.Value.Year == (int.Parse(txtYear.Text) - 1)).FirstOrDefault().NEBT;
                    }
                    catch
                    {
                        Debt = 0;
                    }
                }
                else
                {
                    try
                    {
                        Debt = employeePaymentBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text && u.DATE.Value.Month == int.Parse(txtMonth.Text) && u.DATE.Value.Year == int.Parse(txtYear.Text)).FirstOrDefault().NEBT;
                    }
                    catch
                    {
                        Debt = 0;
                    }
                }
                dataDetailWork.Rows.Clear();
                var listDB = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text && u.Date.Value.Month == int.Parse(txtMonth.Text) && u.Date.Value.Year == int.Parse(txtYear.Text));
                foreach (var item in listDB)
                {
                    dataDetailWork.Rows.Add();
                    dataDetailWork.Rows[i].Cells[0].Value = item.Date.Value.Day + "/" + item.Date.Value.Month;
                    dataDetailWork.Rows[i].Cells[1].Value = item.TimeStart + " - " + item.TimeEnd;
                    dataDetailWork.Rows[i].Cells[2].Value = item.Time;
                    dataDetailWork.Rows[i].Cells[3].Value = item.TotalWeight;
                    dataDetailWork.Rows[i].Cells[4].Value = item.AdvancePayment;
                    dataDetailWork.Rows[i].Cells[5].Value = item.Note;
                    TotalTime += item.Time.Value;
                    TotalProductWeight += int.Parse(item.TotalWeight);
                    TotalCash += item.AdvancePayment.Value;
                    i++;
                }
                txtDayWork.Text = i.ToString();
                txtCash.Text = string.Format("{0:n0}", TotalCash)+" (VNĐ)";
                txtTime.Text = TotalTime.ToString();
                txtProduct.Text = TotalProductWeight.ToString();
                txtDebt.Text = Debt.ToString();
                txtNoteMoney.Text = string.Format("{0:n0}",(TotalTime * preferceProductPriceBO.GetData(u => u.Name == "Thời gian").First().Price.Value + TotalProductWeight * preferceProductPriceBO.GetData(u => u.Name == "Sản phẩm").First().Price.Value-TotalCash-Debt));
                int Pay=(int)(TotalTime * preferceProductPriceBO.GetData(u => u.Name == "Thời gian").First().Price.Value + TotalProductWeight * preferceProductPriceBO.GetData(u => u.Name == "Sản phẩm").First().Price.Value - TotalCash - Debt);
                if (Pay < 0)
                {
                    MessageBox.Show("Do tháng này nhân viên nợ tiền" + Pay * (-1) + " nên tiền thanh toán sẽ 0");
                    txtPay.Text = 0.ToString();
                    //cần lưu lại nợ bằng một biến .Chw không lấy txtDebt vì cái này là nợ của tháng trước=> Cấn biến lưu 
                    employeeDebt = Pay * (-1);
                }
                else
                {
                    txtPay.Text = (TotalTime * preferceProductPriceBO.GetData(u => u.Name == "Thời gian").First().Price.Value + TotalProductWeight * preferceProductPriceBO.GetData(u => u.Name == "Sản phẩm").First().Price.Value - TotalCash - Debt).ToString();
                    employeeDebt = 0;
                }
                }
        }

        private void loadMonth()
        {
            txtMonth.Items.Clear();
            for(int i=1;i<=12;i++)
            {
                txtMonth.Items.Add(i);
            }
        }

        private void loadYear()
        {
            txtYear.Items.Clear();
            int yearEnd = DateTime.Now.Year;
            int yearStart = yearEnd - 10;
            for(int i=yearStart;i<=yearEnd;i++)
            {
                txtYear.Items.Add(i);
            }
        }

        private Data.EmployeePayment InputEmployeePayment()
        {
            employeePayment = new Data.EmployeePayment();
            employeePayment.ID = employeePaymentBO.GetID();
            employeePayment.MSNV = txtMSNV.Text;
            employeePayment.NEBT = int.Parse(txtDebt.Text);
            employeePayment.PAY = ConvertStringToInt(txtPay.Text);
            employeePayment.DATE = txtDate.Value.Date;
            return employeePayment;
        }

        private void loadDG(List<Data.EmployeePayment> list)
        {
            dataDS.Rows.Clear();
            int i = 0;
            foreach(var item in list)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = item.ID;
                dataDS.Rows[i].Cells[1].Value = item.DATE.Value.ToShortDateString();
                dataDS.Rows[i].Cells[2].Value = item.MSNV;
                dataDS.Rows[i].Cells[3].Value = employeeBO.GetNameByID(item.MSNV);
                dataDS.Rows[i].Cells[4].Value = item.PAY;
                dataDS.Rows[i].Cells[5].Value = item.NEBT;
            }
        }
        #endregion Support

        public MCPaymentEmployee()
        {
            InitializeComponent();
        }

        private void MCPayment_Load(object sender, EventArgs e)
        {
            txtID.Text = "TT"+employeePaymentBO.GetID().ToString("d6");
            loadMSNV();
            loadCustomerName();
            loadMonth();
            loadYear();
            txtProduct.Text = "0";
            txtTime.Text = "0";
            txtPay.Text = "0";
            txtCash.Text = "0";
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMoneyOfProduct.Text = preferceProductPriceBO.GetData(u => u.Name.Trim() == "Sản phẩm").First().Price.Value.ToString();
            txtMoneyOfTime.Text= preferceProductPriceBO.GetData(u => u.Name.Trim() == "Thời gian").First().Price.Value.ToString();
            txtMoneyOfProduct.Items.Add(preferceProductPriceBO.GetData(u => u.Name.Trim() == "Sản phẩm").First().Price.Value.ToString());
            txtMoneyOfTime.Items.Add(preferceProductPriceBO.GetData(u => u.Name.Trim() == "Thời gian").First().Price.Value.ToString());
        }

        private void txtMSNV_SelectedValueChanged(object sender, EventArgs e)
        {
            txtEmployeeName.Text = employeeBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text).First().Hoten;
            loadDetailWork();
        }

        private void txtEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMSNV.Text = employeeBO.GetData(u => u.isDelete == false && u.Hoten.Trim() == txtEmployeeName.Text.Trim()).First().MSNV;
            loadDetailWork();
        }

        private void txtMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtMSNV.Text != string.Empty)
            {
                loadDetailWork();
            }
        }

        private void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtMSNV.Text!=string.Empty)
            {
                loadDetailWork();
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

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            list.Add(InputEmployeePayment());
            loadDG(list);
        }
    }
}