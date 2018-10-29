using BUS.Business;
using PlasticsFactory.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmEditCustomerPay : Form
    {
        #region Generate Field

        public PaymentInputBO paymentInputBO = new PaymentInputBO();
        private PaymentOutputBO paymentOutputBO = new PaymentOutputBO();

        #endregion Generate Field

        #region Support

        private int MaxPay()
        {
            int ID = int.Parse(txtMSTT.Text.Trim().Substring(2));
            int MSHD = int.Parse(txtMSHD.Text.Trim().Substring(2));
            string Type = txtMSHD.Text.Trim().Substring(0, 2);
            if (Type == "NH")
            {
                //Tiền đã trả trừ tiền đang update
                int pay = paymentInputBO.GetData(u => u.isDelete == false && u.MSDH == MSHD && u.ID != ID).Sum(u => u.Payment).Value;
                int amount = int.Parse(txtAmount.Text);
                return amount - pay;
            }
            else
            {
                int pay = paymentOutputBO.GetData(u => u.isDelete == false && u.MSDH == MSHD && u.ID != ID).Sum(u => u.Payment).Value;
                int amount = int.Parse(txtAmount.Text);
                return amount - pay;
            }
        }

        #endregion Support

        //Khai báo delegate
        public delegate void Rotate(string MSTT, string Date, string MSHD, string MSKH, string CustomerName, string ProductNames, int ProductWeigth, int Amount, int Payed);

        public Rotate Sender;

        public frmEditCustomerPay()
        {
            InitializeComponent();
            //Tạo con trỏ tới hàm GetMessage
            Sender = new Rotate(loadDataByCustomerPay);
        }

        private void loadDataByCustomerPay(string MSTT, string Date, string MSHD, string MSKH, string CustomerName, string ProductNames, int ProductWeigth, int Amount, int Payed)
        {
            txtMSTT.Text = MSTT;
            txtMSHD.Text = MSHD;
            txtDate.Text = Date;
            txtMSKH.Text = MSKH;
            txtName.Text = CustomerName;
            txtProductNames.Text = ProductNames;
            txtProductWeight.Text = ProductWeigth.ToString();
            txtAmount.Text = Amount.ToString();
            txtPayed.Text = Payed.ToString();
        }

        private void frmEditCustomerPay_Load(object sender, EventArgs e)
        {
            txtPayed.Focus();
        }

        private void txtPayed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Int64 currentPay = Int64.Parse(txtPayed.Text);
            if (txtPayed.Text != string.Empty && txtPayed.Text != "0" && currentPay <= MaxPay())
            {
                string Type = txtMSHD.Text.Trim().Substring(0, 2);
                if (Type == "NH")
                {
                    PaymentInputBO paymentInputBO = new PaymentInputBO();
                    PaymentInput payment = new PaymentInput();
                    payment.ID = int.Parse(txtMSTT.Text.Substring(2));
                    payment.Date = DateTime.Parse(txtDate.Text);
                    payment.MSDH = int.Parse(txtMSHD.Text.Trim().Substring(2));
                    payment.isDelete = false;
                    payment.Payment = int.Parse(txtPayed.Text);
                    paymentInputBO.Update(payment);
                }
                else
                {
                    PaymentOutputBO paymentOutputBO = new PaymentOutputBO();
                    PaymentOutput payment = new PaymentOutput();
                    payment.ID = int.Parse(txtMSTT.Text.Substring(2));
                    payment.Date = DateTime.Parse(txtDate.Text);
                    payment.MSDH = int.Parse(txtMSHD.Text.Trim().Substring(2));
                    payment.isDelete = false;
                    payment.Payment = int.Parse(txtPayed.Text);
                    paymentOutputBO.Update(payment);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Không hợp lệ .Vui lòng kiểm tra lại");
                txtPayed.Text = MaxPay().ToString();
            }
        }

        private void txtPayed_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit.Focus();
            }
        }
    }
}