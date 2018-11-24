using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlasticsFactory.UserControls.Main_Content.MCStatistic;

namespace PlasticsFactory.UserControls.PreferenceMenu
{
    public partial class PMStatistic : UserControl
    {
        public PMStatistic()
        {
            InitializeComponent();
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            MCConfiguration mcConfiguration = new MCConfiguration();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcConfiguration);
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            MCStatisticHome mcStatistic = new MCStatisticHome();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcStatistic);
        }

        private void btnQuantity_Click(object sender, EventArgs e)
        {
            MCQuantity mcQuantity = new MCQuantity();
            frmLayout.panelContents.Controls.Clear();
            frmLayout.panelContents.Controls.Add(mcQuantity);
        }
    }
}
