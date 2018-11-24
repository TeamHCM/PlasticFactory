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

namespace PlasticsFactory.UserControls.Main_Content.MCStatistic
{
    public partial class MCQuantity : UserControl
    {
        #region Generate Field
        QuantityBO quantityBO = new QuantityBO();
        TimekeepingBO timekeepingBO = new TimekeepingBO();
        ProductOutputBO productOutputBO = new ProductOutputBO();
        DetailQuantityBO detailQuantityBO = new DetailQuantityBO();
        DetailSackofQuantityBO detailSackofQuantityBO = new DetailSackofQuantityBO();
        TypeWeightBO typeWeightBO = new TypeWeightBO();
        private int Month = 0;
        private int Year = 0;
        #endregion

        #region Support 
        private void loadDG()
        {
            Month = txtDate.Value.Month;
            Year = txtDate.Value.Year;
            var listDB = quantityBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month && u.Date.Value.Year == Year);
            dataDS.Rows.Clear();
            int r = 0;
            foreach (var item in listDB)
            {
                dataDS.Rows.Add();
                dataDS.Rows[r].Cells[0].Value = item.Id;
                dataDS.Rows[r].Cells[1].Value = item.Date.Value.ToShortDateString();
                string Sack1 = "";
                string Sack2 = "";
                //Shift 
                var listShift = detailSackofQuantityBO.GetData(u => u.isDelete == false && u.Date == item.Date);
                foreach (var itemShift in listShift)
                {
                    if (itemShift.Shirt == 1)
                    {
                        Sack1 += itemShift.Sack + "(" + itemShift.Type + ")+";
                    }
                    else
                    {
                        Sack2 += itemShift.Sack + "(" + itemShift.Type + ")+";
                    }
                }
                dataDS.Rows[r].Cells[2].Value = Sack1.Length != 0 ? Sack1.Substring(0, Sack1.Length - 1) : "";
                dataDS.Rows[r].Cells[3].Value = Sack2.Length != 0 ? Sack2.Substring(0, Sack2.Length - 1) : "";
                dataDS.Rows[r].Cells[4].Value = item.TotalSack;
                dataDS.Rows[r].Cells[5].Value = item.TotalWeight;
                //Ouput
                var listOutput = detailQuantityBO.GetData(u => u.isDelete == false && u.Date == item.Date);
                string SackOut = "";
                foreach (var itemOut in listOutput)
                {
                    SackOut += itemOut.Sack + "(" + itemOut.Type + ")+";
                }
                dataDS.Rows[r].Cells[6].Value = SackOut.Length != 0 ? SackOut.Substring(0, SackOut.Length - 1) : "";
                dataDS.Rows[r].Cells[7].Value = item.TotalOuputWeight;
                dataDS.Rows[r].Cells[8].Value = item.TotalInventory;
                dataDS.Rows[r].Cells[9].Value = item.Note;
                r++;
            }
        }

        private void addDB()
        {
            //Quantity
            if (!IsDateofQuantity(txtDate.Value))
            {
                Quantity quantity = new Quantity();
                quantity.Date = txtDate.Value;
                quantity.Note = txtNote.Text;
                if (quantityBO.GetData(u =>u.isDelete==false&&u.Date.Value.Month == Month && u.Date.Value.Year == Year).Count() == 0)
                {
                    if (txtDate.Value.Month != 1)
                    {
                        var listTon = quantityBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month - 1 && u.Date.Value.Year == Year);
                        if (listTon.Count != 0)
                        {
                            quantity.TotalInventory = listTon.Last().TotalInventory;
                        }
                    }
                    else
                    {
                        var listTon = quantityBO.GetData(u => u.isDelete == false && u.Date.Value.Month == 12 && u.Date.Value.Year == Year-1);
                        if (listTon.Count != 0)
                        {
                            quantity.TotalInventory = listTon.Last().TotalInventory;
                        }
                    }
                }
                quantityBO.Add(quantity);
            }
            //DetailQuantity xuats hàng theo ca;
            if (gPreference.Visible == false || radOutput.Checked)
            {
                if (txtOutput.Text != string.Empty)
                {
                    DatailQuantity detailQuantity = new DatailQuantity();
                    detailQuantity.Date = txtDate.Value;
                    detailQuantity.Sack = double.Parse(txtOutput.Text);
                    detailQuantity.Type = double.Parse(txtTypeOuput.Text);
                    detailQuantity.Weight = double.Parse(txtOutput.Text) * double.Parse(txtTypeOuput.Text);
                    detailQuantityBO.Add(detailQuantity);
                }
            }

