namespace PlasticsFactory.UserControls.PreferenceMenu
{
    partial class PMProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMProduct));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnManagement = new System.Windows.Forms.Button();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnInput = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.Controls.Add(this.btnManagement);
            this.panel1.Controls.Add(this.btnOutput);
            this.panel1.Controls.Add(this.btnInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 73);
            this.panel1.TabIndex = 5;
            // 
            // btnManagement
            // 
            this.btnManagement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManagement.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnManagement.FlatAppearance.BorderSize = 0;
            this.btnManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagement.ForeColor = System.Drawing.SystemColors.Control;
            this.btnManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnManagement.Image")));
            this.btnManagement.Location = new System.Drawing.Point(166, 4);
            this.btnManagement.Name = "btnManagement";
            this.btnManagement.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnManagement.Size = new System.Drawing.Size(75, 67);
            this.btnManagement.TabIndex = 4;
            this.btnManagement.Text = "Quản lý";
            this.btnManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnManagement.UseVisualStyleBackColor = true;
            this.btnManagement.Click += new System.EventHandler(this.btnManagement_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOutput.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOutput.FlatAppearance.BorderSize = 0;
            this.btnOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutput.ForeColor = System.Drawing.SystemColors.Control;
            this.btnOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnOutput.Image")));
            this.btnOutput.Location = new System.Drawing.Point(85, 3);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOutput.Size = new System.Drawing.Size(75, 67);
            this.btnOutput.TabIndex = 3;
            this.btnOutput.Text = "Xuất hàng";
            this.btnOutput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnInput
            // 
            this.btnInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnInput.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnInput.FlatAppearance.BorderSize = 0;
            this.btnInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInput.ForeColor = System.Drawing.SystemColors.Control;
            this.btnInput.Image = ((System.Drawing.Image)(resources.GetObject("btnInput.Image")));
            this.btnInput.Location = new System.Drawing.Point(4, 3);
            this.btnInput.Name = "btnInput";
            this.btnInput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInput.Size = new System.Drawing.Size(75, 67);
            this.btnInput.TabIndex = 2;
            this.btnInput.Text = "Nhập hàng";
            this.btnInput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // PMProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PMProduct";
            this.Size = new System.Drawing.Size(1364, 73);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnManagement;
    }
}
