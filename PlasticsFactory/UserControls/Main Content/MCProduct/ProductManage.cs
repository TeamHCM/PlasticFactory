using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlasticsFactory.Data;
using BUS.Business;

namespace PlasticsFactory.UserControls.Main_Content.MCProduct
{
    public partial class ProductManage : UserControl
    {

        #region Generate Field
        public ProductBO productIpBO = new ProductBO();
        public ProductInputBO productInputBO = new ProductInputBO();
        public ProductOBO productOBO = new ProductOBO();
        public ProductOutputBO productOutputBO = new ProductOutputBO();
        public CustomerBO customerBO = new CustomerBO();
        public CustomerTypeBO customerTypeBO = new CustomerTypeBO();
        public static int MSHD = 0;
        public static bool Input = false;
        #endregion

        #region Support
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
        public void loadInputDG(List<Data.ProductInput> list)
        {
            int i = 0;
            dataDS.Rows.Clear();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = "NH" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = "KH" + item.MSKH.ToString("D6");
                dataDS.Rows[i].Cells[2].Value = customerBO.GetNameByID(item.MSKH); ;
                dataDS.Rows[i].Cells[3].Value = item.Date.Value.ToShortDateString();
                dataDS.Rows[i].Cells[4].Value = item.MSXE;
                dataDS.Rows[i].Cells[5].Value = item.TruckWeight;
                dataDS.Rows[i].Cells[6].Value = item.ProductName;
                dataDS.Rows[i].Cells[7].Value = item.ProductWeight;
                dataDS.Rows[i].Cells[8].Value = item.TotalWeight;
                dataDS.Rows[i].Cells[9].Value = item.ProductPrice;
                dataDS.Rows[i].Cells[10].Value = item.TotalAmount;
                i++;
            }
        }
        public void loadOutputDG(List<Data.ProductOutput> list)
        {
            int i = 0;
            dataDS.Rows.Clear();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = "NH" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = "KH" + item.MSKH.Value.ToString("D6");
                dataDS.Rows[i].Cells[2].Value = customerBO.GetNameByID(item.MSKH.Value); ;
                dataDS.Rows[i].Cells[3].Value = item.Date.Value.ToShortDateString();
                dataDS.Rows[i].Cells[4].Value = item.MSXE;
                dataDS.Rows[i].Cells[5].Value = item.TruckWeight;
                dataDS.Rows[i].Cells[6].Value = item.ProductName;
                dataDS.Rows[i].Cells[7].Value = item.ProductWeight;
                dataDS.Rows[i].Cells[8].Value = item.TotalWeight;
                dataDS.Rows[i].Cells[9].Value = item.ProductPrice;
                dataDS.Rows[i].Cells[10].Value = item.TotalAmount;
                i++;
            }
        }
        public void loadTotalWeightInput(List<Data.ProductInput> list)
        {
            Int64 result = 0;
            foreach (var item in list)
            {
                result += item.ProductWeight.Value;
            }
            txtTotalWeight.Text = string.Format("{0:#,##0.##}", result)+" (KG)";
        }
        public void loadTotalAmountInput(List<Data.ProductInput> list)
        {
            Int64 result = 0;
            foreach (var item in list)
            {
                result += item.TotalAmount.Value;
            }
            txtTotalAmount.Text = string.Format("{0:#,##0.##}", result)+" (VNĐ)";
        }
        public void loadTotalWeightOutput(List<Data.ProductOutput> list)
        {
            Int64 result = 0;
            foreach (var item in list)
            {
                result += item.ProductWeight.Value;
            }
            txtTotalWeight.Text = string.Format("{0:#,##0.##}", result) + " (KG)";
        }
        public void loadTotalAmountOutput(List<Data.ProductOutput> list)
        {
            Int64 result = 0;
            foreach (var item in list)
            {
                result += item.TotalAmount.Value;
            }
            txtTotalAmount.Text = string.Format("{0:#,##0.##}", result) + " (VNĐ)";
        }
        #endregion
        public ProductManage()
        {
            InitializeComponent();
        }

        private void ProductManage_Load(object sender, EventArgs e)
        {
            loadCustomerType();
            loadCustomerName();
            loadProductType();
            loadDayMonthYear();
            int month = int.Parse(txtMonth.Text);
            int year = int.Parse(txtYear.Text);
            txtDay.Text = "";
            List<Data.ProductInput> listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
            loadTotalAmountInput(listDB);
            loadTotalWeightInput(listDB);
            loadInputDG(listDB);
        }

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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadInputDG(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadInputDG(listDB);
                                loadTotalAmountInput(listDB);
                                loadTotalWeightInput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value == date).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value == date).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value == date, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadOutputDG(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.ProductName == ProductName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadOutputDG(listDB);
                                loadTotalAmountOutput(listDB);
                                loadTotalWeightOutput(listDB);
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

        private void txtDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text == string.Empty)
            {
                txtDay.Text = string.Empty;
            }
            txtProductType_SelectedValueChanged(sender, e);
        }

        private void txtMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text == string.Empty)
            {
                txtDay.Text = string.Empty;
            }
            txtProductType_SelectedValueChanged(sender, e);
        }

        private void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProductType_SelectedValueChanged(sender, e);
        }

        private void txtDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtCustomerType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataDS.Rows.Count - 1)
                {
                    MSHD = int.Parse(dataDS.Rows[e.RowIndex].Cells[0].Value.ToString().Trim().Split('H')[1]);
                }
            }
            catch { }
        }

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtCustomerType.Text == "Nhập hàng")
            {
                frmEditProduct frmEditProduct = new frmEditProduct();
                Input = true;
                frmEditProduct.ShowDialog();
                txtCustomerName_SelectedValueChanged(sender, e);
            }
            else
            {
                frmEditProduct frmEditProduct = new frmEditProduct();
                Input = false;
                frmEditProduct.ShowDialog();
                txtCustomerName_SelectedValueChanged(sender, e);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MSHD != 0)
            {
                if (txtCustomerType.Text == "Nhập hàng")
                {
                    string masseage = "Bạn có muốn xóa hóa đơn DH" + MSHD.ToString("d6") + "không ?";
                    string Title = "Chú ý";
                    DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        productInputBO.isDelete(MSHD);
                        txtCustomerName_SelectedValueChanged(sender, e);
                    }
                }
                else
                {
                    string masseage = "Bạn có muốn xóa hóa đơn DH" + MSHD.ToString("d6") + "không ?";
                    string Title = "Chú ý";
                    DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        productOutputBO.isDelete(MSHD);
                        txtCustomerName_SelectedValueChanged(sender, e);
                    }
                    MSHD = 0;
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
