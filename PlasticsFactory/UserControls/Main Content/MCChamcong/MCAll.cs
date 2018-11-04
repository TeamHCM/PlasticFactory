using BUS.Business;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCChamcong
{
    public partial class MCAll : UserControl
    {
        #region field

        public List<Timekeeping> list = new List<Timekeeping>();
        public TimekeepingBO timekeepingBO = new TimekeepingBO();
        public Timekeeping timekeeping;
        public int btnDoubleGrid = 0;
        private string tempMSNV = "";
        private string dateByMSNV = "";

        #endregion field

        #region support

        public void LoadAutoRefreshInformation()
        {
            loadListTimekeeping();
            txtThoigianBD.ResetText();
            txtThoigianKT.ResetText();
            txtWeight.ResetText();
            txtCashAdvance.ResetText();
            txtNote.ResetText();
            txtNgay.Text = DateTime.Now.Day.ToString();
            txtThang.Text = DateTime.Now.Month.ToString();
            txtNam.Text = DateTime.Now.Year.ToString();
            tempMSNV = "";
            dateByMSNV = "";
            txtHoten.Focus();
        }

        public void loadListTimekeeping()
        {
            int i = 0;
            dataDS.Rows.Clear();
            foreach (var item in list)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = item.MSNV;
                dataDS.Rows[i].Cells[1].Value = timekeepingBO.GetNameEmployee(item.MSNV);
                dataDS.Rows[i].Cells[2].Value = item.Date.Value.ToShortDateString();
                dataDS.Rows[i].Cells[3].Value = item.TimeStart;
                dataDS.Rows[i].Cells[4].Value = item.TimeEnd;
                dataDS.Rows[i].Cells[5].Value = item.Time;
                dataDS.Rows[i].Cells[6].Value = item.Weight;
                dataDS.Rows[i].Cells[7].Value = item.Type;
                dataDS.Rows[i].Cells[8].Value = item.TotalWeight;
                dataDS.Rows[i].Cells[9].Value = item.AdvancePayment;
                dataDS.Rows[i].Cells[10].Value = item.Food;
                dataDS.Rows[i].Cells[11].Value = item.Punish;
                dataDS.Rows[i].Cells[12].Value = item.Bunus;
                dataDS.Rows[i].Cells[13].Value = item.Note;
                if (item.isRest == true)
                {
                    dataDS.Rows[i].Cells[14].Value = "Yes";
                }
                else
                {
                    dataDS.Rows[i].Cells[14].Value = "No";
                }
                i++;
            }
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

        public Timekeeping ObjectTimekeeping()
        {
            Timekeeping timekeeping = new Timekeeping();
            timekeeping.MSNV = txtMSNV.Text;
            timekeeping.Date = DateTime.Parse(txtNgay.Text + "/" + txtThang.Text + "/" + txtNam.Text).Date;
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
                timekeeping.TotalWeight =0;
            }
            else
            {
                timekeeping.Weight = int.Parse(txtWeight.Text);
                timekeeping.TotalWeight = (int.Parse(txtWeight.Text) * int.Parse(txtTypeWeight.Text));
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
            timekeeping.Food = txtFood.Text!=string.Empty? double.Parse(txtFood.Text):0;
            timekeeping.Punish =txtPunish.Text!=string.Empty? double.Parse(txtPunish.Text):0;
            timekeeping.Bunus = txtBonus.Text!=string.Empty?double.Parse(txtBonus.Text):0;
            if(checkRest.Checked)
            {
                timekeeping.isRest = true;
            }
            if(!checkRest.Checked)
            {
                timekeeping.isRest = false;
            }
            timekeeping.Note = txtNote.Text;
            timekeeping.isDelete = false;
            return timekeeping;
        }

        public void loadWeight()
        {
            List<TypeWeight> list = timekeepingBO.GetWeight();
            foreach (var item in list)
            {
                txtTypeWeight.Items.Add(item.KG);
            }
            txtTypeWeight.Text = list.First().KG.ToString();
        }

        public void loadMSNV(string Name)
        {
            txtMSNV.Items.Clear();
            List<string> list = timekeepingBO.GetIdByName(Name);
            foreach (var item in list)
            {
                txtMSNV.Items.Add(item);
            }
            txtMSNV.Text = list.First();
        }

        public void loadDay(int month, int year)
        {
            int day = DateTime.DaysInMonth(year, month);
            int maxDay = DateTime.DaysInMonth(year, month);
            int txtDay = int.Parse(txtNgay.Text);
            txtNgay.Items.Clear();
            for (int i = 1; i <= day; i++)
            {
                txtNgay.Items.Add(i);
            }
            if (maxDay < txtDay)
            {
                txtNgay.Text = "";
            }
        }

        public void loadMonth()
        {
            txtThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                txtThang.Items.Add(i);
            }
        }

        public void loadYear()
        {
            int Year = DateTime.Now.Year;
            for (int i = Year - 10; i <= Year; i++)
            {
                txtNam.Items.Add(i);
            }
        }

        public void loadDayMonthYear()
        {
            txtNgay.Text = DateTime.Now.Day.ToString();
            txtThang.Text = DateTime.Now.Month.ToString();
            txtNam.Text = DateTime.Now.Year.ToString();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            loadDay(month, year);
            loadMonth();
            loadYear();
        }

        public void loadHoten()
        {
            List<string> listName = timekeepingBO.GetNameEmployee();
            foreach (var item in listName)
            {
                txtHoten.Items.Add(item);
            }
        }

        public bool IsEmployeeByDateInList(string MSNV, string Date)
        {
            int result = list.Count(u => u.MSNV == MSNV && u.Date == DateTime.Parse(Date));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        #endregion support

        public MCAll()
        {
            InitializeComponent();
        }

        private void MCAll_Load(object sender, EventArgs e)
        {
            txtHoten.Focus();
            loadDayMonthYear();
            loadHoten();
            loadWeight();
        }

        #region Event Infomation Employee

        private void txtThang_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int month = int.Parse(txtThang.Text);
                int year = int.Parse(txtNam.Text);
                if (e.KeyCode == Keys.Enter)
                {
                    if (month > 12 || month <= 0)
                    {
                        txtThang.Text = "";
                    }
                    else
                    {
                        loadDay(month, year);
                        txtNam.Focus();
                    }
                }
                if (e.KeyCode == Keys.Space)
                {
                    if (month > 12 || month <= 0)
                    {
                        txtThang.Text = "";
                    }
                    else
                    {
                        txtThoigianBD.Focus();
                    }
                }
            }
            catch { }
        }

        private void txtThang_SelectedValueChanged(object sender, EventArgs e)
        {
            //Ignore errors when the program compiles for the first time
            try
            {
                int month = int.Parse(txtThang.Text);
                int year = int.Parse(txtNam.Text);
                loadDay(month, year);
            }
            catch
            {
            }
        }

        private void txtNgay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int day = int.Parse(txtNgay.Text);
                int txtmonth = int.Parse(txtThang.Text);
                int txtYear = int.Parse(txtNam.Text);
                int maxDay = DateTime.DaysInMonth(txtYear, 2);
                IEnumerable<int> month = TimekeepingBO.Month(day);
                txtThang.Items.Clear();
                txtThang.Text = txtThang.Text;
                foreach (var item in month)
                {
                    txtThang.Items.Add(item);
                }
                if (maxDay < int.Parse(txtNgay.Text))
                {
                    txtThang.Items.Remove(2);
                }
            }
            catch { }
        }

        private void txtNam_SelectedValueChanged(object sender, EventArgs e)
        {
            int month = int.Parse(txtThang.Text);
            int year = int.Parse(txtNam.Text);
            loadDay(month, year);
        }

        private void txtNgay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNgay_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int numDay = int.Parse(txtNgay.Text);
                int numYear = int.Parse(txtNam.Text);
                int numMonth = int.Parse(txtThang.Text);
                int maxDay = DateTime.DaysInMonth(numYear, numMonth);
                if (e.KeyCode == Keys.Enter)
                {
                    if (numDay > maxDay)
                    {
                        txtNgay.Text = "";
                    }
                    txtThang.Focus();
                }
                if (e.KeyCode == Keys.Space)
                {
                    txtThoigianBD.Focus();
                }
            }
            catch { }
        }

        private void txtMSNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNam_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                int Year = DateTime.Now.Year;
                int Year10 = Year - 10;
                int numYear = int.Parse(txtNam.Text);
                if (numYear > Year || numYear < Year10)
                {
                    txtNam.Text = Year.ToString();
                }
                txtThoigianBD.Focus();
            }
        }

        private void txtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtHoten_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string name = txtHoten.Text;
                bool checkExist = timekeepingBO.checkNameEmployee(name);
                if (checkExist == true)
                {
                    loadMSNV(name);
                    txtNgay.Focus();
                }
                else
                {
                    txtHoten.Text = "";
                    txtMSNV.Text = "";
                    txtMSNV.Items.Clear();
                }
            }
        }

        private void txtHoten_Leave(object sender, EventArgs e)
        {
            string name = txtHoten.Text;
            bool checkExist = timekeepingBO.checkNameEmployee(name);
            if (checkExist == true)
            {
                loadMSNV(name);
                txtNgay.Focus();
            }
            else
            {
                txtHoten.Text = "";
                txtMSNV.Text = "";
                txtMSNV.Items.Clear();
            }
        }

        #endregion Event Infomation Employee

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
                txtFood.Focus();
            }
        }
        #endregion Event CashAdvance

        #region Event DataGridViews

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
                            txtMSNV.Text = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                            txtHoten.Text = dataDS.Rows[e.RowIndex].Cells[1].Value.ToString();
                            DateTime dateRows = DateTime.Parse(dataDS.Rows[e.RowIndex].Cells[2].Value.ToString());
                            txtNgay.Text = dateRows.Day.ToString();
                            txtThang.Text = dateRows.Month.ToString();
                            txtNam.Text = dateRows.Year.ToString();
                            if (dataDS.Rows[e.RowIndex].Cells[3].Value.ToString() == "")
                            {
                                txtThoigianBD.Text = "";
                            }
                            else
                            {
                                txtThoigianBD.Text = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                            }
                            if (dataDS.Rows[e.RowIndex].Cells[4].Value.ToString() == "")
                            {
                                txtThoigianKT.Text = "";
                            }
                            else
                            {
                                txtThoigianKT.Text = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString();
                            }
                            txtWeight.Text = dataDS.Rows[e.RowIndex].Cells[6].Value.ToString();
                            txtTypeWeight.Text = dataDS.Rows[e.RowIndex].Cells[7].Value.ToString();
                            txtCashAdvance.Text = dataDS.Rows[e.RowIndex].Cells[9].Value.ToString();
                            txtNote.Text = dataDS.Rows[e.RowIndex].Cells[10].Value.ToString();
                            btnRemove.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        catch
                        {
                        }
                        txtNgay.Enabled = false;
                        txtThang.Enabled = false;
                        txtNam.Enabled = false;
                        btnDoubleGrid = 1;
                        break;

                    case 1:
                        txtNgay.Enabled = true;
                        txtThang.Enabled = true;
                        txtNam.Enabled = true;
                        btnRemove.Enabled = true;
                        btnSave.Enabled = true;
                        LoadAutoRefreshInformation();
                        btnEdit.Visible = false;
                        btnDoubleGrid = 0;
                        btnThem.Visible = true;
                        break;
                }
            }
        }

        private void txtNote_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.Focus();
                btnEdit.Focus();
            }
        }

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tempMSNV = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                dateByMSNV = dataDS.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch
            {
                //Khi dòng cuối thì gán để không bị lỗi
                tempMSNV = "";
                dateByMSNV = "";
            }
        }


        #endregion Event DataGridViews

        #region Button

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tempMSNV != "")
            {
                string masseage = "Bạn có muốn xóa chấm công nhân viên " + tempMSNV.Trim() + "không ?";
                string Title = "Chú ý";
                DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    DateTime date = DateTime.Parse(dateByMSNV);
                    var deleteEmployee = list.FindIndex(u => u.MSNV == tempMSNV && u.Date == date);
                    list.RemoveAt(deleteEmployee);
                    //load datagridview
                    LoadAutoRefreshInformation();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            timekeepingBO.Add(list);
            list.Clear();
            LoadAutoRefreshInformation();
            dataDS.Rows.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMSNV.Text.Equals("") || txtNgay.Text.Equals("") || txtThang.Text.Equals("") || txtNam.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin nhân viên");
            }
            else
            {
                string Date = txtNgay.Text + "/" + txtThang.Text + "/" + txtNam.Text;
                bool IsFieldEmployeeByDateInList = IsEmployeeByDateInList(txtMSNV.Text, Date);
                bool IsFieldEmployeeByDateInDB = timekeepingBO.IsEmployeeByDateDB(txtMSNV.Text, Date);
                if (IsFieldEmployeeByDateInDB || IsFieldEmployeeByDateInList)
                {
                    MessageBox.Show("Nhân viên đã nãy đã được chấm công vào này rồi trong DataBase hoặc là trong danh sách mới thêm. Vui lòng kiểm tra lại");
                }
                else
                {
                    if ((txtThoigianKT.Text.Equals("") || txtThoigianBD.Text.Equals("")) && txtWeight.Text.Equals(""))
                    {
                        //code giúp kiểm tra  lỗi nuế ở treên rỗng mà trường họcw nhập check vào nghỉ thì ok vẫn cho thêm vài chấm công
                        if (checkRest.Checked)
                        {
                            timekeeping = ObjectTimekeeping();
                            list.Add(timekeeping);
                            LoadAutoRefreshInformation();
                            txtHoten.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng kiểm tra lại thông tin làm việc");
                        }
                    }
                    else
                    {
                        timekeeping = ObjectTimekeeping();
                        list.Add(timekeeping);
                        LoadAutoRefreshInformation();
                        txtHoten.Focus();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var timekeeping = list.FirstOrDefault(u => u.MSNV == txtMSNV.Text && u.Date == DateTime.Parse(txtNgay.Text + "/" + txtThang.Text + "/" + txtNam.Text));
            timekeeping.Date = DateTime.Parse(txtNgay.Text + "/" + txtThang.Text + "/" + txtNam.Text).Date;
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
                timekeeping.TotalWeight = 0;
            }
            else
            {
                timekeeping.Weight = int.Parse(txtWeight.Text);
                timekeeping.TotalWeight = (int.Parse(txtWeight.Text) * int.Parse(txtTypeWeight.Text));
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
            LoadAutoRefreshInformation();
            txtNgay.Enabled = true;
            txtThang.Enabled = true;
            txtNam.Enabled = true;
            btnRemove.Enabled = true;
            btnSave.Enabled = true;
            btnEdit.Visible = false;
            btnThem.Visible = true;
        }

        #endregion Button

        #region food

        private void txtFood_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFood_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNote.Focus();
            }
        }
        #endregion

        #region Note
        private void txtNote_KeyUp_1(object sender, KeyEventArgs e)
        {
            int i = 0;
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit.Focus();
                btnThem.Focus();
            }
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkRest.Checked)
            {
                txtThoigianBD.Enabled = false;
                txtThoigianKT.Enabled = false;
                txtWeight.Enabled = false;
                txtThoigianBD.Text = string.Empty;
                txtThoigianKT.Text =string.Empty ;
                txtWeight.Text = string.Empty;
                txtBonus.Enabled = false;
                txtPunish.Enabled = true;
            }
            if (!checkRest.Checked)
            {
                txtPunish.Enabled = false;
                txtBonus.Enabled = true;
                txtThoigianBD.Enabled = true;
                txtThoigianKT.Enabled = true;
                txtWeight.Enabled = true;
            }
        }

        private void txtPunish_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtCashAdvance.Focus();
            }
        }

        private void txtBonus_TextChanged(object sender, EventArgs e)
        {

        }
    }
}