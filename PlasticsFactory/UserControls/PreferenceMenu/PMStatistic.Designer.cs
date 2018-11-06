namespace PlasticsFactory.UserControls.PreferenceMenu
{
    partial class PMStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMStatistic));
            this.btnStatistic = new System.Windows.Forms.Button();
            this.btnConfiguration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStatistic
            // 
            this.btnStatistic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStatistic.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnStatistic.FlatAppearance.BorderSize = 0;
            this.btnStatistic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatistic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatistic.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStatistic.Image = ((System.Drawing.Image)(resources.GetObject("btnStatistic.Image")));
            this.btnStatistic.Location = new System.Drawing.Point(12, 3);
            this.btnStatistic.Name = "btnStatistic";
            this.btnStatistic.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnStatistic.Size = new System.Drawing.Size(75, 67);
            this.btnStatistic.TabIndex = 5;
            this.btnStatistic.Text = "Doanh thu";
            this.btnStatistic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStatistic.UseVisualStyleBackColor = true;
            this.btnStatistic.Click += new System.EventHandler(this.btnStatistic_Click);
            // 
            // btnConfiguration
            // 
            this.btnConfiguration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConfiguration.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnConfiguration.FlatAppearance.BorderSize = 0;
            this.btnConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguration.ForeColor = System.Drawing.SystemColors.Control;
            this.btnConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("btnConfiguration.Image")));
            this.btnConfiguration.Location = new System.Drawing.Point(93, 4);
            this.btnConfiguration.Name = "btnConfiguration";
            this.btnConfiguration.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnConfiguration.Size = new System.Drawing.Size(75, 67);
            this.btnConfiguration.TabIndex = 6;
            this.btnConfiguration.Text = "Thông số";
            this.btnConfiguration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConfiguration.UseVisualStyleBackColor = true;
            this.btnConfiguration.Click += new System.EventHandler(this.btnConfiguration_Click);
            // 
            // PMStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.Controls.Add(this.btnConfiguration);
            this.Controls.Add(this.btnStatistic);
            this.Name = "PMStatistic";
            this.Size = new System.Drawing.Size(1364, 73);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStatistic;
        private System.Windows.Forms.Button btnConfiguration;
    }
}
