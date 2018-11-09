namespace PlasticsFactory
{
    partial class frmConnectSQL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.DATABASE = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(115, 72);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(322, 20);
            this.txtServer.TabIndex = 1;
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(115, 112);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(322, 20);
            this.txtAccount.TabIndex = 2;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(115, 157);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(322, 20);
            this.txtPass.TabIndex = 3;
            // 
            // DATABASE
            // 
            this.DATABASE.AutoSize = true;
            this.DATABASE.Location = new System.Drawing.Point(222, 24);
            this.DATABASE.Name = "DATABASE";
            this.DATABASE.Size = new System.Drawing.Size(64, 13);
            this.DATABASE.TabIndex = 4;
            this.DATABASE.Text = "DATABASE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tài khoản :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Server :";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(362, 199);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // frmConnectSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 308);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DATABASE);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtServer);
            this.Name = "frmConnectSQL";
            this.Text = "ConnectSQL";
            this.Load += new System.EventHandler(this.frmConnectSQL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label DATABASE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnect;
    }
}