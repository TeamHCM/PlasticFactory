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

namespace PlasticsFactory.UserControls.Main_Content.MCEmployee
{
    public partial class MCPayment : UserControl
    {
        #region Generate Field
        private EmployeeBO employeeBO = new EmployeeBO();
        private TimekeepingBO timekeepingBO = new TimekeepingBO();
        #endregion

        #region Support
        private void loadMSNV()
        {
            txtMSNV.Items.Clear();
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            foreach(var item in listDB)
            {
                txtMSNV.Items.Add(item.MSNV);
            }
        }
        private void loadCustomerName()
        {
            txtEmployeeName.Items.Clear();
            var listDB = employeeBO.GetData(u => u.isDelete == false);
            foreach(var item in listDB)
            {
                txtEmployeeName.Items.Add(item.Hoten);
            }
        }
        private void loadDetailWork()
        {
            if(txtMSNV.Text!=string.Empty)
            {
                int i = 0;
                dataDetailWork.Rows.Clear();
                var listDB=timekeepingBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text && u.Date.Value.Month == int.Parse(txtMonth.Text) && u.Date.Value.Year == int.Parse(txtYear.Text));
                foreach(var item in listDB)
                {
                    dataDetailWork.Rows.Add();
                    dataDetailWork.Rows[i].Cells[0].Value = item.Date.Value.Day + "/" + item.Date.Value.Month;
                    dataDetailWork.Rows[i].Cells[1].Value = item.TimeStart +" - "+ item.TimeEnd;
                    dataDetailWork.Rows[i].Cells[2].Value = item.Time;
                    dataDetailWork.Rows[i].Cells[3].Value = item.TotalWeight;
                    dataDetailWork.Rows[i].Cells[4].Value = item.AdvancePayment;
                    dataDetailWork.Rows[i].Cells[5].Value = item.Note;
                    i++;
                }
            }
        }
        #endregion

        public MCPayment()
        {
            InitializeComponent();
        }

        private void MCPayment_Load(object sender, EventArgs e)
        {
            loadMSNV();
            loadCustomerName();
            txtProduct.Text = "0";
            txtTime.Text = "0";
            txtPay.Text = "0";
            txtCash.Text = "0";
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
        }

        private void txtMSNV_SelectedValueChanged(object sender, EventArgs e)
        {
            txtEmployeeName.Text = employeeBO.GetData(u => u.isDelete == false && u.MSNV == txtMSNV.Text).First().Hoten;
            loadDetailWork();
        }

        private void txtEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMSNV.Text = employeeBO.GetData(u => u.isDelete == false && u.Hoten.Trim() == txtEmployeeName.Text.Trim()).First().MSNV;
            loadDetailWork();
        }
    }
}
