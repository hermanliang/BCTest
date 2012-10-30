namespace NuCloverBarcode
{
    partial class NuClover
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.TxCco = new System.Windows.Forms.TextBox();
            this.TxTco = new System.Windows.Forms.TextBox();
            this.TxTn = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TxSp = new System.Windows.Forms.TextBox();
            this.TxIc = new System.Windows.Forms.TextBox();
            this.TxTw = new System.Windows.Forms.TextBox();
            this.TxTh = new System.Windows.Forms.TextBox();
            this.TxTb = new System.Windows.Forms.TextBox();
            this.TxLb = new System.Windows.Forms.TextBox();
            this.TxTi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(67, 12);
            this.dateTimePicker1.MaxDate = new System.DateTime(2027, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(145, 22);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // TxCco
            // 
            this.TxCco.Location = new System.Drawing.Point(162, 40);
            this.TxCco.Name = "TxCco";
            this.TxCco.Size = new System.Drawing.Size(50, 22);
            this.TxCco.TabIndex = 1;
            this.TxCco.Text = "10";
            this.TxCco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxCco.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxTco
            // 
            this.TxTco.Location = new System.Drawing.Point(162, 68);
            this.TxTco.Name = "TxTco";
            this.TxTco.Size = new System.Drawing.Size(50, 22);
            this.TxTco.TabIndex = 2;
            this.TxTco.Text = "10";
            this.TxTco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTco.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxTn
            // 
            this.TxTn.Location = new System.Drawing.Point(162, 96);
            this.TxTn.Name = "TxTn";
            this.TxTn.Size = new System.Drawing.Size(50, 22);
            this.TxTn.TabIndex = 3;
            this.TxTn.Text = "Test";
            this.TxTn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTn.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 172);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(460, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // TxSp
            // 
            this.TxSp.Location = new System.Drawing.Point(367, 12);
            this.TxSp.Name = "TxSp";
            this.TxSp.Size = new System.Drawing.Size(50, 22);
            this.TxSp.TabIndex = 5;
            this.TxSp.Text = "1.0";
            this.TxSp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxSp.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxIc
            // 
            this.TxIc.Location = new System.Drawing.Point(367, 40);
            this.TxIc.Name = "TxIc";
            this.TxIc.Size = new System.Drawing.Size(50, 22);
            this.TxIc.TabIndex = 6;
            this.TxIc.Text = "0";
            this.TxIc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxIc.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxTw
            // 
            this.TxTw.Location = new System.Drawing.Point(572, 12);
            this.TxTw.Name = "TxTw";
            this.TxTw.Size = new System.Drawing.Size(100, 22);
            this.TxTw.TabIndex = 7;
            this.TxTw.Text = "50";
            this.TxTw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTw.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxTh
            // 
            this.TxTh.Location = new System.Drawing.Point(572, 40);
            this.TxTh.Name = "TxTh";
            this.TxTh.Size = new System.Drawing.Size(100, 22);
            this.TxTh.TabIndex = 8;
            this.TxTh.Text = "60";
            this.TxTh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTh.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxTb
            // 
            this.TxTb.Location = new System.Drawing.Point(367, 96);
            this.TxTb.Name = "TxTb";
            this.TxTb.Size = new System.Drawing.Size(50, 22);
            this.TxTb.TabIndex = 9;
            this.TxTb.Text = "220";
            this.TxTb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTb.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxLb
            // 
            this.TxLb.Location = new System.Drawing.Point(367, 68);
            this.TxLb.Name = "TxLb";
            this.TxLb.Size = new System.Drawing.Size(50, 22);
            this.TxLb.TabIndex = 10;
            this.TxLb.Text = "215";
            this.TxLb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxLb.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // TxTi
            // 
            this.TxTi.Location = new System.Drawing.Point(572, 68);
            this.TxTi.Name = "TxTi";
            this.TxTi.Size = new System.Drawing.Size(100, 22);
            this.TxTi.TabIndex = 11;
            this.TxTi.Text = "80";
            this.TxTi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTi.TextChanged += new System.EventHandler(this.generateBarcodeImage);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "C-Cutoff (0-30):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "T-Cutoff (0-30):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "T-Name (Max. length 4):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(218, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "Slope:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(218, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Intercept:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(218, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 23);
            this.label6.TabIndex = 17;
            this.label6.Text = "Left bound:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(218, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 23);
            this.label7.TabIndex = 18;
            this.label7.Text = "Top bound:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(423, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 23);
            this.label8.TabIndex = 19;
            this.label8.Text = "Target width:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(423, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 23);
            this.label9.TabIndex = 20;
            this.label9.Text = "Target height:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(423, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 23);
            this.label10.TabIndex = 21;
            this.label10.Text = "Target interval:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 23);
            this.label11.TabIndex = 22;
            this.label11.Text = "Date:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(478, 96);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(217, 154);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(15, 133);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(75, 23);
            this.btnLoadImage.TabIndex = 24;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(179, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 23);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Save Barcode";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "BMP File (*.bmp)|*.bmp";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PNG, JPEG File (*.png, *.jpg)|*.png;*.jpg";
            // 
            // NuClover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 262);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxTi);
            this.Controls.Add(this.TxLb);
            this.Controls.Add(this.TxTb);
            this.Controls.Add(this.TxTh);
            this.Controls.Add(this.TxTw);
            this.Controls.Add(this.TxIc);
            this.Controls.Add(this.TxSp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TxTn);
            this.Controls.Add(this.TxTco);
            this.Controls.Add(this.TxCco);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "NuClover";
            this.Text = "NuClover Barcode Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox TxCco;
        private System.Windows.Forms.TextBox TxTco;
        private System.Windows.Forms.TextBox TxTn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TxSp;
        private System.Windows.Forms.TextBox TxIc;
        private System.Windows.Forms.TextBox TxTw;
        private System.Windows.Forms.TextBox TxTh;
        private System.Windows.Forms.TextBox TxTb;
        private System.Windows.Forms.TextBox TxLb;
        private System.Windows.Forms.TextBox TxTi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

