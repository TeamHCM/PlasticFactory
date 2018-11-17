using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS.Business;

namespace PlasticsFactory.UserControls.Main_Content.MCStatistic
{
    public partial class MCConfiguration : UserControl
    {
        #region Generate Field
        PreferceProductPriceBO preferenceProductPriceBO = new PreferceProductPriceBO();
        ProductBO productIBO = new ProductBO();
        ProductOBO productOBO = new ProductOBO();
        TypeWeightBO typeWeightBO = new TypeWeightBO();
        Data.ProductIP productIP = new Data.ProductIP();
        Data.ProductOP productOP = new Data.ProductOP();
        #endregion

        #region Support
        private void loadInformation()
        {
            //Form ProductPrice
            var listPreference = preferenceProductPriceBO.GetData(u => u.isDelete == false);
            txtProductPrice.Text = listPreference.First(u => u.Name.Trim() == "Sản phẩm").Price.ToString();
            txtTimePrice.Text = listPreference.First(u => u.Name.Trim() == "Thời gian").Price.ToString();
            //Form ProductInput
            var listProductInput = productIBO.GetData(u => u.isDelete == false);
            txtAllProductInputName.ResetText();
            txtAllInputPrice.ResetText();
            txtAllProductInputName.Items.Clear();
            foreach (var item in listProductInput)
            {
                txtAllProductInputName.Items.Add(item.Name);
            }
            //Form ProductOutput
            txtAllProductOutputName.ResetText();
            txtAllOutputPrice.ResetText();
            var listProductOutput = productOBO.GetData(u => u.isDelete == false);
            txtAllProductOutputName.Items.Clear();
            foreach (var item in listProductOutput)
            {
                txtAllProductOutputName.Items.Add(item.Name);
            }
            //Form TypeWeight
            var listType = typeWeightBO.GetData(u => u.Type != -1);
            txtTypeWeight.ResetText();
            txtTypeWeight.Items.Clear();
            foreach (var item in listType)
            {
                txtTypeWeight.Items.Add(item.KG);
            }
        }
        #endregion

        public MCConfiguration()
        {
            InitializeComponent();
        }

        private void MCConfiguration_Load(object sender, EventArgs e)
        {
            loadInformation();
        }

        private void btnEditMoneyProduct_Click(object sender, EventArgs e)
        {
            Data.PreferceProductPrice preferceProductPrice = new Data.PreferceProductPrice();
            preferceProductPrice.ID = 1;
            preferceProductPrice.Name = "Sản phẩm";
            preferceProductPrice.Price = int.Parse(txtProductPrice.Text);
            bool checkProduct = preferenceProductPriceBO.Update(preferceProductPrice);
            preferceProductPrice = new Data.PreferceProductPrice();
            preferceProductPrice.Name = "Thời gian";
            preferceProductPrice.ID = 2;
            preferceProductPrice.Price = int.Parse(txtTimePrice.Text);
            bool checkTime = preferenceProductPriceBO.Update(preferceProductPrice);
            if (checkTime == true && checkProduct == true)
            {
                MessageBox.Show("Hệ thống cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Hệ thống cập nhật thất bại");
            }
            loadInformation();
        }

