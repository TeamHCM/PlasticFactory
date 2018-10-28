using PlasticsFactory.UserControls.Main_Content.MCChamcong;
using PlasticsFactory.UserControls.Main_Content.MCCustomer;
using PlasticsFactory.UserControls.Main_Content.MCEmployee;
using PlasticsFactory.UserControls.Main_Content.MCProduct;
using PlasticsFactory.UserControls.PreferenceMenu;
using System;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmLayout : Form
    {
        #region Generate Field

        public static Panel panelContents = new Panel();
        public PMChamcong pmChamcong = new PMChamcong();
        public MCADDKhachhang mcADDBanhang = new MCADDKhachhang();
        public PMCustomer pmCustonmer = new PMCustomer();
        public ProductInput productInput = new ProductInput();
        public PMProduct pmProduct = new PMProduct();
        public MCEAdd mceAdd;
        public PMEmployee pmEployee;
        public MCAll mcAll = new MCAll();

        #endregion Generate Field

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
            mceAdd = new MCEAdd();
            pmEployee = new PMEmployee();
            panelContents.SetBounds(0, 97, 1364, 652);
            this.Controls.Add(panelContents);
            panelPreference.Controls.Add(pmEployee);
            panelContents.Controls.Add(mceAdd);
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
    }
}