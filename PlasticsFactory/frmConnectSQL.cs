using PlasticsFactory.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PlasticsFactory
{
    public partial class frmConnectSQL : Form
    {
        public frmConnectSQL()
        {
            InitializeComponent();
        }

        private void frmConnectSQL_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            AppSetting appSetting = new AppSetting();
            string connectString = @"metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string="+ HttpUtility.HtmlDecode(('"').ToString()) + ";data source="+txtServer.Text+";initial catalog=PlasticFactory;user id="+txtAccount.Text+";password="+txtPass.Text+";MultipleActiveResultSets=True;App=EntityFramework"+ HttpUtility.HtmlDecode(('"').ToString());
            appSetting.SaveConnectionString("PlasticFactoryEntities", connectString);
            MessageBox.Show("Hế thống sẽ thoát để cập nhật dữ liệu");
            this.Close();
        }
    }
}
