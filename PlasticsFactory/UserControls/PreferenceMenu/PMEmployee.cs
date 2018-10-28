using PlasticsFactory.UserControls.Main_Content.MCEmployee;
using System;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.PreferenceMenu
{
    public partial class PMEmployee : UserControl
    {
        private MCEAdd mceAdd = new MCEAdd();
        private MCEmployeeManagement mceManage = new MCEmployeeManagement();
        private MCPayment mcPayment = new MCPayment();

        public PMEmployee()
        {
            InitializeComponent();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mceManage);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mceAdd);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcPayment);
        }
    }
}