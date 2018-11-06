namespace WindowsFormsApplication5
{
    partial class SalesReport
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
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rbtAnually = new System.Windows.Forms.RadioButton();
            this.rbtMonthly = new System.Windows.Forms.RadioButton();
            this.rbtDaily = new System.Windows.Forms.RadioButton();
            this.dtgSales = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSales)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "d MMM yyyy";
            this.dtpEnd.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(7, 272);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(147, 25);
            this.dtpEnd.TabIndex = 108;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "d MMM yyyy";
            this.dtpStart.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(7, 181);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(147, 25);
            this.dtpStart.TabIndex = 107;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 20);
            this.label5.TabIndex = 106;
            this.label5.Text = "To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 105;
            this.label4.Text = "From:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 16);
            this.label10.TabIndex = 110;
            this.label10.Text = "Search By Status:";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.rbtAnually);
            this.panel7.Controls.Add(this.rbtMonthly);
            this.panel7.Controls.Add(this.rbtDaily);
            this.panel7.Location = new System.Drawing.Point(0, 41);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(288, 64);
            this.panel7.TabIndex = 109;
            // 
            // rbtAnually
            // 
            this.rbtAnually.AutoSize = true;
            this.rbtAnually.Location = new System.Drawing.Point(189, 32);
            this.rbtAnually.Name = "rbtAnually";
            this.rbtAnually.Size = new System.Drawing.Size(65, 17);
            this.rbtAnually.TabIndex = 2;
            this.rbtAnually.TabStop = true;
            this.rbtAnually.Text = "Annually";
            this.rbtAnually.UseVisualStyleBackColor = true;
            // 
            // rbtMonthly
            // 
            this.rbtMonthly.AutoSize = true;
            this.rbtMonthly.Location = new System.Drawing.Point(98, 32);
            this.rbtMonthly.Name = "rbtMonthly";
            this.rbtMonthly.Size = new System.Drawing.Size(62, 17);
            this.rbtMonthly.TabIndex = 1;
            this.rbtMonthly.TabStop = true;
            this.rbtMonthly.Text = "Monthly";
            this.rbtMonthly.UseVisualStyleBackColor = true;
            // 
            // rbtDaily
            // 
            this.rbtDaily.AutoSize = true;
            this.rbtDaily.Location = new System.Drawing.Point(3, 32);
            this.rbtDaily.Name = "rbtDaily";
            this.rbtDaily.Size = new System.Drawing.Size(48, 17);
            this.rbtDaily.TabIndex = 0;
            this.rbtDaily.TabStop = true;
            this.rbtDaily.Text = "Daily";
            this.rbtDaily.UseVisualStyleBackColor = true;
            // 
            // dtgSales
            // 
            this.dtgSales.AllowUserToAddRows = false;
            this.dtgSales.AllowUserToDeleteRows = false;
            this.dtgSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgSales.BackgroundColor = System.Drawing.Color.White;
            this.dtgSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSales.Location = new System.Drawing.Point(200, 128);
            this.dtgSales.Name = "dtgSales";
            this.dtgSales.ReadOnly = true;
            this.dtgSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSales.Size = new System.Drawing.Size(502, 381);
            this.dtgSales.TabIndex = 111;
            this.dtgSales.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSales_CellContentClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 11.25F);
            this.button3.Location = new System.Drawing.Point(12, 340);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(158, 48);
            this.button3.TabIndex = 112;
            this.button3.Text = "Filter";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 11.25F);
            this.button1.Location = new System.Drawing.Point(458, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 48);
            this.button1.TabIndex = 113;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 543);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dtgSales);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "SalesReport";
            this.Text = "SalesReport";
            this.Load += new System.EventHandler(this.SalesReport_Load);
            this.Shown += new System.EventHandler(this.SalesReport_Shown);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton rbtAnually;
        private System.Windows.Forms.RadioButton rbtMonthly;
        private System.Windows.Forms.RadioButton rbtDaily;
        private System.Windows.Forms.DataGridView dtgSales;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}