        private void txtAllProductInputName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtAllProductInputName.Text != string.Empty)
            {
                txtAllInputPrice.Text = (productIBO.GetData(u => u.isDelete == false && u.Name.Trim() == txtAllProductInputName.Text).First().Price).ToString();
            }
        }

        private void txtAllProductOutputName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtAllProductOutputName.Text != string.Empty)
            {
                txtAllOutputPrice.Text = (productOBO.GetData(u => u.isDelete == false && u.Name.Trim() == txtAllProductOutputName.Text).First().Price).ToString();
            }
        }

        private void btnEditInput_Click(object sender, EventArgs e)
        {
            if (txtAllProductInputName.Text != string.Empty && txtAllInputPrice.Text != string.Empty && txtAllInputPrice.Text != "0")
            {
                productIP = new Data.ProductIP();
                productIP.Name = txtAllProductInputName.Text;
                productIP.Price = int.Parse(txtAllInputPrice.Text);
                productIP.ID = productIBO.GetIDByName(txtAllProductInputName.Text);
                productIBO.Update(productIP);
                MessageBox.Show("Hệ thống cập nhật thành công");
                loadInformation();
            }
            else
            {
                MessageBox.Show("Hệ thống cập nhật thất bại");
            }
        }

        private void btnEditOutput_Click(object sender, EventArgs e)
        {
            if (txtAllProductOutputName.Text != string.Empty && txtAllOutputPrice.Text != string.Empty && txtAllOutputPrice.Text != "0")
            {
                productOP = new Data.ProductOP();
                productOP.Name = txtAllProductOutputName.Text;
                productOP.Price = int.Parse(txtAllOutputPrice.Text);
                productOP.ID = productOBO.GetIDByName(txtAllProductOutputName.Text);
                productOBO.Update(productOP);
                MessageBox.Show("Hệ thống cập nhật thành công");
                loadInformation();
            }
            else
            {
                MessageBox.Show("Hệ thống cập nhật thất bại");
            }
        }

        private void btnRemoveInput_Click(object sender, EventArgs e)
        {
            if (txtAllProductInputName.Text != string.Empty)
            {
                string masseage = "Bạn có muốn xóa không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    int ID = productIBO.GetIDByName(txtAllProductInputName.Text);
                    productIBO.Delete(ID);
                    loadInformation();
                }
            }
        }

        private void btnRemoveOutput_Click(object sender, EventArgs e)
        {
            if (txtAllProductOutputName.Text != string.Empty)
            {
                string masseage = "Bạn có muốn xóa không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    int ID = productOBO.GetIDByName(txtAllProductOutputName.Text);
                    productOBO.Delete(ID);
                    loadInformation();
                }
            }


        }

        private void btnAddInput_Click(object sender, EventArgs e)
        {
            if (txtInputName.Text != string.Empty && txtInputPrice.Text != string.Empty && int.Parse(txtInputPrice.Text) > 0)
            {
                bool isExist = productIBO.isExistName(txtInputName.Text);
                if (!isExist)
                {
                    productIP = new Data.ProductIP();
                    productIP.Name = txtInputName.Text;
                    productIP.Price = int.Parse(txtInputPrice.Text);
                    bool check=productIBO.Add(productIP);
                    if (check)
                    {
                        MessageBox.Show("Thêm thành công ");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại");
                    }
                    loadInformation();
                    txtInputName.Text = string.Empty;
                    txtInputPrice.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Hàng này đã tồn tại");
                }
            }
            else
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void btnAddOutput_Click(object sender, EventArgs e)
        {
            if (txtOutputName.Text != string.Empty && txtOutputPrice.Text != string.Empty && int.Parse(txtOutputPrice.Text) > 0)
            {
                bool isExist = productOBO.isExistName(txtOutputName.Text);
                if (!isExist)
                {
                    productOP = new Data.ProductOP();
                    productOP.Name = txtOutputName.Text;
                    productOP.Price = int.Parse(txtOutputPrice.Text);
                    bool check=productOBO.Add(productOP);
                    if(check)
                    {
                        MessageBox.Show("Thêm thành công ");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại");
                    }
                    loadInformation();
                    txtOutputName.Text = string.Empty;
                    txtOutputPrice.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Hàng này đã tồn tại");
                }
            }
            else
            {
                MessageBox.Show("Không hợp lệ");
            }
        }

        private void txtProductPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTimePrice.Focus();
        }

        private void txtInputName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtInputPrice.Focus();
        }

        private void txtTimePrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnEditMoneyProduct.Focus();
        }

        private void txtInputPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddInput.Focus();
        }

        private void txtOutputName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOutputPrice.Focus();
        }

        private void txtOutputPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddOutput.Focus();
        }

        private void txtAllProductInputName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAllInputPrice.Focus();
        }

        private void txtAllInputPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnEditInput.Focus();
        }

        private void txtAllProductOutputName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtOutputPrice.Focus();
        }

        private void txtAllOutputPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnEditOutput.Focus();
        }

        private void btnTypeWeight_Click(object sender, EventArgs e)
        {
            if (double.Parse(txtTypeWeightAdd.Text) > 0)
            {
                Data.TypeWeight type = new Data.TypeWeight();
                int count = typeWeightBO.GetData(u => u.KG == int.Parse(txtTypeWeightAdd.Text)).Count();
                if (count == 0)
                {
                    type.KG = (int)double.Parse(txtTypeWeightAdd.Text);
                    bool check = typeWeightBO.Add(type);
                    if (check == true)
                    {
                        MessageBox.Show("Thêm sản thành công");
                        loadInformation();
                    }
                    else
                    {
                        MessageBox.Show("Thêm sản phẩm thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Đã tồn tại!");
                }
            }
        }

        private void btnRemoveTypeWeight_Click(object sender, EventArgs e)
        {
            if (txtTypeWeight.Text != string.Empty)
            {
                string masseage = "Bạn có muốn xóa không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    int ID = typeWeightBO.GetData(u => u.KG == int.Parse(txtTypeWeight.Text)).First().Type;
                    typeWeightBO.Delete(ID);
                    loadInformation();
                }
            }
        }
    }
}
