using BUS.Business;
using PlasticsFactory.Data;
using PlasticsFactory.UserControls.Main_Content.MCChamcong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmEdit : Form
    {
        #region Generate Field

        public TimekeepingBO timekeepingBO = new TimekeepingBO();
        public DateTime Now = DateTime.Now.Date;

        #endregion Generate Field

        #region Support

        public void loadWeight()
        {
            List<TypeWeight> list = timekeepingBO.GetWeight();
            foreach (var item in list)
            {
                txtTypeWeight.Items.Add(item.KG);
            }
            txtTypeWeight.Text = list.First().KG.ToString();
        }

        public float Interval(string timeStart, string timeEnd)
        {
            string[] str1 = timeStart.Split(':');
            string[] str2 = timeEnd.Split(':');
            float hourStart = float.Parse(str1[0]);
            float hourEnd = float.Parse(str2[0]);
            float minuteStart = float.Parse(str1[1]);
            float minuteEnd = float.Parse(str2[1]);
            hourStart = hourStart + (minuteStart / 60);
            hourEnd = hourEnd + (minuteEnd / 60);
            if (hourStart <= hourEnd)
            {
                return hourEnd - hourStart;
            }
            else
            {
                return (24 - hourStart) + hourEnd;
            }
        }

        #endregion Support

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            var temp = Management.tempUpdate;
            txtMSNV.Text = temp.MSNV;
            txtThoigianBD.Text = temp.TimeStart;
            txtThoigianKT.Text = temp.TimeEnd;
            txtWeight.Text = temp.Weight.ToString();
            txtTypeWeight.Text = temp.Type.ToString();
            txtNote.Text = temp.Note;
            txtCashAdvance.Text = temp.AdvancePayment.Value.ToString();
        }

        #region Event Time

        private void txtThoigianBD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txtTimeStart = txtThoigianBD.Text;
                int lenght = txtTimeStart.Length;
                if (lenght > 0 && lenght <= 2)
                {
                    int hour = int.Parse(txtTimeStart);
                    txtThoigianBD.Text = hour.ToString("d2") + ":00";
                    txtThoigianKT.Focus();
                }
                if (lenght > 3)
                {
                    int minutes = int.Parse(txtTimeStart.Substring(3));
                    int hours = int.Parse(txtTimeStart.Substring(0, 2));
                    if (minutes < 10)
                    {
                        txtThoigianBD.Text = hours.ToString("d2") + ":" + minutes.ToString("d2");
                        txtThoigianKT.Focus();
                    }
                    if (minutes < 60)
                    {
                        txtThoigianKT.Focus();
                    }
                    if (minutes >= 60)
                    {
                        txtThoigianBD.Text = "";
                    }
                }
                if (lenght == 3)
                {
                    txtThoigianBD.Text = "";
                }
            }
            if (txtThoigianBD.Text == "" && e.KeyCode == Keys.Space)
            {
                txtWeight.Focus();
            }
        }

        private void txtThoigianBD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtThoigianBD.Text.Length < 2)
            {
                if (!Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
            }
            if (txtThoigianBD.Text.Length == 2)
            {
                if (e.KeyChar.ToString() != ":")
                {
                    e.Handled = true;
                }
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
            }
            if (txtThoigianBD.Text.Length > 2)
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (txtThoigianBD.Text.Length > 4)
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtThoigianBD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtThoigianBD.Text) > 23)
                {
                    txtThoigianBD.ResetText();
                }
            }
            catch
            { }
        }

        private void txtThoigianKT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txtTimeEnd = txtThoigianKT.Text;
                int lenght = txtTimeEnd.Length;
                if (lenght > 0 && lenght <= 2)
                {
                    int hour = int.Parse(txtTimeEnd);
                    txtThoigianKT.Text = hour.ToString("d2") + ":00";
                }
                if (lenght > 3)
                {
                    int minutes = int.Parse(txtTimeEnd.Substring(3));
                    int hours = int.Parse(txtTimeEnd.Substring(0, 2));
                    if (minutes < 10)
                    {
                        txtThoigianKT.Text = hours.ToString("d2") + ":" + minutes.ToString("d2");
                        txtWeight.Focus();
                    }
                    if (minutes < 60)
                    {
                        txtWeight.Focus();
                    }
                    if (minutes >= 60)
                    {
                        txtThoigianKT.Text = "";
                    }
                }
            }
        }

        private void txtThoigianKT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtThoigianKT.Text.Length < 2)
            {
                if (!Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
            }
            if (txtThoigianKT.Text.Length == 2)
            {
                if (e.KeyChar.ToString() != ":")
                {
                    e.Handled = true;
                }
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
            }
            if (txtThoigianKT.Text.Length > 2)
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (txtThoigianKT.Text.Length > 4)
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtThoigianKT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtThoigianKT.Text) > 23)
                {
                    txtThoigianKT.ResetText();
                }
            }
            catch
            { }
        }

        #endregion Event Time

        #region Event Weight

        private void txtTypeWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCashAdvance.Focus();
            }
        }

        #endregion Event Weight

        #region Event CashAdvance

        private void txtCashAdvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCashAdvance.Text.Length > 12)
            {
                e.Handled = true;
            }
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void txtCashAdvance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                List<string> arrList = new List<string>();
                int phannguyen = 0;
                int money = 0;
                bool checkNum = int.TryParse(txtCashAdvance.Text, out money);
                if (checkNum == false)
                {
                    string[] str = txtCashAdvance.Text.Split(',');
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
                txtCashAdvance.Text = result;
                txtCashAdvance.SelectionStart = result.Length;
            }
            catch
            { }
        }

        private void txtCashAdvance_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNote.Focus();
            }
        }

        #endregion Event CashAdvance

        #region Event Note

        private void txtNote_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit.Focus();
            }
        }

        #endregion Event Note

        #region Button

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Timekeeping timekeeping = new Timekeeping();
            timekeeping.isDelete = false;
            timekeeping.MSNV = txtMSNV.Text;
            timekeeping.Id = Management.tempUpdate.Id;
            timekeeping.Date = Management.tempUpdate.Date;
            if (txtThoigianBD.Text.Equals("") || txtThoigianKT.Text.Equals(""))
            {
                timekeeping.TimeStart = "";
                timekeeping.TimeEnd = "";
                timekeeping.Time = 0;
            }
            else
            {
                timekeeping.TimeStart = txtThoigianBD.Text;
                timekeeping.TimeEnd = txtThoigianKT.Text;
                timekeeping.Time = Interval(txtThoigianBD.Text, txtThoigianKT.Text);
            }
            if (txtWeight.Text.Equals(""))
            {
                timekeeping.Weight = 0;
                timekeeping.TotalWeight = "0";
            }
            else
            {
                timekeeping.Weight = int.Parse(txtWeight.Text);
                timekeeping.TotalWeight = (int.Parse(txtWeight.Text) * int.Parse(txtTypeWeight.Text)).ToString();
            }
            timekeeping.Type = int.Parse(txtTypeWeight.Text);
            if (txtCashAdvance.Text.Equals(""))
            {
                timekeeping.AdvancePayment = 0;
            }
            else
            {
                string[] strCash = txtCashAdvance.Text.Split(',');
                string str = "";
                foreach (var item in strCash)
                {
                    str += item;
                }
                timekeeping.AdvancePayment = int.Parse(str);
            }
            timekeeping.Note = txtNote.Text;
            timekeepingBO.Update(timekeeping);
            this.Close();
        }

        #endregion Button
    }
}