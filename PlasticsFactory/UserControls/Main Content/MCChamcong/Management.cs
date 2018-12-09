using BUS.Business;
using PlasticsFactory.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.Main_Content.MCChamcong
{
    public partial class Management : UserControl
    {
        #region Generate Field

        private List<Timekeeping> list = new List<Timekeeping>();
        public DateTime Now = DateTime.Now.Date;
        public TimekeepingBO timekeepingBO = new TimekeepingBO();
        public static Timekeeping tempUpdate = new Timekeeping();
        public Timekeeping tempDelete = new Timekeeping();
        private EmployeeBO employeeBO = new EmployeeBO();
        public double totalCashAdvance = 0;
        public double totalWeight = 0;
        private string tempMSNV = "";

        public delegate void Transport(string EmployeeName, string MSNV, DateTime Date);

        #endregion Generate Field

        #region Support

        public void LoadDataGird(IEnumerable<Timekeeping> listData)
        {
            dataDS.Columns.Clear();
            #region DataGridView
            dataDS.ColumnCount = 16;
            dataDS.Columns[0].Name = "MSNV";
            dataDS.Columns[1].Name = "Họ tên";
            dataDS.Columns[2].Name = "Ngày";
            dataDS.Columns[3].Name = "Thời gian bắt đầu";
            dataDS.Columns[4].Name = "Thời gian kết thúc";
            dataDS.Columns[5].Name = "Tăng ca";
            dataDS.Columns[6].Name = "Thời gian(h)";
            dataDS.Columns[7].Name = "Số bao";
            dataDS.Columns[8].Name = "Loại bao(KG)";
            dataDS.Columns[9].Name = "Số lượng";
            dataDS.Columns[10].Name = "Tiền ứng";
            dataDS.Columns[11].Name = "Tiền cơm";
            dataDS.Columns[12].Name = "Tiền phạt";
            dataDS.Columns[13].Name = "Tiền thưởng";
            dataDS.Columns[14].Name = "Ghi chú";
            dataDS.Columns[15].Name = "Nghỉ";

            dataDS.Columns[0].Width = 80;
            dataDS.Columns[1].Width = 150;
            dataDS.Columns[2].Width = 110;
            dataDS.Columns[3].Width = 110;
            dataDS.Columns[4].Width = 110;
            dataDS.Columns[5].Width = 100;
            dataDS.Columns[6].Width = 100;
            dataDS.Columns[7].Width = 100;
            dataDS.Columns[8].Width = 100;
            dataDS.Columns[9].Width = 100;
            dataDS.Columns[10].Width =120;
            dataDS.Columns[11].Width = 100;
            dataDS.Columns[12].Width = 100;
            dataDS.Columns[13].Width = 100;
            dataDS.Columns[14].Width = 200;
            dataDS.Columns[15].Width = 100;
            #endregion
            txtCountWork.Visible = true;
            int i = 0;
            dataDS.Rows.Clear();
            totalCashAdvance = 0;
            totalWeight = 0;
            txtCountWork.Text = (listData.Count() - listData.Where(u => u.isRest == true).Count()).ToString("#,###");
            foreach (var item in listData)
            {
                dataDS.Rows.Add();
                dataDS.Rows[i].Cells[0].Value = item.MSNV;
                dataDS.Rows[i].Cells[1].Value = timekeepingBO.GetNameEmployee(item.MSNV);
                dataDS.Rows[i].Cells[2].Value = item.Date.Value.ToShortDateString();
                dataDS.Rows[i].Cells[3].Value = item.TimeStart;
                dataDS.Rows[i].Cells[4].Value = item.TimeEnd;
                dataDS.Rows[i].Cells[5].Value = String.Format("{0:0.0}", item.OverTime); ;
                dataDS.Rows[i].Cells[6].Value = String.Format("{0:0.0}", item.Time);
                dataDS.Rows[i].Cells[7].Value = item.Weight;
                dataDS.Rows[i].Cells[8].Value = item.Type;
                dataDS.Rows[i].Cells[9].Value = item.TotalWeight;
                dataDS.Rows[i].Cells[10].Value = item.AdvancePayment;
                dataDS.Rows[i].Cells[11].Value = item.Food;
                dataDS.Rows[i].Cells[12].Value = item.Punish;
                dataDS.Rows[i].Cells[13].Value = item.Bunus;
                dataDS.Rows[i].Cells[14].Value = item.Note;
                if (item.isRest == true)
                {
                    dataDS.Rows[i].Cells[15].Value = "Yes";
                }
                else
                {
                    dataDS.Rows[i].Cells[15].Value = "No";
                }
                totalCashAdvance += item.AdvancePayment;
                totalWeight += item.TotalWeight;
                i++;
            }
            lbCashAdvance.Text = totalCashAdvance == 0 ? "0" : string.Format("{0:0,0 (VNĐ)}", totalCashAdvance);
            lbTotalWeight.Text = totalWeight == 0 ? "0" : string.Format("{0:0,0 (KG)}", totalWeight);
            txtTotalTime.Text = listData.Sum(u => u.Time) == 0 ? "0" : listData.Sum(u => u.Time).ToString();
            txtFood.Text = listData.Sum(u => u.Food).ToString("#,###") == string.Empty ? "0" : listData.Sum(u => u.Food).ToString("#,###") + "VNĐ";
            txtPunish.Text = listData.Sum(u => u.Punish).ToString("#,###") == string.Empty ? "0" : listData.Sum(u => u.Punish).ToString("#,###") + "VNĐ";
            txtBonus.Text = listData.Sum(u => u.Bunus).ToString("#,###") == string.Empty ? "0" : listData.Sum(u => u.Bunus).ToString("#,###") + "VNĐ";
            if (totalCashAdvance > 0)
            {
                //txtWriteMoney.Text = "(" + UnitMoney(totalCashAdvance) + ")";
            }
        }

        public void LoadDataDS(DateTime date)
        {
            try
            {
                dataDS.Columns.Clear();
                #region DataGridView
                dataDS.ColumnCount = 12;
                dataDS.Columns[0].Name = "MSNV";
                dataDS.Columns[1].Name = "Họ tên";
                dataDS.Columns[2].Name = "Ngày";
                dataDS.Columns[3].Name = "Thời gian(h)";
                dataDS.Columns[4].Name = "Số bao";
                dataDS.Columns[5].Name = "Loại bao(KG)";
                dataDS.Columns[6].Name = "Số lượng";
                dataDS.Columns[7].Name = "Tiền ứng";
                dataDS.Columns[8].Name = "Tiền cơm";
                dataDS.Columns[9].Name = "Tiền phạt";
                dataDS.Columns[10].Name = "Tiền thưởng";
                dataDS.Columns[11].Name = "Ngày làm";

                dataDS.Columns[0].Width = 80;
                dataDS.Columns[1].Width = 150;
                dataDS.Columns[2].Width = 110;
                dataDS.Columns[3].Width = 100;
                dataDS.Columns[4].Width = 100;
                dataDS.Columns[5].Width = 100;
                dataDS.Columns[6].Width = 100;
                dataDS.Columns[7].Width = 120;
                dataDS.Columns[8].Width = 100;
                dataDS.Columns[9].Width = 100;
                dataDS.Columns[10].Width = 100;
                dataDS.Columns[11].Width = 100;
                #endregion
                var ListEmployee = employeeBO.GetData(u => u.isDelete == false);
                var listData = timekeepingBO.GetData(u => u.isDelete == false && u.Date.Value.Month == int.Parse(txtMonth.Text) && u.Date.Value.Year == int.Parse(txtYear.Text));
                txtCountWork.Visible = false;
                int i = 0;
                foreach (var item in ListEmployee)
                {
                    var ListDB = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == item.MSNV && u.Date.Value.Month == date.Month && u.Date.Value.Year == date.Year).ToList();
                    if (ListDB.Count != 0)
                    {
                        foreach (var itemType in ListDB.Select(u=>u.Type).Distinct())
                        {
                            dataDS.Rows.Add();
                            if (i % 2 == 0)
                            {
                                dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[6].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[7].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[9].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[10].Style.BackColor = System.Drawing.Color.Gainsboro;
                                dataDS.Rows[i].Cells[11].Style.BackColor = System.Drawing.Color.Gainsboro;
                            }
                            else
                            {
                                dataDS.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[5].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[6].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[7].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[9].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[10].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                                dataDS.Rows[i].Cells[11].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                            }
                            dataDS.Rows[i].Cells[0].Value = item.MSNV;
                            dataDS.Rows[i].Cells[1].Value = timekeepingBO.GetNameEmployee(item.MSNV);
                            dataDS.Rows[i].Cells[2].Value = ListDB.First().Date.Value.Month + "/" + ListDB.First().Date.Value.Year;
                            dataDS.Rows[i].Cells[3].Value = ListDB.Sum(u => u.Time);
                            dataDS.Rows[i].Cells[4].Value = ListDB.Where(u => u.Type == itemType).Sum(u => u.Weight) == 0 ? "0" : ListDB.Where(u => u.Type == itemType).Sum(u => u.Weight).ToString("#,###");
                            dataDS.Rows[i].Cells[5].Value = itemType;//Type sack
                            dataDS.Rows[i].Cells[6].Value = ListDB.Where(u=>u.Type==itemType).Sum(u => u.TotalWeight) == 0 ? "0" : ListDB.Where(u => u.Type == itemType).Sum(u => u.TotalWeight).ToString("#,###");
                            dataDS.Rows[i].Cells[7].Value = ListDB.Sum(u => u.AdvancePayment) == 0 ? "0" : ListDB.Sum(u => u.AdvancePayment).ToString("#,###");
                            dataDS.Rows[i].Cells[8].Value = ListDB.Sum(u => u.Food) == 0 ? "0" : ListDB.Sum(u => u.Food).ToString("#,###");
                            dataDS.Rows[i].Cells[9].Value = ListDB.Sum(u => u.Punish) == 0 ? "0" : ListDB.Sum(u => u.Punish).ToString("#,###");
                            dataDS.Rows[i].Cells[10].Value = ListDB.Sum(u => u.Bunus) == 0 ? "0" : ListDB.Sum(u => u.Bunus).ToString("#,###");
                            dataDS.Rows[i].Cells[11].Value = ListDB.Count() - ListDB.Count(u => u.isRest == true);
                            i++;
                        }
                    }
                }
                lbCashAdvance.Text = listData.Sum(u => u.AdvancePayment) == 0 ? "0" : string.Format("{0:0,0 (VNĐ)}", listData.Sum(u => u.AdvancePayment));
                lbTotalWeight.Text = listData.Sum(u => u.TotalWeight) == 0 ? "0" : string.Format("{0:0,0 (KG)}", listData.Sum(u => u.TotalWeight));
                txtTotalTime.Text = listData.Sum(u => u.Time) == 0 ? "0" : ((double)listData.Sum(u => u.Time)).ToString();
                txtFood.Text = listData.Sum(u => u.Food).ToString("#,###") == string.Empty ? "0" : ((double)listData.Sum(u => u.Food)).ToString("#,###") + "VNĐ";
                txtPunish.Text = listData.Sum(u => u.Punish).ToString("#,###") == string.Empty ? "0" : listData.Sum(u => u.Punish).ToString("#,###") + "VNĐ";
                txtBonus.Text = listData.Sum(u => u.Bunus).ToString("#,###") == string.Empty ? "0" : listData.Sum(u => u.Bunus).ToString("#,###") + "VNĐ";
            }
            catch { }
        }

        public void LoadDataDSByMSNV(string MSNV, DateTime date)
        {
            if (MSNV.Equals(""))
            {
                LoadDataDS(date);
            }
            else
            {
                var listDB = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == MSNV && u.Date.Value.Month == date.Month && u.Date.Value.Year == date.Year);

                LoadDataGird(listDB);
            }
        }

        public void LoadDataDSByDay(DateTime date)
        {
            var listDB = timekeepingBO.GetData(u => u.isDelete == false && u.Date.Value.Day == date.Day && u.Date.Value.Month == date.Month && u.Date.Value.Year == date.Year);
            LoadDataGird(listDB);
        }

        public void LoadDataDSByDayMonthYearMSNV(string MSNV, DateTime date)
        {
            var listDB = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == MSNV && u.Date.Value.Day == date.Day && u.Date.Value.Month == date.Month && u.Date.Value.Year == date.Year);
            LoadDataGird(listDB);
        }

        public void LoadDataDSByMonthNotMSNVAndDay(DateTime date)
        {
            var listDB = timekeepingBO.GetData(u => u.isDelete == false && u.Date.Value.Month == date.Month && u.Date.Value.Year == date.Year);
            LoadDataGird(listDB);
        }

        public void loadDay(int month, int year)
        {
            try
            {
                int day = DateTime.DaysInMonth(year, month);
                int maxDay = DateTime.DaysInMonth(year, month);
                int txtDays = int.Parse(txtDay.Text);
                txtDay.Items.Clear();
                txtDay.Items.Add("");
                for (int i = 1; i <= day; i++)
                {
                    txtDay.Items.Add(i);
                }
                if (maxDay < txtDays)
                {
                    txtDay.Text = "";
                }
            }
            catch { }
        }

        public void loadMonth()
        {
            txtMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                txtMonth.Items.Add(i);
            }
        }

        public void loadYear()
        {
            int Year = DateTime.Now.Year;
            for (int i = Year - 10; i <= Year; i++)
            {
                txtYear.Items.Add(i);
            }
        }

        public void loadDayMonthYear()
        {
            txtDay.Text = DateTime.Now.Day.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            loadDay(month, year);
            loadMonth();
            loadYear();
        }

        public void LoadNameEmployeeName()
        {
            var listName = timekeepingBO.GetNameEmployee();
            foreach (var item in listName)
            {
                txtEmployee.Items.Add(item);
            }
            txtEmployee.Items.Add("All");
        }

        public string ConvertNumberToString(int number)
        {
            string[] str1 = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] str2 = { "không", "một", "hai", "ba", "bốn", "lăm", "sáu", "bảy", "tám", "chín" };
            string strNum = number.ToString();
            if (number < 10)
            {
                return str1[number];
            }
            if (number == 10)
            {
                return "mười";
            }
            if (number > 10 && number < 20)
            {
                return "mười " + str2[int.Parse(strNum.Substring(1, 1))];
            }
            if (number >= 20)
            {
                int num1 = int.Parse(strNum.Substring(0, 1));
                int num2 = int.Parse(strNum.Substring(1, 1));
                return str1[num1] + " mươi " + str2[num2];
            }
            return "";
        }

        public static string VietHoa(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }
            }
            return result.Trim();
        }

        public string UnitMoney(double money)
        {
            string[] strUnit = { " đồng ", "", " trăm ", " nghìn ", "", " trăm ", " triệu ", "", " trăm ", " tỷ ", "", " trăm " };
            string result = "";
            string strMoney = money.ToString();
            int length = money.ToString().Length;
            int tempLength = length - 1;
            for (int i = 0; i < length; i++)
            {
                result = result + strMoney.Substring(i, 1) + strUnit[tempLength];
                tempLength--;
            }
            string tempRs = "";
            //filter 1
            for (int i = 0; i < result.Length; i++)
            {
                if (result.Substring(i, 1) == "0" && result.Substring(i - 2, 1) == "m")
                {
                    tempRs = tempRs + "linh ";
                }
                else
                {
                    tempRs = tempRs + result.Substring(i, 1);
                }
            }
            //filter 2
            result = "";
            string[] arrRs = tempRs.Split(' ');
            for (int i = 0; i < arrRs.Length; i++)
            {
                int number;
                bool isNumber = int.TryParse(arrRs[i], out number);
                if (isNumber)
                {
                    arrRs[i] = ConvertNumberToString(number);
                }
                if (arrRs[i] == "linh" && arrRs[i + 1] == "0")
                {
                    i++;
                    i++;
                }
                if (i == 0)
                {
                    arrRs[i] = VietHoa(arrRs[i]);
                }
                result += arrRs[i] + " ";
            }
            return result;
        }

        public void LoadAutoRefreshInformation()
        {
            loadListTimekeeping();
            tempMSNV = "";
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
                dataDS.Rows[i].Cells[10].Value = item.Note;
                i++;
            }
        }

        public void loadRefreshUpdateRemove()
        {
            if (txtMSNV.Text.Equals(""))
            {
                if (txtDay.Text.Equals(""))
                {
                    string strDate = "1" + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByMSNV(txtMSNV.Text, date);
                }
                else
                {
                    string strDate = txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByDayMonthYearMSNV(txtMSNV.Text, date);
                }
            }
            else
            {
                DateTime date = DateTime.Parse("1" + "/" + txtMonth.Text + "/" + txtYear.Text);
                LoadDataDSByMSNV(txtMSNV.Text, date);
            }
        }

        #endregion Support

        public Transport Sender;

        public Management()
        {
            InitializeComponent();
            txtEmployee.Text = "All";
            lbCashAdvance.Text = "";
            lbTotalWeight.Text = "";
            //txtWriteMoney.Text = "";
            LoadDataDS(Now);
            LoadNameEmployeeName();
            loadDayMonthYear();
            txtDay.Text = "";
            Sender = loadTransport;
        }

        public void loadTransport(string EmployeeName, string MSNV, DateTime Date)
        {
            txtMSNV.Text = MSNV;
            txtEmployee.Text = EmployeeName;
            txtMonth.Text = Date.Month.ToString();
            txtYear.Text = Date.Year.ToString();
            LoadDataDSByMSNV(MSNV, Date);
        }

        public void Management_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Parse(txtMonth.Text + "/" + txtYear.Text);
                LoadDataDS(date);
                btnPrint.Enabled = false;
            }
            catch { }
        }

        #region Event Search

        private void txtEmployee_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void txtDay_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDay.Text != string.Empty)
                {
                    string strDate = txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    if (txtMSNV.Text != string.Empty)
                    {
                        LoadDataDSByDayMonthYearMSNV(txtMSNV.Text, date);
                    }
                }
                else
                {
                    string strDate = txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    if (txtMSNV.Text != string.Empty)
                    {
                        LoadDataDSByMSNV(txtMSNV.Text, date);
                    }
                }
            }
            catch { }
        }

        private void txtDay_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int numDay = int.Parse(txtDay.Text);
                int numYear = int.Parse(txtYear.Text);
                int numMonth = int.Parse(txtMonth.Text);
                int maxDay = DateTime.DaysInMonth(numYear, numMonth);
                if (e.KeyCode == Keys.Enter)
                {
                    if (numDay > maxDay)
                    {
                        txtDay.Text = "";
                    }
                    txtMonth.Focus();
                }
            }
            catch { }
        }

        private void txtDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int day = int.Parse(txtDay.Text);
                int txtmonth = int.Parse(txtMonth.Text);
                int txtyear = int.Parse(txtYear.Text);
                int maxDay = DateTime.DaysInMonth(txtyear, 2);
                IEnumerable<int> month = TimekeepingBO.Month(day);
                txtMonth.Items.Clear();
                txtMonth.Text = txtMonth.Text;
                foreach (var item in month)
                {
                    txtMonth.Items.Add(item);
                }
                if (maxDay < int.Parse(txtDay.Text))
                {
                    txtMonth.Items.Remove(2);
                }
            }
            catch { }
        }

        private void txtMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int month = int.Parse(txtMonth.Text);
                int year = int.Parse(txtYear.Text);
                loadDay(month, year);
            }
            catch
            {
            }

            if (txtMSNV.Text.Equals(""))
            {
                if (txtDay.Text.Equals(""))
                {
                    string strDate = "1" + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDS(date);
                }
                else
                {
                    string strDate = txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByDay(date);
                }
            }
            else
            {
                if (txtDay.Text.Equals(""))
                {
                    string strDate = "1" + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByMSNV(txtMSNV.Text, date);
                }
                else
                {
                    string strDate = txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByDayMonthYearMSNV(txtMSNV.Text, date);
                }
            }
        }

        private void txtMonth_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int month = int.Parse(txtMonth.Text);
                int year = int.Parse(txtYear.Text);
                if (e.KeyCode == Keys.Enter)
                {
                    if (month > 12 || month <= 0)
                    {
                        txtMonth.Text = "";
                    }
                    else
                    {
                        loadDay(month, year);
                        txtYear.Focus();
                    }
                }
                if (e.KeyCode == Keys.Space)
                {
                    if (month > 12 || month <= 0)
                    {
                        txtMonth.Text = "";
                    }
                }
            }
            catch { }
        }

        private void txtMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtYear_SelectedValueChanged(object sender, EventArgs e)
        {
            int month = int.Parse(txtMonth.Text);
            int year = int.Parse(txtYear.Text);
            loadDay(month, year);
            if (txtMSNV.Text.Equals(""))
            {
                if (txtDay.Text.Equals(""))
                {
                    string strDate = "1" + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByMonthNotMSNVAndDay(date);
                }
                else
                {
                    string strDate = txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByDay(date);
                }
            }
            else
            {
                if (txtDay.Text.Equals(""))
                {
                    string strDate = "1" + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByMSNV(txtMSNV.Text, date);
                }
                else
                {
                    string strDate = txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text;
                    DateTime date = DateTime.Parse(strDate);
                    LoadDataDSByDayMonthYearMSNV(txtMSNV.Text, date);
                }
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMSNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion Event Search

        #region Event Update,Remove

        private void dataDS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != dataDS.RowCount - 1)
            //{
            //    string msnv = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
            //    DateTime date = DateTime.Parse(dataDS.Rows[e.RowIndex].Cells[2].Value.ToString());
            //    tempUpdate.Id = timekeepingBO.GetIdByMSNVDate(msnv, date);
            //    tempUpdate.Date = date;
            //    tempUpdate.MSNV = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
            //    tempUpdate.TimeStart = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
            //    tempUpdate.TimeEnd = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString();
            //    tempUpdate.Weight = int.Parse(dataDS.Rows[e.RowIndex].Cells[6].Value.ToString());
            //    tempUpdate.Type = int.Parse(dataDS.Rows[e.RowIndex].Cells[7].Value.ToString());
            //    tempUpdate.AdvancePayment = int.Parse(dataDS.Rows[e.RowIndex].Cells[9].Value.ToString());
            //    tempUpdate.Note = dataDS.Rows[e.RowIndex].Cells[10].Value.ToString();
            //    frmEdit frmEdit = new frmEdit();
            //    frmEdit.ShowDialog();
            //    loadRefreshUpdateRemove();
            //}
        }

        //private void btnRemove_Click(object sender, EventArgs e)
        //{
        //    if (tempMSNV != "")
        //    {
        //        string masseage = "Bạn có muốn xóa chấm công nhân viên " + tempMSNV.Trim() + "không ?";
        //        string Title = "Chú ý";
        //        DialogResult result = MessageBox.Show(masseage, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        //        if (result == DialogResult.Yes)
        //        {
        //            timekeepingBO.Update(tempDelete);
        //            loadRefreshUpdateRemove();
        //        }
        //    }
        //}

        private void dataDS_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tempMSNV = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                DateTime date = DateTime.Parse(dataDS.Rows[e.RowIndex].Cells[2].Value.ToString());
                tempDelete.Id = timekeepingBO.GetIdByMSNVDate(tempMSNV, date);
                tempDelete.MSNV = dataDS.Rows[e.RowIndex].Cells[0].Value.ToString();
                tempDelete.Date = DateTime.Parse(dataDS.Rows[e.RowIndex].Cells[2].Value.ToString()).Date;
                tempDelete.TimeStart = dataDS.Rows[e.RowIndex].Cells[3].Value.ToString();
                tempDelete.TimeEnd = dataDS.Rows[e.RowIndex].Cells[4].Value.ToString();
                tempDelete.Time = float.Parse(dataDS.Rows[e.RowIndex].Cells[5].Value.ToString());
                tempDelete.Weight = int.Parse(dataDS.Rows[e.RowIndex].Cells[6].Value.ToString());
                tempDelete.Type = int.Parse(dataDS.Rows[e.RowIndex].Cells[7].Value.ToString());
                tempDelete.TotalWeight = int.Parse(dataDS.Rows[e.RowIndex].Cells[8].Value.ToString());
                tempDelete.AdvancePayment = int.Parse(dataDS.Rows[e.RowIndex].Cells[9].Value.ToString());
                tempDelete.Note = dataDS.Rows[e.RowIndex].Cells[10].Value.ToString();
                tempDelete.isDelete = true;
            }
            catch
            {
                //Khi dòng cuối thì gán để không bị lỗi
                tempMSNV = "";
            }
        }

        #endregion Event Update,Remove

        private void txtEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var listMSNV = timekeepingBO.GetIdByName(txtEmployee.Text);
                if (listMSNV.Count != 0)
                {
                    txtMSNV.Items.Clear();
                    foreach (var item in listMSNV)
                    {
                        txtMSNV.Items.Add(item);
                    }
                    txtMSNV.Text = listMSNV.First();
                }
                else
                {
                    txtMSNV.Text = "";
                    txtMSNV.Items.Clear();
                }
                DateTime date = DateTime.Parse("1" + "/" + txtMonth.Text + "/" + txtYear.Text);
                LoadDataDSByMSNV(txtMSNV.Text, date);
                txtDay.SelectedItem = "";
                if (txtEmployee.Text == "All")
                {
                    btnPrint.Enabled = false;
                }
                else
                {
                    btnPrint.Enabled = true;
                }
            }
            catch
            {
            }
        }

        private void txtEmployee_TextChanged(object sender, EventArgs e)
        {
            txtEmployee_SelectedIndexChanged(sender, e);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtEmployee.Text != "All")
            {
                int year = int.Parse(txtYear.Text);
                int month = int.Parse(txtMonth.Text);
                string msnv = txtMSNV.Text;
                List<pTimekeeping> listDB = new List<pTimekeeping>();
                var listTimekeep = timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == msnv && u.Date.Value.Month == month && u.Date.Value.Year == year);
                foreach (var item in listTimekeep)
                {
                    pTimekeeping ptime = new pTimekeeping();
                    ptime.Id = item.Id;
                    ptime.MSNV = item.MSNV;
                    ptime.TimeEnd = item.TimeEnd != string.Empty ? item.TimeEnd : "X";
                    ptime.TimeStart = item.TimeStart != string.Empty ? item.TimeStart : "X";
                    ptime.Overtime = item.OverTime;
                    ptime.Time = item.Time;
                    ptime.Weight = item.Weight;
                    ptime.Type = item.Type;
                    ptime.TotalWeight = item.TotalWeight;
                    ptime.Food = item.Food;
                    ptime.Bunus = item.Bunus;
                    ptime.Punish = item.Punish;
                    ptime.Date = item.Date.Value;
                    ptime.Note = item.Note;
                    ptime.AdvancePayment = item.AdvancePayment;
                    if (item.isRest == true)
                    {
                        ptime.isRest = "Yes";
                    }
                    else
                    {
                        ptime.isRest = "No";
                    }
                    listDB.Add(ptime);
                }
                using (frmPrintTimekeeping frmprintTimekeeping = new frmPrintTimekeeping(listDB, msnv))
                {
                    frmprintTimekeeping.ShowDialog();
                }
            }
        }

        private void lbCashAdvance_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lbCashAdvance.Text != "0")
            {
                if (txtEmployee.Text != "All")
                {
                    IEnumerable<Timekeeping> listDB = timekeepingBO
                        .GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text
                        && u.Date.Value.Month == int.Parse(txtMonth.Text) && u.Date.Value.Year == int.Parse(txtYear.Text) && u.AdvancePayment != 0);
                    LoadDataGird(listDB);
                }
            }
        }

        private void txtMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}