namespace PlasticsFactory
{
    partial class frmInventation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventation));
            this.dataDS = new System.Windows.Forms.DataGridView();
            this.dgvType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSack = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSack = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataDS)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataDS
            // 
            this.dataDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataDS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvType,
            this.dgvWeight});
            this.dataDS.Location = new System.Drawing.Point(197, 81);
            this.dataDS.Name = "dataDS";
            this.dataDS.Size = new System.Drawing.Size(240, 183);
            this.dataDS.TabIndex = 0;
            this.dataDS.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataDS_CellClick);
            this.dataDS.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataDS_CellContentClick);
            // 
            // dgvType
            // 
            this.dgvType.HeaderText = "Loại";
            this.dgvType.Name = "dgvType";
            this.dgvType.Width = 95;
            // 
            // dgvWeight
            // 
            this.dgvWeight.HeaderText = "Trọng lượng";
            this.dgvWeight.Name = "dgvWeight";
            // 
            // txtSack
            // 
            this.txtSack.Location = new System.Drawing.Point(31, 127);
            this.txtSack.Name = "txtSack";
            this.txtSack.Size = new System.Drawing.Size(121, 20);
            this.txtSack.TabIndex = 1;
            this.txtSack.TextChanged += new System.EventHandler(this.txtSack_TextChanged);
            this.txtSack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSack_KeyPress);
            // 
            // txtType
            // 
            this.txtType.FormattingEnabled = true;
            this.txtType.Location = new System.Drawing.Point(31, 83);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(121, 21);
            this.txtType.TabIndex = 2;
            this.txtType.SelectedIndexChanged += new System.EventHandler(this.txtType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Loại";
            // 
            // lbSack
            // 
            this.lbSack.AutoSize = true;
            this.lbSack.Location = new System.Drawing.Point(28, 111);
            this.lbSack.Name = "lbSack";
            this.lbSack.Size = new System.Drawing.Size(64, 13);
            this.lbSack.TabIndex = 4;
            this.lbSack.Text = "Trọng lượng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "SẢN LƯỢNG TỒN";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Location = new System.Drawing.Point(197, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 29);
            this.panel1.TabIndex = 110;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(1261, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 99;
            this.pictureBox3.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(3, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "CHI TIẾT";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Green;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Location = new System.Drawing.Point(95, 153);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 23);
            this.btnAdd.TabIndex = 111;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEdit.Location = new System.Drawing.Point(94, 153);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(57, 23);
            this.btnEdit.TabIndex = 113;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Red;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRemove.Location = new System.Drawing.Point(95, 177);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(57, 23);
            this.btnRemove.TabIndex = 114;
            this.btnRemove.Text = "Xóa";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmInventation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 276);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbSack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtSack);
            this.Controls.Add(this.dataDS);
            this.Name = "frmInventation";
            this.Text = "frmInventation";
            this.Load += new System.EventHandler(this.frmInventation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataDS)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataDS;
        private System.Windows.Forms.TextBox txtSack;
        private System.Windows.Forms.ComboBox txtType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvWeight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
    }
}