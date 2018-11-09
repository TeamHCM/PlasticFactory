using PlasticsFactory.UserControls.Main_Content.MCChamcong;
using PlasticsFactory.UserControls.Main_Content.MCCustomer;
using PlasticsFactory.UserControls.Main_Content.MCEmployee;
using PlasticsFactory.UserControls.Main_Content.MCProduct;
using PlasticsFactory.UserControls.Main_Content.MCStatistic;
using PlasticsFactory.UserControls.PreferenceMenu;
using System;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;
using System.Xml;
namespace PlasticsFactory
{
    public partial class frmLayout : Form
    {
        #region Generate Field
        public static Panel panelContents = new Panel();
        public static Panel panelPreference = new Panel();
        public PMStatistic pmStatistic = new PMStatistic();
        public MCStatisticHome mcStatisticHome = new MCStatisticHome();
        public PMChamcong pmChamcong = new PMChamcong();
        public MCADDKhachhang mcADDBanhang = new MCADDKhachhang();
        public PMCustomer pmCustonmer = new PMCustomer();
        public ProductInput productInput = new ProductInput();
        public PMProduct pmProduct = new PMProduct();
        public MCEAdd mceAdd;
        public PMEmployee pmEployee;
        public MCAll mcAll = new MCAll();
        #endregion Generate Field

        #region Support
        #endregion
        //public PMChamcong pmChamcong;

        public frmLayout()
        {
            InitializeComponent();
        }

        private void toolEmployee_Click(object sender, EventArgs e)
        {
            panelPreference.Controls.Clear();
            panelContents.Controls.Clear();
            mceAdd = new MCEAdd();
            pmEployee = new PMEmployee();
            panelPreference.Controls.Add(pmEployee);
            panelContents.Controls.Add(mceAdd);
        }

        private void frmLayout_Load(object sender, EventArgs e)
        {
            try
            {
                mceAdd = new MCEAdd();
                pmEployee = new PMEmployee();
                panelPreference.Anchor = AnchorStyles.Top;
                //panelPreference.Dock = DockStyle.Top;
                panelPreference.SetBounds(0, 24, 1364, 73);
                panelContents.SetBounds(0, 97, 1364, 652);
                this.Controls.Add(panelContents);
                this.Controls.Add(panelPreference);
                panelPreference.Controls.Add(pmEployee);
                panelContents.Controls.Add(mceAdd);
            }
            catch
            {
                frmConnectSQL frm = new frmConnectSQL();
                frm.ShowDialog();
                this.Close();
            }
        }

        private void chấmCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPreference.Controls.Clear();
            panelContents.Controls.Clear();
            mcAll = mcAll ?? new MCAll();
            pmChamcong = pmChamcong ?? new PMChamcong();
            panelPreference.Controls.Add(pmChamcong);
            panelContents.Controls.Add(mcAll);
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPreference.Controls.Clear();
            panelContents.Controls.Clear();
            mcADDBanhang = mcADDBanhang ?? new MCADDKhachhang();
            pmCustonmer = pmCustonmer ?? new PMCustomer();
            panelPreference.Controls.Add(pmCustonmer);
            panelContents.Controls.Add(mcADDBanhang);
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPreference.Controls.Clear();
            panelContents.Controls.Clear();
            pmProduct = pmProduct ?? new PMProduct();
            productInput = productInput ?? new ProductInput();
            panelPreference.Controls.Add(pmProduct);
            panelContents.Controls.Add(productInput);
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPreference.Controls.Clear();
            panelContents.Controls.Clear();
            pmStatistic = new PMStatistic();
            mcStatisticHome = new MCStatisticHome();
            panelPreference.Controls.Add(pmStatistic);
            panelContents.Controls.Add(mcStatisticHome);
        }
    }
}