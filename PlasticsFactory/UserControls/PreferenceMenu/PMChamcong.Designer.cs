namespace PlasticsFactory.UserControls.PreferenceMenu
{
    partial class PMChamcong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMChamcong));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnManage = new System.Windows.Forms.Button();
            this.btnTimekeeping = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnManage);
            this.panel1.Controls.Add(this.btnTimekeeping);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.LightCyan;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 73);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(165, 3);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(75, 67);
            this.button1.TabIndex = 6;
            this.button1.Text = "Theo bao";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnManage
            // 
            this.btnManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManage.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnManage.FlatAppearance.BorderSize = 0;
            this.btnManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManage.ForeColor = System.Drawing.SystemColors.Control;
            this.btnManage.Image = ((System.Drawing.Image)(resources.GetObject("btnManage.Image")));
            this.btnManage.Location = new System.Drawing.Point(84, 3);
            this.btnManage.Name = "btnManage";
            this.btnManage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnManage.Size = new System.Drawing.Size(75, 67);
            this.btnManage.TabIndex = 5;
            this.btnManage.Text = "Quản lý";
            this.btnManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnTimekeeping
            // 
            this.btnTimekeeping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTimekeeping.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTimekeeping.FlatAppearance.BorderSize = 0;
            this.btnTimekeeping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimekeeping.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimekeeping.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTimekeeping.Image = ((System.Drawing.Image)(resources.GetObject("btnTimekeeping.Image")));
            this.btnTimekeeping.Location = new System.Drawing.Point(3, 3);
            this.btnTimekeeping.Name = "btnTimekeeping";
            this.btnTimekeeping.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTimekeeping.Size = new System.Drawing.Size(75, 67);
            this.btnTimekeeping.TabIndex = 4;
            this.btnTimekeeping.Text = "Chấm công";
            this.btnTimekeeping.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTimekeeping.UseVisualStyleBackColor = true;
            this.btnTimekeeping.Click += new System.EventHandler(this.btnTimekeeping_Click);
            // 
            // PMChamcong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PMChamcong";
            this.Size = new System.Drawing.Size(1364, 73);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Button btnTimekeeping;
        private System.Windows.Forms.Button button1;
    }
}
