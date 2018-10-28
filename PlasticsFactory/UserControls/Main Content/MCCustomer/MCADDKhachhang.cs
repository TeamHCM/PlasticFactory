using BUS.Business;
using PlasticsFactory.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCCustomer
{
    public partial class MCADDKhachhang : UserControl
    {
        #region Generate Field

        public CustomerBO customer = new CustomerBO();
        public TruckBO truckBO = new TruckBO();
        public int btnDoubleGrid = 0;
        public Customer tempCustomer = new Customer();
        private int currentlyRows = 0;
        private int currentlyTruckRows = 0;
        public int btnDoubleGirdOfTruck = 0;
        public int IDofTruck = 0;

        #endregion Generate Field

        #region Susport

        public void LoadTypeofCustomer()
        {
            var listDB = customer.GetTypeofCustomer();
            txtTypeofCustomer.Items.Clear();
            foreach (var item in listDB)
            {
                txtTypeofCustomer.Items.Add(item);
            }
            txtTypeofCustomer.Text = listDB.First();
        }

        public string GetSex()
        {
            if (rdNam.Checked == true)
            {
                return "Nam";
            }
            return "Nữ";
        }

        public void LoadDG()
        {
            var listDB = customer.GetData(u => u.isDelete == false, u => u.TypeofCustomer);
            dataDS.Rows.Clear();

            int i = 0;
            foreach (var item in listDB)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = "KD" + item.ID.ToString("d6");
                dataDS.Rows[i].Cells[1].Value = item.Name;
                dataDS.Rows[i].Cells[2].Value = item.Sex;
                dataDS.Rows[i].Cells[3].Value = item.Phone.Trim();
                dataDS.Rows[i].Cells[4].Value = item.TypeofCustomer.Type;
                i++;
            }
        }

        public void Refreshs()
        {
            txtName.ResetText();
            txtSDT.ResetText();
            rdNam.Checked = true;
        }

        public int SplitMSKH(string MSKH)
        {
            return int.Parse(MSKH.Trim().Substring(2));
        }

        public void loadDataDGoftruck()
        {
            int MSKH = SplitMSKH(txtMSKHofTruck.Text);
            var listDB = truckBO.GetData(u => u.isDelete == false && u.MSKH == MSKH);
            int i = 0;
            dataDSoftruck.Rows.Clear();
            foreach (var item in listDB)
            {
                dataDSoftruck.Rows.Add();
                dataDSoftruck.Rows[i].Cells[0].Value = i;
                dataDSoftruck.Rows[i].Cells[1].Value = item.LicencePlate;
                dataDSoftruck.Rows[i].Cells[2].Value = item.Weight;
                dataDSoftruck.Rows[i].Cells[3].Value = item.MSKH;
                i++;
            }
        }

        public void RefreshTruck()
        {
            txtMSKHofTruck.ResetText();
            txtWeightofTruck.ResetText();
            txtLicencePlate.ResetText();
            dataDSoftruck.Rows.Clear();
        }

        public void RefreshTruckNoRemoveMSKH()
        {
            txtWeightofTruck.ResetText();
            txtLicencePlate.ResetText();
            dataDSoftruck.Rows.Clear();
        }

        #endregion Susport

        public MCADDKhachhang()
        {
            InitializeComponent();
        }

        private void txtTypeofCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void MCADDKhachhang_Load(object sender, EventArgs e)
        {
            LoadTypeofCustomer();
            LoadDG();
            txtMSKH.Text = customer.GetID();
            txtName.Focus();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                tempCustomer = tempCustomer ?? new Customer();
                tempCustomer.Name = txtName.Text;
                tempCustomer.Sex = GetSex();
                tempCustomer.Phone = txtSDT.Text;
                tempCustomer.Type = customer.GetIDByType(txtTypeofCustomer.Text);
                tempCustomer.isDelete = false;
                customer.Add(tempCustomer);
                txtMSKH.Text = customer.GetID();
                RefreshTruck();
                Refreshs();
                LoadDG();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tối thiểu tên của khách hàng");
            }
        }

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dataDS.RowCount - 1)
            {
                switch (btnDoubleGrid)
                {
                    case 0:
                        try
                        {
                            btnEdit.Visible = true;
                            txtMSKH.Text = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                            txtName.Text = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
                            txtTypeofCustomer.Text = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString();
                            txtSDT.Text = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                            if (dataDS.Rows[e.RowIndex].Cells[2].Value.ToString() == "Nam")
                            {
                                rdNam.Checked = true;
                            }
                            else
                            {
                                rdNu.Checked = true;
                            }
                            btnRemove.Enabled = false;
                            btnAdditional.Enabled = false;
                        }
                        catch
                        {
                        }
                        btnDoubleGrid = 1;
                        break;

                    case 1:
                        txtMSKH.Text = customer.GetID();
                        LoadDG();
                        Refresh();
                        btnRemove.Enabled = true;
                        btnAdditional.Enabled = true;
                        btnEdit.Visible = false;
                        btnDoubleGrid = 0;
                        btnThem.Visible = true;
                        break;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            tempCustomer = new Customer();
            tempCustomer.ID = SplitMSKH(txtMSKH.Text);
            tempCustomer.Name = txtName.Text;
            tempCustomer.Phone = txtSDT.Text;
            tempCustomer.Sex = GetSex();
            tempCustomer.Type = customer.GetIDByType(txtTypeofCustomer.Text);
            tempCustomer.isDelete = false;
            customer.Update(tempCustomer);
            //refresh
            btnRemove.Enabled = true;
            btnAdditional.Enabled = true;
            btnEdit.Visible = false;
            btnThem.Visible = true;
            Refresh();
            LoadDG();
            txtMSKH.Text = customer.GetID();
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            gTruckInformation.Enabled = false;
            try
            {
                txtMSKHofTruck.Text = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch { }
            RefreshTruckNoRemoveMSKH();
            currentlyRows = e.RowIndex;
        }

        private void btnAdditional_Click(object sender, EventArgs e)
        {
            if (currentlyRows < dataDS.Rows.Count - 1)
            {
                gTruckInformation.Enabled = true;
                RefreshTruck();
                txtMSKHofTruck.Text = dataDS.Rows[currentlyRows].Cells[0].Value.ToString();
                loadDataDGoftruck();
                txtLicencePlate.Focus();
            }
        }

        private void btnAditoftruck_Click(object sender, EventArgs e)
        {
            if (txtLicencePlate.Text.Equals("") || txtWeightofTruck.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            }
            else
            {
                bool isCheck = truckBO.isExistLicencePlate(txtLicencePlate.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Biển số xe đã tồn tại vui lòng kiểm tra lại");
                    txtLicencePlate.ResetText();
                    txtWeightofTruck.ResetText();
                }
                else
                {
                    Truck truck = new Truck();
                    truck.MSKH = SplitMSKH(txtMSKHofTruck.Text);
                    truck.LicencePlate = txtLicencePlate.Text;
                    truck.Weight = int.Parse(txtWeightofTruck.Text);
                    truckBO.Add(truck);
                    RefreshTruckNoRemoveMSKH();
                    loadDataDGoftruck();
                }
            }
        }

        private void txtWeightofTruck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int ID = SplitMSKH(dataDS.Rows[currentlyRows].Cells[0].Value.ToString());
            bool ischeck = customer.isDelete(ID);
            if (ischeck == false)
            {
                MessageBox.Show("Lỗi không xóa được");
            }
            RefreshTruck();
            LoadDG();
        }

        private void txtTypeofCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataDSoftruck_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataDSoftruck.Rows.Count - 1)
            {
                switch (btnDoubleGirdOfTruck)
                {
                    case 0:
                        if (e.RowIndex < dataDSoftruck.Rows.Count - 1)
                        {
                            txtLicencePlate.Text = dataDSoftruck.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                            txtWeightofTruck.Text = dataDSoftruck.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                            currentlyTruckRows = truckBO.GetIDByNameLicence(dataDSoftruck.Rows[e.RowIndex].Cells[1].Value.ToString());
                        }
                        else
                        {
                            currentlyTruckRows = 0;
                        }
                        btnDoubleGirdOfTruck = 1;
                        btnAditoftruck.Enabled = false;
                        break;

                    case 1:
                        btnDoubleGirdOfTruck = 0;
                        btnAditoftruck.Enabled = true;
                        txtLicencePlate.ResetText();
                        txtWeightofTruck.ResetText();
                        break;
                }
            }
        }

        private void btnSaveoftruck_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentlyTruckRows != 0)
                {
                    Truck truck = new Truck();
                    truck.ID = currentlyTruckRows;
                    truck.isDelete = false;
                    truck.LicencePlate = txtLicencePlate.Text;
                    truck.Weight = int.Parse(txtWeightofTruck.Text);
                    truck.MSKH = SplitMSKH(txtMSKHofTruck.Text);
                    truckBO.Update(truck);
                    btnAditoftruck.Enabled = true;
                    RefreshTruckNoRemoveMSKH();
                    loadDataDGoftruck();
                }
            }
            catch { }
        }

        private void btnRemoveoftruck_Click(object sender, EventArgs e)
        {
            if (IDofTruck != 0)
            {
                truckBO.Delete(IDofTruck);
                RefreshTruckNoRemoveMSKH();
                loadDataDGoftruck();
            }
        }

        private void dataDSoftruck_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < dataDSoftruck.Rows.Count - 1)
                {
                    IDofTruck = truckBO.GetIDByNameLicence(dataDSoftruck.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
            catch { }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSDT.Focus();
            }
        }

        private void txtSDT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.Focus();
                btnEdit.Focus();
            }
        }

        private void txtLicencePlate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWeightofTruck.Focus();
            }
        }

        private void txtWeightofTruck_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAditoftruck.Focus();
            }
        }
    }
}