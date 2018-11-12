using BUS.Business;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PlasticsFactory.UserControls.Main_Content.MCStatistic
{
    public partial class MCStatisticHome : UserControl
    {
        #region Generate Field

        private ProductInputBO productInputBO = new ProductInputBO();
        private ProductOutputBO productOutputBO = new ProductOutputBO();
        private TimekeepingBO timekeepingBO = new TimekeepingBO();
        private EmployeePaymentBO employeePaymentBO = new EmployeePaymentBO();
        private PreferceProductPriceBO preferceProductPriceBO = new PreferceProductPriceBO();
        private PaymentInputBO paymentInputBO = new PaymentInputBO();
        private PaymentOutputBO paymentOutputBO = new PaymentOutputBO();
        private int Month = 0;
        private int Year = 0;

        #endregion Generate Field

        #region Support

        private void loadMonth()
        {
            txtMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                txtMonth.Items.Add(i);
            }
            Month = DateTime.Now.Month;
            txtMonth.Text = Month.ToString();
        }

        private void loadYear()
        {

            Year = DateTime.Now.Year;
            int curYear = DateTime.Now.Year;
            txtYear.Items.Clear();
            for (int i = curYear - 10; i <= curYear; i++)
            {
                txtYear.Items.Add(i);
            }
            txtYear.Text = Year.ToString();
        }

        private void loadCharArea()
        {
            var chart = chartStatistic.ChartAreas[0];
            chartStatistic.Controls.Clear();
            chartStatistic.Titles.Add("NHẬP XUẤT HÀNG TRONG VÒNG 12 THÁNG");
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            //trục này là trục x tháng 1 - 12
            chart.AxisX.Minimum = 1;
            chart.AxisX.Maximum = 12;
            chart.AxisY.Minimum = 0;
        }

        private void loadChart()
        {
            chartStatistic.Series.Clear();
            
            //Duong input
            Series SRInput = new Series();
            SRInput.ChartType = SeriesChartType.Line;
            SRInput.Color = Color.Crimson;
            SRInput.Name = "Nhập hàng";
            SRInput.MarkerStyle = MarkerStyle.Circle;
            SRInput.MarkerSize = 6;
            SRInput.BorderWidth = 3;
            SRInput.MarkerBorderColor = Color.Black;
            //Xuat hang
            Series SROutput = new Series();
            SROutput.ChartType = SeriesChartType.Line;
            SROutput.Color = Color.SteelBlue;
            SROutput.Name = "Xuất hàng";
            SROutput.MarkerStyle = MarkerStyle.Circle;
            SROutput.MarkerSize = 6;
            SROutput.BorderWidth = 3;
            SROutput.MarkerBorderColor = Color.Black;
            var listDBinput = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == Year);
            var listDBoutput = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == Year);
            for (int i = 1; i <= 12; i++)
            {
                int productInputWeight = listDBinput.Where(u => u.Date.Value.Month == i).Sum(u => u.ProductWeight).Value;
                int productOutputWeight = listDBoutput.Where(u => u.Date.Value.Month == i).Sum(u => u.ProductWeight).Value;
                SRInput.Points.Add(new DataPoint(i, productInputWeight));
                SROutput.Points.Add(new DataPoint(i, productOutputWeight));
            }
            chartStatistic.Series.Add(SRInput);
            chartStatistic.Series.Add(SROutput);
        }

        private void loadAllInformation()
        {
            //Generate Field
            var listInput = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month && u.Date.Value.Year == Year,u=>u.PaymentInputs);
            var listOutput = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month && u.Date.Value.Year == Year,u=>u.PaymentOutputs);
            var listEmployeePayment = employeePaymentBO.GetData((u => u.isDelete == false && u.DATE.Value.Month == Month && u.DATE.Value.Year == Year));
            var listTimekeeping = timekeepingBO.GetData((u => u.isDelete==false));
            // Weight
            txtProductInputWeight.Text = listInput.Sum(u => u.ProductWeight).Value!=0? listInput.Sum(u =>u.ProductWeight).Value.ToString("#,###"):"0";
            txtProductOutputWeight.Text = listOutput.Sum(u => u.ProductWeight).Value!=0? listOutput.Sum(u => u.ProductWeight).Value.ToString("#,###"):"0";
            //Amount
            txtProductInputAmount.Text = listInput.Sum(u => u.TotalAmount)!=0? listInput.Sum(u => u.TotalAmount).ToString("#,###"):"0";
            txtProductOutputAmount.Text = listOutput.Sum(u => u.TotalAmount).Value!=0? listOutput.Sum(u => u.TotalAmount).Value.ToString("#,###"):"0";
            //Payed
            txtInputPay.Text = listInput.Sum(u => u.Payed)!=0? listInput.Sum(u => u.Payed).ToString("#,###"):"0";
            txtOutputPayed.Text = listOutput.Sum(u => u.Payed)!=0? listOutput.Sum(u => u.Payed).ToString("#,###"):"0";
            //Residue
            txtInputResidue.Text = (listInput.Sum(u => u.TotalAmount) - listInput.Sum(u => u.Payed))!=0? (listInput.Sum(u => u.TotalAmount) - listInput.Sum(u => u.Payed)).ToString("#,###"):"0";
            txtOutputResidue.Text = (listOutput.Sum(u => u.TotalAmount).Value - listOutput.Sum(u => u.Payed))!=0? (listOutput.Sum(u => u.TotalAmount).Value - listOutput.Sum(u => u.Payed)).ToString("#,###"):"0";
            //Wage
            txtWage.Text = listEmployeePayment.Sum(u => u.PAY)!=0? listEmployeePayment.Sum(u => u.PAY).ToString("#,###"):"0";
            //Profitable
            txtProfitable.Text = (listOutput.Sum(u => u.Payed) - listInput.Sum(u => u.Payed) - listEmployeePayment.Sum(u => u.PAY)) != 0 ? (listOutput.Sum(u => u.Payed) - listInput.Sum(u => u.Payed) - listEmployeePayment.Sum(u => u.PAY)).ToString("#,###") : "0";
            txtProfitableTotal.Text=(listOutput.Sum(u => u.TotalAmount) - listInput.Sum(u => u.TotalAmount) - listEmployeePayment.Sum(u => u.PAY)) != 0 ? (listOutput.Sum(u => u.Payed) - listInput.Sum(u => u.Payed) - listEmployeePayment.Sum(u => u.PAY)).ToString("#,###") : "0";
            //percent
            var listInputMonthAgo = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month - 1 && u.Date.Value.Year == Year);
            var listOutputMonthAgo = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == Month - 1 && u.Date.Value.Year == Year);
            if (Month==1)
            {
                listInputMonthAgo = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == 12 && u.Date.Value.Year == Year-1);
                listOutputMonthAgo = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Month == 12 && u.Date.Value.Year == Year-1);
            }
            int TotalInput = productInputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == Year).Sum(u => u.TotalWeight);
            int TotalOutput = productOutputBO.GetData(u => u.isDelete == false && u.Date.Value.Year == Year).Sum(u => u.TotalWeight);
            float InputPercent = ((float)((listInput.Sum(u => u.TotalWeight) * 100) / (float)TotalInput) - ((float)(listInputMonthAgo.Sum(u => u.TotalWeight) * 100) / (float)TotalInput));
            float OutputPercent = ((float)listOutput.Sum(u => u.TotalWeight) * 100 / (float)TotalOutput - (float)listOutputMonthAgo.Sum(u => u.TotalWeight) * 100 / (float)TotalOutput);
            if(InputPercent>=0)
            {
                txtInputPercent.Text = InputPercent.ToString("0.0") + "%";
                pictureInputPercent.Image = Properties.Resources.arrow_up_icon;
            }
            if(InputPercent<0)
            {
                txtInputPercent.Text = (InputPercent*(-1)).ToString("0.0") + "%";
                pictureInputPercent.Image = Properties.Resources.arrow_down_icon;
            }
            if (OutputPercent >= 0)
            {
                txtOutputPercent.Text = OutputPercent.ToString("0.0") + "%";
                pictureOutputPercent.Image = Properties.Resources.arrow_up_icon;
            }
            if (OutputPercent < 0)
            {
                txtOutputPercent.Text = (OutputPercent * (-1)).ToString("0.0") + "%";
                pictureOutputPercent.Image = Properties.Resources.arrow_down_icon;
            }
        }
        #endregion Support

        public MCStatisticHome()
        {
            InitializeComponent();
        }

        #region Pending Interface
        private void MCStatisticHome_Load(object sender, EventArgs e)
        {
            //
            loadCharArea();
            loadYear();
            loadMonth();
            loadChart();
            loadAllInformation();
        }

        private void panelInput_Click(object sender, EventArgs e)
        {
            if (panelOutput.Height <= 122)
            {
                if (panelInput.Height == 122)
                {
                    panelInput.Height = 200;
                    panelOutput.Location = new Point(panelOutput.Bounds.X, panelOutput.Bounds.Y + 80);
                    panelOutput.Height = 40;
                    pictureInput.Image = Properties.Resources.arrow_up;
                    lbOuptut.Location = new Point(lbOuptut.Bounds.X, lbOuptut.Bounds.Y - 9);
                    //AnimatorInput.ShowSync(panelInput);
                }
                else
                {
                    panelInput.Height = 122;
                    panelOutput.Height = 122;
                    pictureInput.Image = Properties.Resources.arrow_down;
                    panelOutput.Location = new Point(panelOutput.Bounds.X, panelOutput.Bounds.Y - 80);
                    lbOuptut.Location = new Point(lbOuptut.Bounds.X, lbOuptut.Bounds.Y + 9);
                    //AnimatorInput.ShowSync(panelInput);
                }
            }
        }

        private void pictureInput_Click(object sender, EventArgs e)
        {
            panelInput_Click(sender, e);
        }

        private void panelOutput_Click(object sender, EventArgs e)
        {
            if (panelInput.Height <= 122)
            {
                if (panelOutput.Height == 122)
                {
                    panelOutput.Height = 200;
                    panelEmployee.Location = new Point(panelEmployee.Bounds.X, panelEmployee.Bounds.Y + 80);
                    panelEmployee.Height = 40;
                    pictureOutput.Image = Properties.Resources.arrow_up;
                    lbEmployee.Location = new Point(lbEmployee.Bounds.X, lbEmployee.Bounds.Y - 9);
                    //AnimatorInput.ShowSync(panelOutput);
                }
                else
                {
                    panelOutput.Height = 122;
                    panelEmployee.Height = 122;
                    pictureOutput.Image = Properties.Resources.arrow_down;
                    panelEmployee.Location = new Point(panelEmployee.Bounds.X, panelEmployee.Bounds.Y - 80);
                    lbEmployee.Location = new Point(lbEmployee.Bounds.X, lbEmployee.Bounds.Y + 9);
                    //AnimatorInput.ShowSync(panelOutput);
                }
            }
        }

        private void pictureOutput_Click(object sender, EventArgs e)
        {
            panelOutput_Click(sender, e);
        }

        #endregion

        #region Date
        private void txtMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Month = int.Parse(txtMonth.Text);
            loadAllInformation();
        }

        private void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Year = int.Parse(txtYear.Text);
                loadChart();
                loadAllInformation();
            }
            catch
            {

            }
        }
        #endregion
    }
}