            if (gPreference.Visible == false || radShift.Checked)
            {
                if (txtSack1.Text != string.Empty || txtSack2.Text != string.Empty)
                {
                    //DetailSackofQuantity1 sẩn phẩm theo ca
                    if (txtSack1.Text != string.Empty)
                    {
                        Data.DetailSackofQuantity DetailShift1 = new DetailSackofQuantity();
                        DetailShift1.Date = txtDate.Value;
                        DetailShift1.Shirt = 1;
                        DetailShift1.Sack = double.Parse(txtSack1.Text != string.Empty ? txtSack1.Text : "0");
                        DetailShift1.Type = double.Parse(txtType1.Text);
                        DetailShift1.Weight = double.Parse(txtSack1.Text) * double.Parse(txtType1.Text);
                        detailSackofQuantityBO.Add(DetailShift1);
                    }
                    if (txtSack2.Text != string.Empty)
                    {
                        //DetailSackofQuantity2 sẩn phẩm theo ca
                        Data.DetailSackofQuantity DetailShift2 = new DetailSackofQuantity();
                        DetailShift2.Date = txtDate.Value;
                        DetailShift2.Shirt = 2;
                        DetailShift2.Sack = double.Parse(txtSack2.Text != string.Empty ? txtSack2.Text : "0");
                        DetailShift2.Type = double.Parse(txtType2.Text);
                        DetailShift2.Weight = double.Parse(txtSack2.Text) * double.Parse(txtType2.Text);
                        detailSackofQuantityBO.Add(DetailShift2);
                    }
                }
            }
        }

        private bool IsDateofQuantity(DateTime date)
        {
            int count = quantityBO.GetData(u => u.isDelete == false && u.Date == DateTime.Parse(date.ToShortDateString())).Count();
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        private bool Validation()
        {
            if (txtSack1.Text == "" && txtSack2.Text == "" && txtOutput.Text == "")
            {
                return false;
            }
            return true;
        }
        #endregion
        public MCQuantity()
        {
            InitializeComponent();
        }

        private void MCQuantity_Load(object sender, EventArgs e)
        {
            radShift.Checked = true;
            bool check = IsDateofQuantity(txtDate.Value);
            if (check == true)
            {
                gPreference.Visible = true;
                txtSack1.ResetText();
                txtSack2.ResetText();
                txtOutput.ResetText();
                txtSack1.Enabled = true;
                txtSack2.Enabled = true;
                txtType1.Enabled = true;
                txtType2.Enabled = true;
                txtTypeOuput.Enabled = false;
                txtOutput.Enabled = false;
            }
            else
            {
                gPreference.Visible = false;
                txtSack1.ResetText();
                txtSack2.ResetText();
                txtOutput.ResetText();
                txtSack1.Enabled = true;
                txtSack2.Enabled = true;
                txtType1.Enabled = true;
                txtType2.Enabled = true;
                txtTypeOuput.Enabled = true;
                txtOutput.Enabled = true;
            }
            loadDG();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                addDB();
                loadDG();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại");
            }
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            Month = txtDate.Value.Month;
            Year = txtDate.Value.Year;
            bool check = IsDateofQuantity(txtDate.Value);
            if (check == true)
            {
                gPreference.Visible = true;
                txtSack1.ResetText();
                txtSack2.ResetText();
                txtOutput.ResetText();
                txtSack1.Enabled = true;
                txtSack2.Enabled = true;
                txtType1.Enabled = true;
                txtType2.Enabled = true;
                txtTypeOuput.Enabled = false;
                txtOutput.Enabled = false;
            }
            else
            {
                gPreference.Visible = false;
                txtSack1.ResetText();
                txtSack2.ResetText();
                txtOutput.ResetText();
                txtSack1.Enabled = true;
                txtSack2.Enabled = true;
                txtType1.Enabled = true;
                txtType2.Enabled = true;
                txtTypeOuput.Enabled = true;
                txtOutput.Enabled = true;
            }
            loadDG();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radShift.Checked)
            {
                txtSack1.ResetText();
                txtSack2.ResetText();
                txtOutput.ResetText();
                txtSack1.Enabled = true;
                txtSack2.Enabled = true;
                txtType1.Enabled = true;
                txtType2.Enabled = true;
                txtTypeOuput.Enabled = false;
                txtOutput.Enabled = false;
            }
            else
            {
                txtSack1.ResetText();
                txtSack2.ResetText();
                txtOutput.ResetText();
                txtSack1.Enabled = false;
                txtSack2.Enabled = false;
                txtType1.Enabled = false;
                txtType2.Enabled = false;
                txtTypeOuput.Enabled = true;
                txtOutput.Enabled = true;
            }
        }

    }
}
