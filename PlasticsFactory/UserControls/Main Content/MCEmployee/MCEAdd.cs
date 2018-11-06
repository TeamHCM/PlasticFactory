using BUS.Business;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCEmployee
{
    public partial class MCEAdd : UserControl
    {
        #region generate Field

        private List<Employee> list = new List<Employee>();
        private EmployeeBO employeeBO;
        private int tempClickDS = 0;
        private string tempMSNV = "";
        private string msnvBO;

        #endregion generate Field



        #region method support

        public string GetSex()
        {
            if (rdNam.Checked == true)
            {
                return "Nam";
            }
            return "Nữ";
        }

        public string BirthDay(DateTime date)
        {
            if (date.Year >= DateTime.Now.Year)
            {
                return "";
            }
            return date.ToShortDateString();
        }

        public void RefreshInformation()
        {
            #region Reset Nhập thông tin

            lbWarn.Visible = false;
            txtMSNV.Text = GetMSNV();
            txtName.ResetText();
            txtSDT.ResetText();
            txtDiachi.ResetText();
            txtCMND.ResetText();
            txtBirthDay.ResetText();
            rdNam.Checked = true;
            radByDay.Checked = true;
            #endregion Reset Nhập thông tin
        }

        public void LoadListEmployee()
        {
            int i = 0;
            dataDS.Rows.Clear();
            //Sắp xep sort implement lại ICompare
            list.Sort(new EmployeeComparer());
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = i.ToString();
                dataDS.Rows[i].Cells[1].Value = item.MSNV;
                dataDS.Rows[i].Cells[2].Value = item.Hoten;
                dataDS.Rows[i].Cells[3].Value = BirthDay(item.Ngaysinh.Value);
                dataDS.Rows[i].Cells[4].Value = item.Gioitinh;
                dataDS.Rows[i].Cells[5].Value = item.Diachi;
                dataDS.Rows[i].Cells[6].Value = item.CMND;
                dataDS.Rows[i].Cells[7].Value = item.SDT;
                if(item.Type==1)
                {
                    dataDS.Rows[i].Cells[8].Value = "Tháng";
                }
                else
                {
                    dataDS.Rows[i].Cells[8].Value = "Ngày";
                }
                i++;
            }
        }

        public string GetMSNV()
        {
            string[] arrStr = employeeBO.AutoGetMSNV().Split('V');
            int i = int.Parse(arrStr[1]);
            string msnv = "";
            int tempWhile = 0;
            while (tempWhile != 1)
            {
                msnv = "NV" + i.ToString("D3");
                var check = list.FirstOrDefault(u => u.MSNV == msnv);
                if (check == null)
                {
                    tempWhile = 1;
                }
                i++;
            };
            return msnv;
        }

        private int GetTypeWork()
        {
            if (radByDay.Checked)
            {
                return 2;
            }
            return 1;
        }

        #endregion method support

        public MCEAdd()
        {
            InitializeComponent();
            employeeBO = employeeBO ?? new EmployeeBO();
            msnvBO = employeeBO.AutoGetMSNV();
        }

        #region Event Button

        private void btnThem_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            DateTime DateBirth = DateTime.Now;
            string Sex = GetSex();
            string CMND = txtCMND.Text;
            if (CMND.Length != 9)
            {
                if (CMND.Length != 12)
                {
                    CMND = "";
                }
            }
            try
            {
                DateBirth = DateTime.Parse(txtBirthDay.Text);
            }
            catch
            {
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tối thiểu Họ và tên nhân viên");
            }
            else
            {
                employee.MSNV = txtMSNV.Text;
                employee.Hoten = txtName.Text;
                employee.Gioitinh = Sex;
                employee.SDT = txtSDT.Text;
                employee.CMND = CMND;
                employee.Diachi = txtDiachi.Text;
                employee.Ngaysinh = DateBirth;
                employee.Type = GetTypeWork();
                list.Add(employee);

                #region load dataGridview

                LoadListEmployee();

                #endregion load dataGridview

                #region Reset Nhập thông tin

                RefreshInformation();

                #endregion Reset Nhập thông tin
            }
        }

        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void MCEAdd_Load(object sender, EventArgs e)
        {
            txtMSNV.Text = employeeBO.AutoGetMSNV();
            txtName.Select();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCMND_TextChanged(object sender, EventArgs e)
        {
            if (txtCMND.Text.Length != 9)
            {
                if (txtCMND.Text.Length != 12)
                    lbWarn.Visible = true;
            }
            if (txtCMND.Text.Length == 12 || txtCMND.Text.Length == 9 || txtCMND.Text.Length == 0)
            {
                lbWarn.Visible = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //lấy mã số nhân viên cần update từ list
            var updateEmployee = list.FirstOrDefault(u => u.MSNV == txtMSNV.Text);
            DateTime DateBirth = DateTime.Now;
            string Sex = GetSex();
            string CMND = txtCMND.Text;
            if (CMND.Length != 9)
            {
                if (CMND.Length != 12)
                {
                    CMND = "";
                }
            }
            try
            {
                DateBirth = DateTime.Parse(txtBirthDay.Text);
            }
            catch
            {
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tối thiểu Họ và tên nhân viên");
            }
            else
            {
                #region Update Employee

                updateEmployee.MSNV = txtMSNV.Text;
                updateEmployee.Hoten = txtName.Text;
                updateEmployee.Gioitinh = Sex;
                updateEmployee.SDT = txtSDT.Text;
                updateEmployee.CMND = CMND;
                updateEmployee.Diachi = txtDiachi.Text;
                updateEmployee.Ngaysinh = DateBirth;
                updateEmployee.Type = GetTypeWork();
                #endregion Update Employee

                //load datagridview
                LoadListEmployee();
                RefreshInformation();
                btnRemove.Enabled = true;
                btnEdit.Visible = false;
            }
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
                            btnEdit.Visible = true;
                            txtMSNV.Text = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
                            txtName.Text = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
                            txtBirthDay.Text = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                            if (dataDS.Rows[e.RowIndex].Cells[4].Value.ToString() == "Nam")
                            {
                                rdNam.Checked = true;
                            }
                            else
                            {
                                rdNu.Checked = true;
                            }
                            txtDiachi.Text = dataDS.Rows[e.RowIndex].Cells[5].Value.ToString();
                            txtCMND.Text = dataDS.Rows[e.RowIndex].Cells[6].Value.ToString();
                            txtSDT.Text = dataDS.Rows[e.RowIndex].Cells[7].Value.ToString();
                            if(dataDS.Rows[e.RowIndex].Cells[8].Value.ToString()=="Ngày")
                            {
                                radByDay.Checked = true;
                            }
                            else
                            {
                                radByMonth.Checked = true;
                            }
                            btnRemove.Enabled = false;
                        }
                        catch
                        {
                        }
                        tempClickDS = 1;
                        break;

                    case 1:
                        RefreshInformation();
                        btnEdit.Visible = false;
                        tempClickDS = 0;
                        btnRemove.Enabled = true;
                        break;
                }
            }
        }

        private void dataDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    tempMSNV = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
            //}
            //catch { }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tempMSNV != "")
            {
                string masseage = "Bạn có muốn xóa nhân viên " + tempMSNV + " không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var deleteEmployee = list.FindIndex(u => u.MSNV == tempMSNV);
                    list.RemoveAt(deleteEmployee);
                    //load datagridview
                    LoadListEmployee();
                    txtMSNV.Text = GetMSNV();
                }
            }
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tempMSNV = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch
            {
                //Khi dòng cuối thì gán để không bị lỗi
                tempMSNV = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            employeeBO.Add(list);
            txtMSNV.Text = employeeBO.AutoGetMSNV();
            list.Clear();
            dataDS.Rows.Clear();
        }

        #endregion Event Button

        #region Focus

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBirthDay.Focus();
            }
            if (e.KeyCode == Keys.Tab)
            {
                btnThem.Focus();
            }
        }

        private void txtBirthDay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiachi.Focus();
            }
        }

        private void txtDiachi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCMND.Focus();
            }
        }

        private void txtCMND_KeyUp(object sender, KeyEventArgs e)
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
            }
        }

        #endregion Focus
    }
}