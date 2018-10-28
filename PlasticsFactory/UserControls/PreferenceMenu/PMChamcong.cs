using PlasticsFactory.UserControls.Main_Content.MCChamcong;
using System;
using System.Windows.Forms;

namespace PlasticsFactory.UserControls.PreferenceMenu
{
    public partial class PMChamcong : UserControl
    {
        public PMChamcong()
        {
            InitializeComponent();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            Management management = new Management();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(management);
        }

        private void btnTimekeeping_Click(object sender, EventArgs e)
        {
            MCAll mcAll = new MCAll();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcAll);
        }
    }
}