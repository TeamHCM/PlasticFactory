using BUS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCCustomer
{
    public partial class MCCustomerMG : UserControl
    {
        #region Generate Fiedld

        private PaymentInputBO paymentInputBO = new PaymentInputBO();
        private PaymentOutputBO paymentOutputBO = new PaymentOutputBO();
        public ProductBO productIpBO = new ProductBO();
        public ProductInputBO productInputBO = new ProductInputBO();
        public ProductOBO productOBO = new ProductOBO();
        public ProductOutputBO productOutputBO = new ProductOutputBO();
        public CustomerBO customerBO = new CustomerBO();
        public CustomerTypeBO customerTypeBO = new CustomerTypeBO();
        public static int DoubleClickRow = 0;
        public int curMSTT = 0;

        public struct Parameter
        {
            public int MSTT { get; set; }
            public string MSHDOfType { get; set; }
        }

        private Parameter parameter = new Parameter();

        #endregion Generate Fiedld

        #region Support

        private void loadProductType()
        {
            var customerType = customerTypeBO.GetData(u => u.isDelete == false);
            if (txtCustomerType.Text == customerType[0].Type)
            {
                txtProductType.Items.Clear();
                var listDB = productIpBO.GetData(u => u.isDelete == false);
                foreach (var item in listDB)
                {
                    txtProductType.Items.Add(item.Name);
                }
                txtProductType.Items.Add("All");
                txtProductType.Text = "All";
            }
            if (txtCustomerType.Text == customerType[1].Type)
            {
                txtProductType.Items.Clear();
                var listDB = productOBO.GetData(u => u.isDelete == false);
                foreach (var item in listDB)
                {
                    txtProductType.Items.Add(item.Name);
                }
                txtProductType.Items.Add("All");
                txtProductType.Text = "All";
            }
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
            txtCustomerName.Items.Add("All");
            txtCustomerName.Text = "All";
        }

        public void loadDay(int month, int year)
        {
            int day = DateTime.DaysInMonth(year, month);
            int maxDay = DateTime.DaysInMonth(year, month);
            int txtDays = int.Parse(txtDay.Text);
            txtDay.Items.Clear();
            for (int i = 1; i <= day; i++)
            {
                txtDay.Items.Add(i);
            }
            if (maxDay < txtDays)
            {
                txtDay.Text = "";
            }
            txtDay.Items.Add("");
        }

        public void loadMonth()
        {
            txtMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                txtMonth.Items.Add(i);
            }
            txtMonth.Items.Add("");
        }

        public void loadYear()
        {
            int Year = DateTime.Now.Year;
            for (int i = Year - 10; i <= Year; i++)
            {
                txtYear.Items.Add(i);
            }
        }

        public void loadDayMonthYear()
        {
            txtDay.Text = DateTime.Now.Day.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            loadDay(month, year);
            loadMonth();
            loadYear();
        }

        public void loadInputDG(List<Data.PaymentInput> list)
        {
            int i = 0;
            dataDS.Rows.Clear();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                Data.ProductInput productInput = new Data.ProductInput();
                productInput = productInputBO.GetData(u => u.isDelete == false && u.ID == item.MSDH).First();
                dataDS.Rows[i].Cells[0].Value = "TT" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = item.Date.Value.Date.ToShortDateString();
                dataDS.Rows[i].Cells[2].Value = "KH" + productInput.MSKH.ToString("d6");
                dataDS.Rows[i].Cells[3].Value = customerBO.GetNameByID(productInput.MSKH);
                dataDS.Rows[i].Cells[4].Value = "NH" + item.MSDH.Value.ToString("d6");
                dataDS.Rows[i].Cells[5].Value = productInput.ProductName;
                dataDS.Rows[i].Cells[6].Value = productInput.Date.Value.Date.ToShortDateString();
                dataDS.Rows[i].Cells[7].Value = productInput.ProductWeight;
                dataDS.Rows[i].Cells[8].Value = productInput.ProductPrice;
                dataDS.Rows[i].Cells[9].Value = productInput.TotalAmount;
                dataDS.Rows[i].Cells[10].Value = productInput.MSXE;
                dataDS.Rows[i].Cells[11].Value = item.Payment;
                i++;
            }
        }

        public void loadOutputDG(List<Data.PaymentOutput> list)
        {
            int i = 0;
            dataDS.Rows.Clear();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                Data.ProductOutput productOutput = new Data.ProductOutput();
                productOutput = productOutputBO.GetData(u => u.isDelete == false && u.ID == item.MSDH).First();
                dataDS.Rows[i].Cells[0].Value = "TT" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = item.Date.Value.Date.ToShortDateString();
                dataDS.Rows[i].Cells[2].Value = "KH" + productOutput.MSKH.Value.ToString("d6");
                dataDS.Rows[i].Cells[3].Value = customerBO.GetNameByID(productOutput.MSKH.Value);
                dataDS.Rows[i].Cells[4].Value = "XH" + item.MSDH.Value.ToString("d6");
                dataDS.Rows[i].Cells[5].Value = productOutput.ProductName;
                dataDS.Rows[i].Cells[6].Value = productOutput.Date.Value.Date.ToShortDateString();
                dataDS.Rows[i].Cells[7].Value = productOutput.ProductWeight;
                dataDS.Rows[i].Cells[8].Value = productOutput.ProductPrice;
                dataDS.Rows[i].Cells[9].Value = productOutput.TotalAmount;
                dataDS.Rows[i].Cells[10].Value = productOutput.MSXE;
                dataDS.Rows[i].Cells[11].Value = item.Payment;
                i++;
            }
        }

        public void loadTotalWeightInput(List<Data.ProductInput> list)
        {
            double result = 0;
            foreach (var item in list)
            {
                result += item.ProductWeight;
            }
            txtTotalWeight.Text = string.Format("{0:#,##0.##}", result) + " (KG)";
        }

        public void loadTotalAmountInput(List<Data.ProductInput> list)
        {
            double result = 0;
            foreach (var item in list)
            {
                result += item.TotalAmount;
            }
            txtTotalAmount.Text = string.Format("{0:#,##0.##}", result) + " (VNĐ)";
        }

        public void loadTotalPayInput(List<Data.PaymentInput> list)
        {
            Int64 result = 0;
            foreach (var item in list)
            {
                result += item.Payment.Value;
            }
            txtTotalPay.Text = string.Format("{0:#,##0.##}", result) + " (VNĐ)";
        }

        public void loadTotalWeightOutput(List<Data.ProductOutput> list)
        {
            double result = 0;
            foreach (var item in list)
            {
                result += item.ProductWeight;
            }
            txtTotalWeight.Text = string.Format("{0:#,##0.##}", result) + " (KG)";
        }

        public void loadTotalAmountOutput(List<Data.ProductOutput> list)
        {
            double result = 0;
            foreach (var item in list)
            {
                result += item.TotalAmount;
            }
            txtTotalAmount.Text = string.Format("{0:#,##0.##}", result) + " (VNĐ)";
        }

        public void loadTotalPayOutput(List<Data.PaymentOutput> list)
        {
            Int64 result = 0;
            foreach (var item in list)
            {
                result += item.Payment.Value;
            }
            txtTotalPay.Text = string.Format("{0:#,##0.##}", result) + " (VNĐ)";
        }

        #endregion Support

        public MCCustomerMG()
        {
            InitializeComponent();
        }

        private void MCCustomerMG_Load(object sender, EventArgs e)
        {
            loadCustomerType();
            loadCustomerName();
            loadDayMonthYear();
            int month = int.Parse(txtMonth.Text);
            int year = int.Parse(txtYear.Text);
            txtDay.Text = "";
            List<Data.PaymentInput> listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
            List<Data.ProductInput> list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
            loadTotalAmountInput(list);
            loadTotalWeightInput(list);
            loadTotalPayInput(listDB);
            loadInputDG(listDB);
        }

        #region Event Customer , Product

        private void txtCustomerType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDay.Text = string.Empty;
                txtMonth.Text = DateTime.Now.Month.ToString(); ;
                txtYear.Text = DateTime.Now.Year.ToString();
                if (txtCustomerType.Text.Equals("Nhập hàng"))
                {
                    loadCustomerName();
                    loadProductType();
                    if (txtCustomerName.Text.Equals("All"))
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.ProductName == ProductName && u.Date.Value == date, u => u.ProductInput).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductInput).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.ProductName == ProductName && u.Date.Value.Year == year, u => u.ProductInput).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                    }
                    else
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.Date.Value == date, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.ProductInput.ProductName == ProductName && u.Date.Value == date, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.ProductInput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.ProductInput.ProductName == ProductName && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                    }
                }
                else
                {
                    loadCustomerName();
                    loadProductType();
                    if (txtCustomerName.Text.Equals("All"))
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.ProductName == ProductName && u.Date.Value == date).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                    }
                    else
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.Date.Value == date, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.ProductOutput.ProductName == ProductName && u.Date.Value == date, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.ProductOutput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.ProductOutput.ProductName == ProductName && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void txtCustomerName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerType.Text.Equals("Nhập hàng"))
                {
                    if (txtCustomerName.Text.Equals("All"))
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.ProductName == ProductName && u.Date.Value == date, u => u.ProductInput).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductInput).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.ProductName == ProductName && u.Date.Value.Year == year, u => u.ProductInput).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                    }
                    else
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.Date.Value == date, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.ProductInput.ProductName == ProductName && u.Date.Value == date, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.ProductInput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                    var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(list);
                                    loadTotalWeightInput(list);
                                    loadTotalPayInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = paymentInputBO.GetData(u => u.isDelete == false && u.ProductInput.Customer.Name == CustomerName && u.ProductInput.ProductName == ProductName && u.Date.Value.Year == year, u => u.ProductInput.Customer).ToList();
                                var list = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(list);
                                loadTotalWeightInput(list);
                                loadTotalPayInput(listDB);
                            }
                        }
                    }
                }
                else
                {
                    if (txtCustomerName.Text.Equals("All"))
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.ProductName == ProductName && u.Date.Value == date).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                    }
                    else
                    {
                        if (txtProductType.Text == "All")
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.Date.Value == date, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                        else
                        {
                            if (txtMonth.Text != string.Empty)
                            {
                                if (txtDay.Text != string.Empty)
                                {
                                    int day = int.Parse(txtDay.Text);
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    DateTime date = DateTime.Parse(day + "/" + month + "/" + year);
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.ProductOutput.ProductName == ProductName && u.Date.Value == date, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.ProductOutput.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                    var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(list);
                                    loadTotalWeightOutput(list);
                                    loadTotalPayOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = paymentOutputBO.GetData(u => u.isDelete == false && u.ProductOutput.Customer.Name == CustomerName && u.ProductOutput.ProductName == ProductName && u.Date.Value.Year == year, u => u.ProductOutput.Customer).ToList();
                                var list = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year && u.Payed != 0, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(list);
                                loadTotalWeightOutput(list);
                                loadTotalPayOutput(listDB);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void txtProductType_SelectedValueChanged(object sender, EventArgs e)
        {
            txtCustomerName_SelectedValueChanged(sender, e);
        }

        private void txtCustomerType_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion Event Customer , Product

        #region Date

        private void txtDay_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text == string.Empty)
            {
                txtDay.Text = string.Empty;
            }
            txtProductType_SelectedValueChanged(sender, e);
        }

        private void txtMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text == string.Empty)
            {
                txtDay.Text = string.Empty;
            }
            txtProductType_SelectedValueChanged(sender, e);
        }

        private void txtYear_SelectedValueChanged(object sender, EventArgs e)
        {
            txtProductType_SelectedValueChanged(sender, e);
        }

        private void txtDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion Date

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmEditCustomerPay frmEditCustomerPay = new frmEditCustomerPay();
            string MSTT = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
            string Date = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
            string MSHD = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString();
            string MSKH = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
            string CustomerName = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
            string ProductNames = dataDS.Rows[e.RowIndex].Cells[5].Value.ToString();
            int ProductWeigth = int.Parse(dataDS.Rows[e.RowIndex].Cells[7].Value.ToString());
            int Amount = int.Parse(dataDS.Rows[e.RowIndex].Cells[9].Value.ToString());
            int Payed = int.Parse(dataDS.Rows[e.RowIndex].Cells[11].Value.ToString());
            frmEditCustomerPay.Sender(MSTT, Date, MSHD, MSKH, CustomerName, ProductNames, ProductWeigth, Amount, Payed);
            frmEditCustomerPay.ShowDialog();
            txtCustomerName_SelectedValueChanged(sender, e);
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataDS.Rows.Count - 1)
                {
                    //curMSTT = int.Parse(dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Trim().Substring(2));
                    parameter = new Parameter();
                    parameter.MSTT = int.Parse(dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Trim().Substring(2));
                    parameter.MSHDOfType = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().Substring(0, 2);
                }
            }
            catch { }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (parameter.MSHDOfType != string.Empty && parameter.MSTT != 0)
            {
                string Type = parameter.MSHDOfType;
                if (Type == "NH")
                {
                    string masseage = "Bạn có muốn xóa hóa đơn thanh toán TT" + parameter.MSTT.ToString("d6") + " không ?";
                    string Title = "Chú ý";
                    DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        paymentInputBO.isDelete(parameter.MSTT);
                    }
                }
                else
                {
                    string masseage = "Bạn có muốn xóa hóa đơn thanh toán TT" + parameter.MSTT.ToString("d6") + " không ?";
                    string Title = "Chú ý";
                    DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        paymentOutputBO.isDelete(parameter.MSTT);
                    }
                }
                parameter = new Parameter();
                txtCustomerName_SelectedValueChanged(sender, e);
            }
        }
    }
}