namespace PlasticsFactory.UserControls.Main_Content.MCStatistic
{
    partial class MCQuantity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MCQuantity));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dataDS = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.RichTextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTypeOuput = new System.Windows.Forms.ComboBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtType2 = new System.Windows.Forms.ComboBox();
            this.txtSack2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType1 = new System.Windows.Forms.ComboBox();
            this.txtSack1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radShift = new System.Windows.Forms.RadioButton();
            this.radOutput = new System.Windows.Forms.RadioButton();
            this.gPreference = new System.Windows.Forms.GroupBox();
            this.dgvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvShift1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvShift2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTotalWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOuputKG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTotalQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDS)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gPreference.SuspendLayout();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(30, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(144, 25);
            this.label19.TabIndex = 97;
            this.label19.Text = "SẢN LƯỢNG";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Location = new System.Drawing.Point(35, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1287, 29);
            this.panel1.TabIndex = 109;
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
            this.label13.Size = new System.Drawing.Size(95, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "DACH SÁCH";
            // 
            // dataDS
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataDS.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataDS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvID,
            this.dgvDate,
            this.dgvShift1,
            this.dgvShift2,
            this.TotalWeight,
            this.dgvTotalWeight,
            this.dgvOutput,
            this.dgvOuputKG,
            this.dgvTotalQuantity,
            this.dgvNote});
            this.dataDS.Location = new System.Drawing.Point(35, 236);
            this.dataDS.Name = "dataDS";
            this.dataDS.ReadOnly = true;
            this.dataDS.Size = new System.Drawing.Size(1287, 224);
            this.dataDS.TabIndex = 108;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(35, 466);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1287, 129);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(51, 45);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(160, 20);
            this.txtDate.TabIndex = 116;
            this.txtDate.ValueChanged += new System.EventHandler(this.txtDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 117;
            this.label1.Text = "Ngày";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gPreference);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtNote);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTypeOuput);
            this.groupBox2.Controls.Add(this.txtOutput);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtType2);
            this.groupBox2.Controls.Add(this.txtSack2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtType1);
            this.groupBox2.Controls.Add(this.txtSack1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDate);
            this.groupBox2.Location = new System.Drawing.Point(35, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1287, 157);
            this.groupBox2.TabIndex = 117;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nhập thông tin";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(996, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 133;
            this.label8.Text = "Ghi chú";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(999, 45);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(200, 57);
            this.txtNote.TabIndex = 132;
            this.txtNote.Text = "\'";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEdit.Location = new System.Drawing.Point(1015, 108);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(89, 32);
            this.btnEdit.TabIndex = 131;
            this.btnEdit.Text = "Thay đổi";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Green;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Location = new System.Drawing.Point(1110, 108);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(89, 32);
            this.btnAdd.TabIndex = 130;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(749, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 129;
            this.label6.Text = "Xuất(bao)";
            // 
            // txtTypeOuput
            // 
            this.txtTypeOuput.FormattingEnabled = true;
            this.txtTypeOuput.Location = new System.Drawing.Point(752, 84);
            this.txtTypeOuput.Name = "txtTypeOuput";
            this.txtTypeOuput.Size = new System.Drawing.Size(200, 21);
            this.txtTypeOuput.TabIndex = 128;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(752, 45);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(200, 20);
            this.txtOutput.TabIndex = 127;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(749, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 126;
            this.label7.Text = "Loại";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(502, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 125;
            this.label4.Text = "Ca 2";
            // 
            // txtType2
            // 
            this.txtType2.FormattingEnabled = true;
            this.txtType2.Location = new System.Drawing.Point(502, 84);
            this.txtType2.Name = "txtType2";
            this.txtType2.Size = new System.Drawing.Size(200, 21);
            this.txtType2.TabIndex = 124;
            // 
            // txtSack2
            // 
            this.txtSack2.Location = new System.Drawing.Point(502, 45);
            this.txtSack2.Name = "txtSack2";
            this.txtSack2.Size = new System.Drawing.Size(200, 20);
            this.txtSack2.TabIndex = 123;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(499, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 122;
            this.label5.Text = "Loại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Ca 1";
            // 
            // txtType1
            // 
            this.txtType1.FormattingEnabled = true;
            this.txtType1.Location = new System.Drawing.Point(255, 84);
            this.txtType1.Name = "txtType1";
            this.txtType1.Size = new System.Drawing.Size(200, 21);
            this.txtType1.TabIndex = 120;
            // 
            // txtSack1
            // 
            this.txtSack1.Location = new System.Drawing.Point(255, 45);
            this.txtSack1.Name = "txtSack1";
            this.txtSack1.Size = new System.Drawing.Size(200, 20);
            this.txtSack1.TabIndex = 119;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 118;
            this.label2.Text = "Loại";
            // 
            // radShift
            // 
            this.radShift.AutoSize = true;
            this.radShift.Location = new System.Drawing.Point(15, 15);
            this.radShift.Name = "radShift";
            this.radShift.Size = new System.Drawing.Size(38, 17);
            this.radShift.TabIndex = 134;
            this.radShift.TabStop = true;
            this.radShift.Text = "Ca";
            this.radShift.UseVisualStyleBackColor = true;
            this.radShift.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radOutput
            // 
            this.radOutput.AutoSize = true;
            this.radOutput.Location = new System.Drawing.Point(78, 15);
            this.radOutput.Name = "radOutput";
            this.radOutput.Size = new System.Drawing.Size(47, 17);
            this.radOutput.TabIndex = 135;
            this.radOutput.TabStop = true;
            this.radOutput.Text = "Xuất";
            this.radOutput.UseVisualStyleBackColor = true;
            // 
            // gPreference
            // 
            this.gPreference.Controls.Add(this.radShift);
            this.gPreference.Controls.Add(this.radOutput);
            this.gPreference.Location = new System.Drawing.Point(46, 67);
            this.gPreference.Name = "gPreference";
            this.gPreference.Size = new System.Drawing.Size(165, 38);
            this.gPreference.TabIndex = 136;
            this.gPreference.TabStop = false;
            this.gPreference.Text = "Tùy chọn";
            // 
            // dgvID
            // 
            this.dgvID.HeaderText = "ID";
            this.dgvID.Name = "dgvID";
            this.dgvID.ReadOnly = true;
            this.dgvID.Width = 50;
            // 
            // dgvDate
            // 
            this.dgvDate.HeaderText = "Ngày";
            this.dgvDate.Name = "dgvDate";
            this.dgvDate.ReadOnly = true;
            this.dgvDate.Width = 170;
            // 
            // dgvShift1
            // 
            this.dgvShift1.HeaderText = "Ca I (bao)";
            this.dgvShift1.Name = "dgvShift1";
            this.dgvShift1.ReadOnly = true;
            this.dgvShift1.Width = 150;
            // 
            // dgvShift2
            // 
            this.dgvShift2.HeaderText = "Ca II (bao)";
            this.dgvShift2.Name = "dgvShift2";
            this.dgvShift2.ReadOnly = true;
            this.dgvShift2.Width = 150;
            // 
            // TotalWeight
            // 
            this.TotalWeight.HeaderText = "Tổng bao";
            this.TotalWeight.Name = "TotalWeight";
            this.TotalWeight.ReadOnly = true;
            // 
            // dgvTotalWeight
            // 
            this.dgvTotalWeight.HeaderText = "Tổng KG";
            this.dgvTotalWeight.Name = "dgvTotalWeight";
            this.dgvTotalWeight.ReadOnly = true;
            // 
            // dgvOutput
            // 
            this.dgvOutput.HeaderText = "Xuất (bao)";
            this.dgvOutput.Name = "dgvOutput";
            this.dgvOutput.ReadOnly = true;
            this.dgvOutput.Width = 150;
            // 
            // dgvOuputKG
            // 
            this.dgvOuputKG.HeaderText = "Xuất KG";
            this.dgvOuputKG.Name = "dgvOuputKG";
            this.dgvOuputKG.ReadOnly = true;
            // 
            // dgvTotalQuantity
            // 
            this.dgvTotalQuantity.HeaderText = "Tổng kho";
            this.dgvTotalQuantity.Name = "dgvTotalQuantity";
            this.dgvTotalQuantity.ReadOnly = true;
            // 
            // dgvNote
            // 
            this.dgvNote.HeaderText = "Ghi chú";
            this.dgvNote.Name = "dgvNote";
            this.dgvNote.ReadOnly = true;
            this.dgvNote.Width = 200;
            // 
            // MCQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataDS);
            this.Controls.Add(this.label19);
            this.Name = "MCQuantity";
            this.Size = new System.Drawing.Size(1330, 652);
            this.Load += new System.EventHandler(this.MCQuantity_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDS)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gPreference.ResumeLayout(false);
            this.gPreference.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataDS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox txtTypeOuput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox txtType2;
        private System.Windows.Forms.TextBox txtSack2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox txtType1;
        private System.Windows.Forms.TextBox txtSack1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox txtNote;
        private System.Windows.Forms.GroupBox gPreference;
        private System.Windows.Forms.RadioButton radShift;
        private System.Windows.Forms.RadioButton radOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvShift1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvShift2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTotalWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOuputKG;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTotalQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNote;
    }
}
