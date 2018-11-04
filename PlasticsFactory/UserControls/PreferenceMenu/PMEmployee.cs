using PlasticsFactory.UserControls.Main_Content.MCEmployee;
using System;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.PreferenceMenu
{
    public partial class PMEmployee : UserControl
    {
        private MCEAdd mceAdd ;
        private MCEmployeeManagement mceManage;
        private MCPaymentEmployee mcPayment ;

        public PMEmployee()
        {
            InitializeComponent();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            mceManage = new MCEmployeeManagement();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mceManage);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mceAdd = new MCEAdd();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mceAdd);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            mcPayment = new MCPaymentEmployee();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcPayment);
        }
    }
}