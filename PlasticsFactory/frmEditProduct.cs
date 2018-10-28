using BUS.Business;
using PlasticsFactory.UserControls.Main_Content.MCProduct;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmEditProduct : Form
    {
        #region Genarete Field

        private Data.ProductInput productInput = new Data.ProductInput();
        private ProductInputBO productInputBO = new ProductInputBO();
        private ProductBO productIpBO = new ProductBO();
        private Data.ProductOutput productOutput = new Data.ProductOutput();
        private ProductOutputBO productOutputBO = new ProductOutputBO();
        private ProductOBO productOBO = new ProductOBO();
        private TruckBO truckBO = new TruckBO();

        #endregion Genarete Field

        #region Support

        private void loadProductTypeInput()
        {
            var listDB = productIpBO.GetData(u => u.isDelete == false);
            txtProductName.Items.Clear();
            foreach (var item in listDB)
            {
                txtProductName.Items.Add(item.Name);
            }
        }

        private void loadProductPriceInput()
        {
            var listDB = productIpBO.GetData(u => u.isDelete == false);
            txtPrice.Items.Clear();
            foreach (var item in listDB)
            {
                txtPrice.Items.Add(item.Price);
            }
        }

        private void loadLicencePlateInput(int MSKH)
        {
            var listDB = truckBO.GetData(u => u.isDelete == false && u.MSKH == MSKH);
            txtLicencePlate.Items.Clear();
            foreach (var item in listDB)
            {
                txtLicencePlate.Items.Add(item.LicencePlate);
            }
        }

        private void loadProductTypeOutput()
        {
            var listDB = productOBO.GetData(u => u.isDelete == false);
            txtProductName.Items.Clear();
            foreach (var item in listDB)
            {
                txtProductName.Items.Add(item.Name);
            }
        }

        private void loadProductPriceOutput()
        {
            var listDB = productOBO.GetData(u => u.isDelete == false);
            txtPrice.Items.Clear();
            foreach (var item in listDB)
            {
                txtPrice.Items.Add(item.Price);
            }
        }

        private void loadLicencePlateOutput(int MSKH)
        {
            var listDB = truckBO.GetData(u => u.isDelete == false && u.MSKH == MSKH);
            txtLicencePlate.Items.Clear();
            foreach (var item in listDB)
            {
                txtLicencePlate.Items.Add(item.LicencePlate);
            }
        }

        public int SplitMSKH(string MSKH)
        {
            return int.Parse(MSKH.Trim().Substring(2));
        }

        public void loadWeight()
        {
            try
            {
                var listDB = truckBO.GetData(u => u.isDelete == false && u.LicencePlate == txtLicencePlate.Text);
            }
            catch { }
        }

        public bool Validation()
        {
            if (txtProductName.Text == string.Empty || txtPrice.Text == string.Empty || txtLicencePlate.Text == string.Empty || txtTruckofWeight.Text == string.Empty || txtAll.Text == string.Empty)
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

        public void loadInput()
        {
            if (ProductManage.Input == true)
            {
                productInput = new Data.ProductInput();
                productInput.ID = ProductManage.MSHD;
                productInput.MSKH = SplitMSKH(txtMSNV.Text);
                productInput.MSXE = txtLicencePlate.Text;
                productInput.Date = DateTime.Parse(txtDate.Text);
                productInput.ProductName = txtProductName.Text;
                productInput.ProductPrice = int.Parse(txtPrice.Text);
                productInput.TruckWeight = int.Parse(txtTruckofWeight.Text);
                productInput.ProductWeight = int.Parse(txtProductWeight.Text);
                productInput.TotalWeight = int.Parse(txtTruckofWeight.Text) + int.Parse(txtProductWeight.Text);
                productInput.TotalAmount = Int32.Parse(txtProductWeight.Text) * Int32.Parse(txtPrice.Text);
                productInput.ProductPrice = Int32.Parse(txtPrice.Text);
            }
            else
            {
                productOutput = new Data.ProductOutput();
                productOutput.ID = ProductManage.MSHD;
                productOutput.MSKH = SplitMSKH(txtMSNV.Text);
                productOutput.MSXE = txtLicencePlate.Text;
                productOutput.Date = DateTime.Parse(txtDate.Text);
                productOutput.ProductName = txtProductName.Text;
                productOutput.ProductPrice = int.Parse(txtPrice.Text);
                productOutput.TruckWeight = int.Parse(txtTruckofWeight.Text);
                productOutput.ProductWeight = int.Parse(txtProductWeight.Text);
                productOutput.TotalWeight = int.Parse(txtTruckofWeight.Text) + int.Parse(txtProductWeight.Text);
                productOutput.TotalAmount = Int32.Parse(txtProductWeight.Text) * Int32.Parse(txtPrice.Text);
                productOutput.ProductPrice = Int32.Parse(txtPrice.Text);
            }
        }

        #endregion Support

        public frmEditProduct()
        {
            InitializeComponent();
        }

        private void frmEditProduct_Load(object sender, EventArgs e)
        {
            txtProductName.Focus();
            if (ProductManage.Input == true)
            {
                int ID = ProductManage.MSHD;
                productInput = productInputBO.GetData(u => u.isDelete == false && u.ID == ID, u => u.Customer).Single();
                loadProductTypeInput();
                loadProductPriceInput();
                loadLicencePlateInput(productInput.MSKH);
                txtName.Text = productInput.Customer.Name;
                txtDate.Text = productInput.Date.Value.ToShortDateString();
                txtMSNV.Text = "KH" + productInput.MSKH.ToString("d6");
                txtMSHD.Text = "HD" + productInput.ID.ToString("d6");
                txtProductName.Text = productInput.ProductName;
                txtProductWeight.Text = productInput.ProductWeight.ToString();
                txtTruckofWeight.Text = productInput.TruckWeight.ToString();
                txtLicencePlate.Text = productInput.MSXE.ToString();
                txtAll.Text = productInput.TotalWeight.ToString();
                txtPrice.Text = productInput.ProductPrice.ToString();
            }
            else
            {
                int ID = ProductManage.MSHD;
                productOutput = productOutputBO.GetData(u => u.isDelete == false && u.ID == ID, u => u.Customer).Single();
                loadProductTypeOutput();
                loadProductPriceOutput();
                loadLicencePlateOutput(productOutput.MSKH.Value);
                txtName.Text = productOutput.Customer.Name;
                txtDate.Text = productOutput.Date.Value.ToShortDateString();
                txtMSNV.Text = "KH" + productOutput.MSKH.Value.ToString("d6");
                txtMSHD.Text = "HD" + productOutput.ID.ToString("d6");
                txtProductName.Text = productOutput.ProductName;
                txtProductWeight.Text = productOutput.ProductWeight.ToString();
                txtTruckofWeight.Text = productOutput.TruckWeight.ToString();
                txtLicencePlate.Text = productOutput.MSXE.ToString();
                txtAll.Text = productOutput.TotalWeight.ToString();
                txtPrice.Text = productOutput.ProductPrice.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                int maxMount = int.Parse(txtProductWeight.Text) * int.Parse(txtPrice.Text);
                if (maxMount <= 0)
                {
                    MessageBox.Show("Trọng lượng không hợp lệ hoặc quá tải!");
                }
                else
                {
                    if (ProductManage.Input == true)
                    {
                        loadInput();
                        bool isCheck = productInputBO.Update(productInput);
                    }
                    else
                    {
                        loadInput();
                        productOutputBO.Update(productOutput);
                    }
                    this.Close();
                }
            }
            else
            {
                txtLicencePlate.Focus();
                MessageBox.Show("Vui lòng điền đúng thông tin");
            }
        }

        #region Event Product

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            if (ProductManage.Input == true)
            {
                if (txtProductName.Text != "")
                {
                    txtPrice.Text = productIpBO.GetPriceByName(txtProductName.Text).ToString();
                }
            }
            else
            {
                if (txtProductName.Text != "")
                {
                    txtPrice.Text = productOBO.GetPriceByName(txtProductName.Text).ToString();
                }
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

        private void txtTruckofWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion Event Truck

        #region AllWeight

        private void txtAll_Leave(object sender, EventArgs e)
        {
            try
            {
                int all = int.Parse(txtAll.Text);
                int truckWeight = int.Parse(txtTruckofWeight.Text);
                int result = all - truckWeight;
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
                    int all = int.Parse(txtAll.Text);
                    int truckWeight = int.Parse(txtTruckofWeight.Text);
                    int result = all - truckWeight;
                    txtProductWeight.Text = result.ToString();
                }
                catch { }
                btnEdit.Focus();
            }
        }

        private void txtAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion AllWeight
    }
}