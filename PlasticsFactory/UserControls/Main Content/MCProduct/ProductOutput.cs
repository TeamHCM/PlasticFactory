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

        private List<Data.ProductOutput> list = new List<Data.ProductOutput>();
        private Data.ProductOutput product = new Data.ProductOutput();

        //Các loại sản phẩm
        private ProductOBO productBO = new ProductOBO();

        private ProductOutputBO productInputBO = new ProductOutputBO();
        public CustomerBO customer = new CustomerBO();
        private TruckBO truckBO = new TruckBO();
        private int flagRow = 0;
        private int currentlyRows = 0;
        private int fladID = 0;
        public static int productID = 0;

        #endregion Generate Field

        #region Support

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
            product.ProductPrice = int.Parse(txtPrice.Text);
            product.TruckWeight = int.Parse(txtTruckofWeight.Text);
            product.ProductWeight = int.Parse(txtProductWeight.Text);
            product.TotalWeight = int.Parse(txtTruckofWeight.Text) + int.Parse(txtProductWeight.Text);
            product.TotalAmount = Int32.Parse(txtProductWeight.Text) * Int32.Parse(txtPrice.Text);
            product.ProductPrice = Int32.Parse(txtPrice.Text);
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
                if (int.Parse(txtProductWeight.Text) <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void loadDG()
        {
            int i = 0;
            int ID = productInputBO.GetID();
            dataDS.Rows.Clear();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                list[i].ID = ID;
                dataDS.Rows[i].Cells[0].Value = "NH" + ID.ToString("D6");
                dataDS.Rows[i].Cells[1].Value = "KH" + item.MSKH.Value.ToString("D6");
                dataDS.Rows[i].Cells[2].Value = customer.GetNameByID(item.MSKH.Value); ;
                dataDS.Rows[i].Cells[3].Value = item.Date.Value.ToShortDateString();
                dataDS.Rows[i].Cells[4].Value = item.MSXE;
                dataDS.Rows[i].Cells[5].Value = item.TruckWeight;
                dataDS.Rows[i].Cells[6].Value = item.ProductName;
                dataDS.Rows[i].Cells[7].Value = item.ProductWeight;
                dataDS.Rows[i].Cells[8].Value = item.TotalWeight;
                dataDS.Rows[i].Cells[9].Value = item.ProductPrice;
                dataDS.Rows[i].Cells[10].Value = item.TotalAmount;
                i++;
                ID++;
            }
        }

        public void RefreshAdd()
        {
            txtMSKH.Items.Clear();
            txtLicencePlate.Items.Clear();
            txtHoten.ResetText();
            txtMSKH.ResetText();
            txtLicencePlate.ResetText();
            txtTruckofWeight.ResetText();
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
                        txtLicencePlate.Text = dataDS.Rows[r].Cells[4].Value.ToString();
                        txtTruckofWeight.Text = dataDS.Rows[r].Cells[5].Value.ToString();
                        txtProductWeight.Text = dataDS.Rows[r].Cells[7].Value.ToString();
                        txtAll.Text = dataDS.Rows[r].Cells[8].Value.ToString();
                        txtPrice.Text = dataDS.Rows[r].Cells[9].Value.ToString();
                        flagRow = 1;
                        btnRemove.Enabled = false;
                        btnSave.Enabled = false;
                        btnThem.Visible = false;
                        btnEdit.Visible = true;
                        break;

                    case 1:
                        flagRow = 0;
                        fladID = 0;
                        btnRemove.Enabled = true;
                        btnSave.Enabled = true;
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
                int i = productInputBO.GetID();
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
            return productInputBO.GetID();
        }

        #endregion Support

        public ProductOutput()
        {
            InitializeComponent();
        }

        private void ProductOutput_Load(object sender, EventArgs e)
        {
            productID = productBO.GetID();
            loadCustomerName();
            loadProduct();
            txtHoten.Focus();
        }

        #region Event Customer

        private void txtHoten_Leave(object sender, EventArgs e)
        {
            txtMSKH.ResetText();
            txtMSKH.Items.Clear();
            try
            {
                var listDB = productInputBO.GetIDByCustomerName(txtHoten.Text);
                foreach (var item in listDB)
                {
                    txtMSKH.Items.Add("KH" + item.ToString("d6"));
                }
                txtMSKH.Text = "KH" + listDB.First().ToString("d6");
            }
            catch { }
        }

        private void txtMSKH_Leave(object sender, EventArgs e)
        {
            if (txtMSKH.Text != "")
            {
                loadLecencePlate();
                loadWeight();
            }
        }

        private void txtMSKH_TextChanged(object sender, EventArgs e)
        {
            loadLecencePlate();
        }

        private void txtHoten_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtHoten.Text != string.Empty)
                {
                    txtMSKH.Focus();
                }
            }
        }

        private void txtMSKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProductName.Focus();
            }
        }

        private void txtMSKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDate.Focus();
            }
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
                        int all = int.Parse(txtAll.Text);
                        int truckWeight = int.Parse(txtTruckofWeight.Text);
                        int result = all - truckWeight;
                        txtProductWeight.Text = result.ToString();
                    }
                    txtAll.Focus();
                }
            }
            catch { }
        }

        private void txtLicencePlate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTruckofWeight.Focus();
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
                }
                catch { }
                btnThem.Focus();
                btnEdit.Focus();
            }
        }

        #endregion AllWeight

        #region DataGridView

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetDGByForm(e.RowIndex);
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
                if (maxMount <= 0)
                {
                    MessageBox.Show("Trọng lượng không hợp lệ hoặc quá tải!");
                }
                else
                {
                    loadInput();
                    list.Add(product);
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
            var obj = list.Find(u => u.ID == fladID);
            if (Validation())
            {
                double maxMount = double.Parse(txtProductWeight.Text) * double.Parse(txtPrice.Text);
                if (maxMount <= 0)
                {
                    MessageBox.Show("Trọng lượng không hợp lệ hoặc quá tải!");
                }
                else
                {
                    obj.MSKH = SplitMSKH(txtMSKH.Text);
                    obj.MSXE = txtLicencePlate.Text;
                    obj.Date = txtDate.Value.Date;
                    obj.ProductName = txtProductName.Text;
                    obj.ProductPrice = int.Parse(txtPrice.Text);
                    obj.TruckWeight = int.Parse(txtTruckofWeight.Text);
                    obj.ProductWeight = int.Parse(txtProductWeight.Text);
                    obj.TotalWeight = int.Parse(txtTruckofWeight.Text) + int.Parse(txtProductWeight.Text);
                    obj.TotalAmount = int.Parse(txtProductWeight.Text) * int.Parse(txtPrice.Text);
                    obj.ProductPrice = int.Parse(txtPrice.Text);
                    loadDG();
                    RefreshAdd();
                    txtHoten.Focus();
                    btnEdit.Visible = false;
                    btnThem.Visible = true;
                    btnSave.Enabled = true;
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
                    list.Remove(product);
                    loadDG();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int count = list.Count();
            if (count > 0)
            {
                bool isCheck = productInputBO.Add(list);
                if (isCheck == false)
                {
                    MessageBox.Show("Lỗi Insert");
                }
                else
                {
                    list.Clear();
                    dataDS.Rows.Clear();
                    RefreshAdd();
                }
            }
        }

        #endregion button
        
    }
}