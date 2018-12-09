using BUS.Business;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmInventation : Form
    {
        #region Generate Filed
        TypeWeightBO typeWeightBO = new TypeWeightBO();
        List<QuantityNotDetail> ListQ = new List<QuantityNotDetail>();
        QuantityNotDetailBO quantityNotDetailBO = new QuantityNotDetailBO();
        private int ID;
        private int row = 0;
        #endregion

        #region Support
        private void loadTypeSack()
        {
            var listDB = typeWeightBO.GetData(u => u.Type != 0);
            txtType.Items.Clear();
            foreach (var item in listDB)
            {
                txtType.Items.Add(item.KG);
            }
        }

        private bool Validation()
        {
            if (txtSack.Text != "" && txtType.Text != "")
            {
                return true;
            }
            return false;
        }

        private void loadDG()
        {
            ListQ = quantityNotDetailBO.GetData(u => u.isDelete == false && u.QuantityID == ID).ToList();
            dataDS.Rows.Clear();
            int i = 0;
            foreach (var item in ListQ)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = item.Type;
                dataDS.Rows[i].Cells[1].Value = item.Weight;
                i++;
            }
        }
        #endregion 


        public frmInventation(int ID)
        {
            InitializeComponent();
            ListQ = quantityNotDetailBO.GetData(u => u.isDelete == false && u.QuantityID == ID).ToList();
            this.ID = ID;
        }

        private void frmInventation_Load(object sender, EventArgs e)
        {
            loadTypeSack();
            loadDG();
        }

        private void txtSack_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                List<string> arrList = new List<string>();
                int phannguyen = 0;
                int money = 0;
                bool checkNum = int.TryParse(txtSack.Text, out money);
                if (checkNum == false)
                {
                    string[] str = txtSack.Text.Split(',');
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
                txtSack.Text = result;
                txtSack.SelectionStart = result.Length;
            }
            catch
            { }
        }

        private void txtSack_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                QuantityNotDetail itemQND = new QuantityNotDetail();
                itemQND.Type = int.Parse(txtType.Text);
                itemQND.Weight = double.Parse(txtSack.Text);
                itemQND.QuantityID = ID;
                if (ListQ.Where(u=>u.Type==itemQND.Type).Count()==0)
                {
                    quantityNotDetailBO.Add(itemQND);
                    txtType_SelectedIndexChanged(sender, e);
                    loadDG();
                }
                else
                {
                    MessageBox.Show("Loại này đã nhập rồi");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool check=quantityNotDetailBO.Add(ListQ);
            if(check)
            {
                this.Close();
            }
            else
            {

                MessageBox.Show("Lỗi hệ thống");
            }
        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listDB = quantityNotDetailBO.GetData(u => u.isDelete == false && u.Type == int.Parse(txtType.Text));
            btnEdit.Visible = listDB.Count != 0 ? true : false;
        }

        private void dataDS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(row!=-1)
            {

                int type = int.Parse(dataDS.Rows[row].Cells[0].Value.ToString());
                string masseage = "Bạn có muốn xóa loại "+type+" không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    QuantityNotDetail itemQND = new QuantityNotDetail();
                    itemQND = ListQ.Where(u => u.isDelete == false && u.Type == type && u.QuantityID == ID).First();
                    itemQND.isDelete = true;
                    bool isCheck=quantityNotDetailBO.Update(itemQND);
                    if (isCheck == true)
                    {
                        loadDG();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống");
                    }
                }
            }
            row = -1;
        }

        private void dataDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataDS.Rows.Count - 1)
            {
                row = e.RowIndex;
            }
        }
    }
}