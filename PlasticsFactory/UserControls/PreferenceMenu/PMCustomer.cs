using PlasticsFactory.UserControls.Main_Content.MCCustomer;
using System;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.PreferenceMenu
{
    public partial class PMCustomer : UserControl
    {
        public MCADDKhachhang mcADDBanhang;
        public MCThanhtoan mcThanhtoan;
        public MCCustomerMG mcCustomerMG;

        public PMCustomer()
        {
            InitializeComponent();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            mcThanhtoan = new MCThanhtoan();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcThanhtoan);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            mcADDBanhang = new MCADDKhachhang();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcADDBanhang);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            mcCustomerMG = new MCCustomerMG();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcCustomerMG);
        }
    }
}