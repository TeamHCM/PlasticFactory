using BUS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCProduct
{
    public partial class ProductOutput : UserControl
    {
        #region Generate Field
        private TypeWeightBO typeWeightBO = new TypeWeightBO();
        private List<Data.ProductOutput> list = new List<Data.ProductOutput>();
        private Data.ProductOutput product = new Data.ProductOutput();
        private ProductOBO productBO = new ProductOBO();
        private ProductOutputBO productOutputBO = new ProductOutputBO();
        public CustomerBO customer = new CustomerBO();
        private TruckBO truckBO = new TruckBO();
        private int flagRow = 0;
        private int currentlyRows = 0;
        private int fladID = 0;
        public static int productID = 0;

        #endregion Generate Field

        #region Support
        public void loadTypeWeight()
        {
            try
            {
                var listDB = typeWeightBO.GetData(u => u.KG != 0).OrderByDescending(u => u.KG);
                txtTypeWeight.Items.Clear();
                foreach (var item in listDB)
                {
                    txtTypeWeight.Items.Add(item.KG);
                }
                txtTypeWeight.Text = listDB.First().KG.ToString();
            }
            catch
            {
                MessageBox.Show("Vui lòng thêm loại bao vào hệ thống trông 'Tùy chọn'");
            }
        }

        public int SplitMSKH(string MSKH)
        {
            return int.Parse(MSKH.Trim().Substring(2));
        }

        public void loadCustomerName()
        {
            var listDB = customer.GetData(u => u.isDelete == false && u.TypeofCustomer.Type == "Xuất hàng", u => u.TypeofCustomer);
            txtHoten.Items.Clear();
            foreach (var item in listDB)
            {
                txtHoten.Items.Add(item.Name);
            }
        }

        public void loadLecencePlate()
        {
            try
            {
                int MSKH = SplitMSKH(txtMSKH.Text);
                var listDB = truckBO.GetLicencePlateByIDofCustomer(MSKH);
                txtLicencePlate.Items.Clear();
                foreach (var item in listDB)
                {
                    txtLicencePlate.Items.Add(item);
                }
                txtLicencePlate.Text = listDB.First();
            }
            catch { }
        }

        public void loadProduct()
        {
            try
            {
                var listDB = productBO.GetData(u => u.isDelete == false);
                txtProductName.Items.Clear();
                foreach (var item in listDB)
                {
                    txtProductName.Items.Add(item.Name);
                }
                txtProductName.Text = listDB.First().Name;
                txtPrice.Text = productBO.GetPriceByName(txtProductName.Text).ToString();
            }
            catch { }
        }

        public void loadInput()
        {
            product = new Data.ProductOutput();
            product.ID = GetIDMSHD();
            product.MSKH = SplitMSKH(txtMSKH.Text);
            product.MSXE = txtLicencePlate.Text;
            product.Date = txtDate.Value.Date;
            product.ProductName = txtProductName.Text;
            product.ProductPrice = double.Parse(txtPrice.Text);
            product.TruckWeight = double.Parse(txtTruckofWeight.Text);
            product.ProductWeight = double.Parse(txtProductWeight.Text);
            product.TypeSack = int.Parse(txtTypeWeight.Text);
            product.sack = int.Parse(txtWeight.Text);
            product.TotalWeight = double.Parse(txtTruckofWeight.Text) + double.Parse(txtProductWeight.Text);
            product.TotalAmount = double.Parse(txtProductWeight.Text) * double.Parse(txtPrice.Text);
            product.ProductPrice = double.Parse(txtPrice.Text);
        }

        public void loadWeight()
        {
            try
            {
                var listDB = truckBO.GetData(u => u.isDelete == false && u.LicencePlate == txtLicencePlate.Text);
                txtTruckofWeight.Text = listDB.First().Weight.Value.ToString();
            }
            catch { }
        }

        public bool Validation()
        {
            if (txtMSKH.Text == string.Empty || txtProductName.Text == string.Empty || txtPrice.Text == string.Empty || txtLicencePlate.Text == string.Empty || txtTruckofWeight.Text == string.Empty || txtAll.Text == string.Empty)
            {
                return false;
            }
            else
            {
                if (int.Parse(txtProductWeight.Text) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void loadDG()
        {
            int i = 0;
            dataDS.Rows.Clear();
            int month = txtDate.Value.Month;
            int year = txtDate.Value.Year;
            list = productOutputBO.GetData(u => u.isDelete == false && u.MSKH == int.Parse(txtMSKH.Text.Substring(2)) && u.Date.Value.Month == month && u.Date.Value.Year == year).ToList();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                if (item.ProductWeight <= 0)
                {
                    dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[6].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[7].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[9].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[10].Style.BackColor = System.Drawing.Color.Salmon;
                    dataDS.Rows[i].Cells[11].Style.BackColor = System.Drawing.Color.Salmon;
                }
                dataDS.Rows[i].Cells[0].Value = "XH" + item.ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = "KH" + item.MSKH.Value.ToString("D6");
                dataDS.Rows[i].Cells[2].Value = customer.GetNameByID(item.MSKH.Value); ;
                dataDS.Rows[i].Cells[3].Value = item.Date.Value.ToShortDateString();
                dataDS.Rows[i].Cells[4].Value = item.MSXE;
                dataDS.Rows[i].Cells[5].Value = item.TruckWeight;
                dataDS.Rows[i].Cells[6].Value = item.ProductName;
                dataDS.Rows[i].Cells[7].Value = item.ProductWeight;
                dataDS.Rows[i].Cells[8].Value = item.TypeSack;
                dataDS.Rows[i].Cells[9].Value = item.sack;
                dataDS.Rows[i].Cells[10].Value = item.TotalWeight;
                dataDS.Rows[i].Cells[11].Value = item.ProductPrice;
                dataDS.Rows[i].Cells[12].Value = item.TotalAmount;
                i++;
            }
            if (i > 0)
            {
                dataDS.CurrentCell = dataDS.Rows[i - 1].Cells[0];
                dataDS.CurrentRow.Selected = true;
            }
            txtTotalWeight.Text = list.Sum(u => u.ProductWeight) != 0 ? list.Sum(u => u.ProductWeight).ToString("#,###") : "0";
        }

        public void RefreshAdd()
        {
            txtLicencePlate.Items.Clear();
            txtAll.ResetText();
            txtProductWeight.ResetText();
        }

        public void GetDGByForm(int r)
        {
            if (r < dataDS.Rows.Count - 1)
            {
                switch (flagRow)
                {
                    case 0:
                        fladID = int.Parse(dataDS.Rows[r].Cells[0].Value.ToString().Split('H')[1]);
                        txtMSKH.Text = dataDS.Rows[r].Cells[1].Value.ToString();
                        txtHoten.Text = dataDS.Rows[r].Cells[2].Value.ToString();
                        txtDate.Text = DateTime.Parse(dataDS.Rows[r].Cells[3].Value.ToString()).ToString();
                        txtProductName.Text = dataDS.Rows[r].Cells[6].Value.ToString();
                        txtWeight.Text = dataDS.Rows[r].Cells[9].Value.ToString();
                        txtTypeWeight.Text = dataDS.Rows[r].Cells[8].Value.ToString();
                        txtLicencePlate.Text = dataDS.Rows[r].Cells[4].Value.ToString();
                        txtTruckofWeight.Text = dataDS.Rows[r].Cells[5].Value.ToString();
                        txtProductWeight.Text = dataDS.Rows[r].Cells[7].Value.ToString();
                        txtAll.Text = dataDS.Rows[r].Cells[10].Value.ToString();
                        txtPrice.Text = dataDS.Rows[r].Cells[11].Value.ToString();
                        flagRow = 1;
                        btnRemove.Enabled = false;
                        btnThem.Visible = false;
                        btnEdit.Visible = true;
                        break;

                    case 1:
                        flagRow = 0;
                        fladID = 0;
                        btnRemove.Enabled = true;
                        btnThem.Visible = true;
                        btnEdit.Visible = false;
                        RefreshAdd();
                        break;
                }
            }
        }

        public int GetIDMSHD()
        {
            if (list.Count != 0)
            {
                int i = productOutputBO.GetID();
                foreach (var item in list)
                {
                    if (i != item.ID)
                    {
                        return i;
                    }
                    i++;
                }
                return i;
            }
            return productOutputBO.GetID();
        }

        #endregion Support

        public ProductOutput()
        {
            InitializeComponent();
        }

        private void ProductOutput_Load(object sender, EventArgs e)
        {
            productID = productBO.GetID();
            loadTypeWeight();
            loadCustomerName();
            loadProduct();
            txtHoten.Focus();
        }

        #region Event Customer

        private void txtMSKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtMSKH.Text != string.Empty)
            {
                loadLecencePlate();
                loadWeight();
                loadDG();
            }
        }

        private void txtMSKH_Leave(object sender, EventArgs e)
        {
            if (txtMSKH.Text != string.Empty)
            {
                txtMSKH_SelectedIndexChanged(sender, e);
            }
        }

        private void txtMSKH_TextChanged(object sender, EventArgs e)
        {
            loadLecencePlate();
        }

        private void txtMSKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtMSKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDate.Focus();
            }
        }

        private void txtHoten_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMSKH.ResetText();
            txtMSKH.Items.Clear();
            try
            {
                var listDB = productOutputBO.GetIDByCustomerName(txtHoten.Text);
                foreach (var item in listDB)
                {
                    txtMSKH.Items.Add("KH" + item.ToString("d6"));
                }
                txtMSKH.Text = "KH" + listDB.First().ToString("d6");

            }
            catch { }
        }

        private void txtHoten_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtHoten.Text != string.Empty)
                {
                    txtDate.Focus();
                }
            }
        }

        private void txtDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProductName.Focus();
            }
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            loadDG();
        }

        #endregion Event Customer

        #region Event Product

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            if (txtProductName.Text != "")
            {
                txtPrice.Text = productBO.GetPriceByName(txtProductName.Text).ToString();
            }
        }

        private void txtProductName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPrice.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                txtDate.Focus();
            }
            if (e.KeyCode == Keys.Right)
            {
                txtLicencePlate.Focus();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLicencePlate.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                txtProductName.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                txtDate.Focus();
            }
            if (e.KeyCode == Keys.Right)
            {
                txtTruckofWeight.Focus();
            }
        }

        #endregion Event Product

        #region Event Truck

        private void txtLicencePlate_Leave(object sender, EventArgs e)
        {
            loadWeight();
        }

        private void txtTruckofWeight_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtAll.Text != string.Empty)
                    {
                        double all = double.Parse(txtAll.Text);
                        double truckWeight = double.Parse(txtTruckofWeight.Text);
                        double result = all - truckWeight;
                        txtProductWeight.Text = result.ToString();
                    }
                    //btnSaveTruck.Focus();
                    txtAll.Focus();
                }
                if (e.KeyCode == Keys.Left)
                {
                    txtPrice.Focus();
                }
                if (e.KeyCode == Keys.Right)
                {
                    txtTruckofWeight.Focus();
                }
                if (e.KeyCode == Keys.Up)
                {
                    txtLicencePlate.Focus();
                }
            }
            catch { }
        }

        private void txtTruckofWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void txtTruckofWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                List<string> arrList = new List<string>();
                int phannguyen = 0;
                int money = 0;
                bool checkNum = int.TryParse(txtTruckofWeight.Text, out money);
                if (checkNum == false)
                {
                    string[] str = txtTruckofWeight.Text.Split('.');
                    string resStr = "";
                    foreach (var item in str)
                    {
                        resStr += item;
                    }
                    money = int.Parse(resStr);
                }
                do
                {
                    phannguyen = money % 1000;
                    money = money / 1000;
                    if (money != 0)
                    {
                        arrList.Add("." + phannguyen.ToString("d3"));
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
                txtTruckofWeight.Text = result;
                txtTruckofWeight.SelectionStart = result.Length;
            }
            catch
            { }
        }

        private void txtLicencePlate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtMSKH.Text != string.Empty && txtLicencePlate.Text != string.Empty)
                {
                    var listTruck = truckBO.GetData(u => u.isDelete == false && u.MSKH == int.Parse(txtMSKH.Text.Substring(2)) && u.LicencePlate.Trim() == txtLicencePlate.Text.Trim());
                    if (listTruck.Count == 0)
                    {
                        btnSaveTruck.Visible = true;
                    }
                }
                txtTruckofWeight.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                txtProductName.Focus();
            }
            if (e.KeyCode == Keys.Right)
            {
                txtAll.Focus();
            }
        }
        #endregion Event Truck

        #region AllWeight

        private void txtAll_Leave(object sender, EventArgs e)
        {
            try
            {
                double all = double.Parse(txtAll.Text);
                double truckWeight = double.Parse(txtTruckofWeight.Text);
                double result = all - truckWeight;
                txtProductWeight.Text = result.ToString();
            }
            catch { }
        }

        private void txtAll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double all = double.Parse(txtAll.Text);
                    double truckWeight = double.Parse(txtTruckofWeight.Text);
                    double result = all - truckWeight;
                    txtProductWeight.Text = result.ToString();
                    txtWeight.Text = Math.Round((result / int.Parse(txtTypeWeight.Text))).ToString();
                }

                catch { }
                btnThem.Focus();
                btnEdit.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                txtLicencePlate.Focus();
            }
        }

        private void txtAll_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                List<string> arrList = new List<string>();
                int phannguyen = 0;
                int money = 0;
                bool checkNum = int.TryParse(txtAll.Text, out money);
                if (checkNum == false)
                {
                    string[] str = txtAll.Text.Split('.');
                    string resStr = "";
                    foreach (var item in str)
                    {
                        resStr += item;
                    }
                    money = int.Parse(resStr);
                }
                do
                {
                    phannguyen = money % 1000;
                    money = money / 1000;
                    if (money != 0)
                    {
                        arrList.Add("." + phannguyen.ToString("d3"));
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
                txtAll.Text = result;
                txtAll.SelectionStart = result.Length;
            }
            catch
            { }
        }

        private void txtAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        #endregion AllWeight

        #region DataGridView

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GetDGByForm(e.RowIndex);
                dataDS.CurrentCell = dataDS.Rows[e.RowIndex].Cells[0];
                dataDS.CurrentRow.Selected = true;
            }
            catch { }
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataDS.Rows.Count - 1)
            {
                currentlyRows = e.RowIndex;
            }
            else
            {
                currentlyRows = dataDS.Rows.Count;
            }
        }


        #endregion DataGridView

        #region button

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                double maxMount = double.Parse(txtProductWeight.Text) * double.Parse(txtPrice.Text);
                if (maxMount < 0)
                {
                    MessageBox.Show("Trọng lượng không hợp lệ hoặc quá tải!");
                }
                else
                {
                    loadInput();
                    productOutputBO.Add(product);
                    loadDG();
                    RefreshAdd();
                    txtHoten.Focus();
                }
            }
            else
            {
                txtLicencePlate.Focus();
                MessageBox.Show("Vui lòng điền đúng thông tin");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            product = new Data.ProductOutput();
            if (Validation())
            {
                int maxMount = int.Parse(txtProductWeight.Text) * int.Parse(txtPrice.Text);
                if (maxMount < 0)
                {
                    MessageBox.Show("Trọng lượng không hợp lệ hoặc quá tải!");
                }
                else
                {
                    product.ID = fladID;
                    product.MSKH = SplitMSKH(txtMSKH.Text);
                    product.MSXE = txtLicencePlate.Text;
                    product.Date = txtDate.Value.Date;
                    product.ProductName = txtProductName.Text;
                    product.sack = int.Parse(txtWeight.Text);
                    product.TypeSack = int.Parse(txtTypeWeight.Text);
                    product.ProductPrice = double.Parse(txtPrice.Text);
                    product.TruckWeight = double.Parse(txtTruckofWeight.Text);
                    product.ProductWeight = double.Parse(txtProductWeight.Text);
                    product.TotalWeight = double.Parse(txtTruckofWeight.Text) + double.Parse(txtProductWeight.Text);
                    product.TotalAmount = double.Parse(txtProductWeight.Text) * double.Parse(txtPrice.Text);
                    product.ProductPrice = double.Parse(txtPrice.Text);
                    productOutputBO.Update(product);
                    loadDG();
                    RefreshAdd();
                    txtHoten.Focus();
                    btnEdit.Visible = false;
                    btnThem.Visible = true;
                    btnRemove.Enabled = true;
                }
            }
            else
            {
                txtLicencePlate.Focus();
                MessageBox.Show("Vui lòng điền đúng thông tin");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (currentlyRows < dataDS.Rows.Count - 1)
            {
                product = new Data.ProductOutput();
                int ID = int.Parse(dataDS.Rows[currentlyRows].Cells[0].Value.ToString().Split('H')[1]);
                product = list.Find(u => u.ID == ID);
                string masseage = "Bạn có muốn xóa hóa đơn " + "HK" + product.ID.ToString("D6").Trim() + " không?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    productOutputBO.isDelete(ID);
                    loadDG();
                }
            }
        }

        private void btnSaveTruck_Click(object sender, EventArgs e)
        {
            if (txtLicencePlate.Text != string.Empty && double.Parse(txtTruckofWeight.Text) > 0)
            {
                bool check = truckBO.Add(new Data.Truck { MSKH = int.Parse(txtMSKH.Text.Substring(2)), LicencePlate = txtLicencePlate.Text, Weight = double.Parse(txtTruckofWeight.Text) });
                if (check == true)
                {
                    MessageBox.Show("Thêm thành công ");
                    btnSaveTruck.Visible = false;
                    txtAll.Focus();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
            else
            {
                MessageBox.Show("Không hợp lệ!");
            }
        }

        #endregion button

        private void txtTypeWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double all = double.Parse(txtAll.Text);
                double truckWeight = double.Parse(txtTruckofWeight.Text);
                double result = all - truckWeight;
                txtProductWeight.Text = result.ToString();
                txtWeight.Text = Math.Round((result / int.Parse(txtTypeWeight.Text))).ToString();
            }
            catch { }
        }

        private void txtTypeWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtLicencePlate.Focus();
            }
        }
    }
}