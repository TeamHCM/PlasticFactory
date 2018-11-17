using BUS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        #endregion Generate Field

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

        #region loadDG input

        public void loadAllInput(List<Data.ProductInput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 4;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            var listCustomer = customerBO.GetData(u => u.isDelete == false && u.Type == 1);
            foreach (var item in listCustomer)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = item.Name;
                dataDS.Rows[i].Cells[2].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight).ToString("#,###") : "0";
                dataDS.Rows[i].Cells[3].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadAllProductInput(List<Data.ProductInput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 5;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Đơn giá";
            dataDS.Columns[4].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            var listCustomer = customerBO.GetData(u => u.isDelete == false && u.Type == 1);
            foreach (var item in listCustomer)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = item.Name;
                dataDS.Rows[i].Cells[2].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight).ToString("#,###") : "0";
                if (listDB.Where(u => u.MSKH == item.ID).Count() != 0)
                {
                    dataDS.Rows[i].Cells[3].Value = listDB.Where(u => u.MSKH == item.ID).First().ProductPrice.Value != 0 ? listDB.Where(u => u.MSKH == item.ID).First().ProductPrice.Value.ToString("#,###") : "0";
                }
                else
                {
                    dataDS.Rows[i].Cells[3].Value = "0";
                }
                dataDS.Rows[i].Cells[4].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadCustomerProductInput(List<Data.ProductInput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 5;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Đơn giá";
            dataDS.Columns[4].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            //theo loai san pham
                dataDS.Rows.Add();
                dataDS.Rows[0].Cells[0].Value = "KH" + listDB.First().Customer.ID.ToString("d6");
                dataDS.Rows[0].Cells[1].Value = customerBO.GetNameByID(listDB.First().Customer.ID);
                dataDS.Rows[0].Cells[2].Value = listDB.Sum(u => u.ProductWeight) != 0 ? listDB.Sum(u => u.ProductWeight).ToString("#,###") : "0";
                if (listDB.Count() != 0)
                {
                    dataDS.Rows[0].Cells[3].Value = listDB.First().ProductPrice.Value != 0 ? listDB.First().ProductPrice.Value.ToString("#,###") : "0";
                }
                else
                {
                    dataDS.Rows[0].Cells[3].Value = "0";
                }
                dataDS.Rows[0].Cells[4].Value = listDB.Sum(u => u.TotalAmount) != 0 ? listDB.Sum(u => u.TotalAmount).ToString("#,###") : "0";
               
        }
        //đơn giá , tên hàng
        public void loadCustomerProductAllInput(List<Data.ProductInput> list)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            var listProduct = list.Select(u => u.ProductName).Distinct();
            #region Create Item DataGridview
            dataDS.ColumnCount = 6;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Tên hàng";
            dataDS.Columns[3].Name = "Trọng tải hàng";
            dataDS.Columns[4].Name = "Đơn giá";
            dataDS.Columns[5].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            foreach (var item in listProduct)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + list.First().Customer.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = customerBO.GetNameByID(list.First().Customer.ID);
                dataDS.Rows[i].Cells[2].Value = item;//tên hàng
                dataDS.Rows[i].Cells[3].Value = list.Where(u => u.ProductName == item).Sum(u => u.ProductWeight) != 0 ? list.Where(u => u.ProductName == item).Sum(u => u.ProductWeight).ToString("#,###") : "0";
                if (list.Count() != 0)
                {
                    dataDS.Rows[i].Cells[4].Value = list.Where(u => u.ProductName == item).First().ProductPrice.Value != 0 ? list.Where(u => u.ProductName == item).First().ProductPrice.Value.ToString("#,###") : "0";
                }
                else
                {
                    dataDS.Rows[i].Cells[4].Value = "0";
                }
                dataDS.Rows[i].Cells[5].Value = list.Where(u => u.ProductName == item).Sum(u => u.TotalAmount) != 0 ? list.Where(u => u.ProductName == item).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadCustomerNameProductMonthAll(List<Data.ProductInput> listDB)
        {
            List<int> listMonth = listDB.Select(u => u.Date.Value.Month).Distinct().ToList();
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 5;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Tháng";
            dataDS.Columns[3].Name = "Trọng tải hàng";
            dataDS.Columns[4].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            foreach (var item in listMonth)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + listDB.First().Customer.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = customerBO.GetNameByID(listDB.First().Customer.ID);
                dataDS.Rows[i].Cells[2].Value = item;
                dataDS.Rows[i].Cells[3].Value = listDB.Sum(u => u.ProductWeight) != 0 ? listDB.Sum(u => u.ProductWeight).ToString("#,###") : "0";
                dataDS.Rows[i].Cells[4].Value = listDB.Where(u=>u.Date.Value.Month == item&&u.Date.Value.Year==listDB.First().Date.Value.Year).Sum(u => u.TotalAmount ) != 0 ? listDB.Where(u => u.Date.Value.Month == item && u.Date.Value.Year == listDB.First().Date.Value.Year).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadCustomerInput(List<Data.ProductInput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            int i = 0;
            #region Create Item DataGridview
            dataDS.ColumnCount = 4;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            foreach (var item in listDB)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = customerBO.GetNameByID(item.ID);
                dataDS.Rows[i].Cells[2].Value = listDB.Sum(u => u.ProductWeight) != 0 ? listDB.Sum(u => u.ProductWeight).ToString("#,###") : "0";
                dataDS.Rows[i].Cells[3].Value = listDB.Sum(u => u.TotalAmount) != 0 ? listDB.Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        #endregion

        #region loadDG Output

        public void loadAllOutput(List<Data.ProductOutput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 4;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            var listCustomer = customerBO.GetData(u => u.isDelete == false && u.Type == 1);
            foreach (var item in listCustomer)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = item.Name;
                dataDS.Rows[i].Cells[2].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight).ToString("#,###") : "0";
                dataDS.Rows[i].Cells[3].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadAllProductOutput(List<Data.ProductOutput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 5;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Đơn giá";
            dataDS.Columns[4].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            var listCustomer = customerBO.GetData(u => u.isDelete == false && u.Type == 1);
            foreach (var item in listCustomer)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = item.Name;
                dataDS.Rows[i].Cells[2].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.ProductWeight).ToString("#,###") : "0";
                if (listDB.Where(u => u.MSKH == item.ID).Count() != 0)
                {
                    dataDS.Rows[i].Cells[3].Value = listDB.Where(u => u.MSKH == item.ID).First().ProductPrice != 0 ? listDB.Where(u => u.MSKH == item.ID).First().ProductPrice.ToString("#,###") : "0";
                }
                else
                {
                    dataDS.Rows[i].Cells[3].Value = "0";
                }
                dataDS.Rows[i].Cells[4].Value = listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount) != 0 ? listDB.Where(u => u.MSKH == item.ID).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadCustomerProductOutput(List<Data.ProductOutput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 5;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Đơn giá";
            dataDS.Columns[4].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            //theo loai san pham
            dataDS.Rows.Add();
            dataDS.Rows[0].Cells[0].Value = "KH" + listDB.First().Customer.ID.ToString("d6");
            dataDS.Rows[0].Cells[1].Value = customerBO.GetNameByID(listDB.First().Customer.ID);
            dataDS.Rows[0].Cells[2].Value = listDB.Sum(u => u.ProductWeight) != 0 ? listDB.Sum(u => u.ProductWeight).ToString("#,###") : "0";
            if (listDB.Count() != 0)
            {
                dataDS.Rows[0].Cells[3].Value = listDB.First().ProductPrice != 0 ? listDB.First().ProductPrice.ToString("#,###") : "0";
            }
            else
            {
                dataDS.Rows[0].Cells[3].Value = "0";
            }
            dataDS.Rows[0].Cells[4].Value = listDB.Sum(u => u.TotalAmount) != 0 ? listDB.Sum(u => u.TotalAmount).ToString("#,###") : "0";

        }
        //đơn giá , tên hàng
        public void loadCustomerProductAllOutput(List<Data.ProductOutput> list)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            var listProduct = list.Select(u => u.ProductName).Distinct();
            #region Create Item DataGridview
            dataDS.ColumnCount = 6;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Tên hàng";
            dataDS.Columns[3].Name = "Trọng tải hàng";
            dataDS.Columns[4].Name = "Đơn giá";
            dataDS.Columns[5].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            foreach (var item in listProduct)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + list.First().Customer.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = customerBO.GetNameByID(list.First().Customer.ID);
                dataDS.Rows[i].Cells[2].Value = item;//tên hàng
                dataDS.Rows[i].Cells[3].Value = list.Where(u => u.ProductName == item).Sum(u => u.ProductWeight) != 0 ? list.Where(u => u.ProductName == item).Sum(u => u.ProductWeight).ToString("#,###") : "0";
                if (list.Count() != 0)
                {
                    dataDS.Rows[i].Cells[4].Value = list.Where(u => u.ProductName == item).First().ProductPrice != 0 ? list.Where(u => u.ProductName == item).First().ProductPrice.ToString("#,###") : "0";
                }
                else
                {
                    dataDS.Rows[i].Cells[4].Value = "0";
                }
                dataDS.Rows[i].Cells[5].Value = list.Where(u => u.ProductName == item).Sum(u => u.TotalAmount) != 0 ? list.Where(u => u.ProductName == item).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadCustomerNameProductMonthAllOutput(List<Data.ProductOutput> listDB)
        {
            List<int> listMonth = listDB.Select(u => u.Date.Value.Month).Distinct().ToList();
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            #region Create Item DataGridview
            dataDS.ColumnCount = 5;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Tháng";
            dataDS.Columns[3].Name = "Trọng tải hàng";
            dataDS.Columns[4].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            int i = 0;
            foreach (var item in listMonth)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + listDB.First().Customer.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = customerBO.GetNameByID(listDB.First().Customer.ID);
                dataDS.Rows[i].Cells[2].Value = item;
                dataDS.Rows[i].Cells[3].Value = listDB.Sum(u => u.ProductWeight) != 0 ? listDB.Sum(u => u.ProductWeight).ToString("#,###") : "0";
                dataDS.Rows[i].Cells[4].Value = listDB.Where(u => u.Date.Value.Month == item && u.Date.Value.Year == listDB.First().Date.Value.Year).Sum(u => u.TotalAmount) != 0 ? listDB.Where(u => u.Date.Value.Month == item && u.Date.Value.Year == listDB.First().Date.Value.Year).Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        public void loadCustomerOutput(List<Data.ProductOutput> listDB)
        {
            dataDS.Columns.Clear();
            dataDS.Rows.Clear();
            int i = 0;
            #region Create Item DataGridview
            dataDS.ColumnCount = 4;
            dataDS.Columns[0].Name = "MSKH";
            dataDS.Columns[1].Name = "Họ và tên";
            dataDS.Columns[2].Name = "Trọng tải hàng";
            dataDS.Columns[3].Name = "Thành tiền";

            dataDS.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataDS.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
            foreach (var item in listDB)
            {
                dataDS.Rows.Add();
                if (i % 2 == 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {

                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                dataDS.Rows[i].Cells[0].Value = "KH" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = customerBO.GetNameByID(item.ID);
                dataDS.Rows[i].Cells[2].Value = listDB.Sum(u => u.ProductWeight) != 0 ? listDB.Sum(u => u.ProductWeight).ToString("#,###") : "0";
                dataDS.Rows[i].Cells[3].Value = listDB.Sum(u => u.TotalAmount) != 0 ? listDB.Sum(u => u.TotalAmount).ToString("#,###") : "0";
                i++;
            }
        }

        #endregion
        #endregion Support

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
                                    loadAllInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadAllInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadAllInput(listDB);
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
                                    loadAllInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadAllInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadAllInput(listDB);
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
                                    loadAllProductInput(listDB);
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
                                    loadAllProductInput(listDB);
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
                                loadAllProductInput(listDB);
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
                                    //loadAllInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadCustomerProductInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                loadAllInput(listDB);
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
                                    loadAllProductInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadAllProductInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadAllProductInput(listDB);
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
                                    loadCustomerNameProductMonthAll(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadCustomerProductAllInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadCustomerNameProductMonthAll(listDB);
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
                                    //loadCustomerProductInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name.Trim() == CustomerName.Trim() && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadCustomerProductInput(listDB);
                                    loadTotalAmountInput(listDB);
                                    loadTotalWeightInput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = productInputBO.GetData(u => u.isDelete == false && u.Customer.Name == txtCustomerName.Text && u.ProductName == ProductName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadCustomerNameProductMonthAll(listDB);
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
                                    //loadAllOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadCustomerProductOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == year).ToList();
                                loadAllOutput(listDB);
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
                                    loadAllProductOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
                                    loadAllProductOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.ProductName == ProductName && u.Date.Value.Year == year).ToList();
                                loadAllProductOutput(listDB);
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
                                    loadCustomerNameProductMonthAllOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadCustomerProductAllOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                string CustomerName = txtCustomerName.Text.Trim();
                                int year = int.Parse(txtYear.Text);
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == CustomerName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadCustomerNameProductMonthAllOutput(listDB);
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
                                    //loadCustomerProductOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                                else
                                {
                                    int month = int.Parse(txtMonth.Text);
                                    int year = int.Parse(txtYear.Text);
                                    string ProductName = txtProductType.Text;
                                    string CustomerName = txtCustomerName.Text.Trim();
                                    var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name.Trim() == CustomerName.Trim() && u.ProductName == ProductName && u.Date.Value.Month == month && u.Date.Value.Year == year, u => u.Customer).ToList();
                                    loadCustomerProductOutput(listDB);
                                    loadTotalAmountOutput(listDB);
                                    loadTotalWeightOutput(listDB);
                                }
                            }
                            else
                            {
                                int year = int.Parse(txtYear.Text);
                                string ProductName = txtProductType.Text;
                                string CustomerName = txtCustomerName.Text.Trim();
                                var listDB = productOutputBO.GetData(u => u.isDelete == false && u.Customer.Name == txtCustomerName.Text && u.ProductName == ProductName && u.Date.Value.Year == year, u => u.Customer).ToList();
                                loadCustomerNameProductMonthAllOutput(listDB);
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
    }
}