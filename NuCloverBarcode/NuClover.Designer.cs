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
            this.LbCco = new System.Windows.Forms.Label();
            this.LbTco = new System.Windows.Forms.Label();
            this.LbTn = new System.Windows.Forms.Label();
            this.LbSp = new System.Windows.Forms.Label();
            this.LbIc = new System.Windows.Forms.Label();
            this.LbLb = new System.Windows.Forms.Label();
            this.LbTb = new System.Windows.Forms.Label();
            this.LbTw = new System.Windows.Forms.Label();
            this.LbTh = new System.Windows.Forms.Label();
            this.LbTi = new System.Windows.Forms.Label();
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
            this.TxTn.MaxLength = 4;
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
            // LbCco
            // 
            this.LbCco.Location = new System.Drawing.Point(13, 40);
            this.LbCco.Name = "LbCco";
            this.LbCco.Size = new System.Drawing.Size(143, 23);
            this.LbCco.TabIndex = 12;
            this.LbCco.Text = "C-Cutoff (0-30):";
            this.LbCco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTco
            // 
            this.LbTco.Location = new System.Drawing.Point(13, 68);
            this.LbTco.Name = "LbTco";
            this.LbTco.Size = new System.Drawing.Size(143, 23);
            this.LbTco.TabIndex = 13;
            this.LbTco.Text = "T-Cutoff (0-30):";
            this.LbTco.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTn
            // 
            this.LbTn.Location = new System.Drawing.Point(12, 96);
            this.LbTn.Name = "LbTn";
            this.LbTn.Size = new System.Drawing.Size(144, 23);
            this.LbTn.TabIndex = 14;
            this.LbTn.Text = "T-Name (Max. length 4):";
            this.LbTn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbSp
            // 
            this.LbSp.Location = new System.Drawing.Point(218, 12);
            this.LbSp.Name = "LbSp";
            this.LbSp.Size = new System.Drawing.Size(143, 23);
            this.LbSp.TabIndex = 15;
            this.LbSp.Text = "Slope:";
            this.LbSp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbIc
            // 
            this.LbIc.Location = new System.Drawing.Point(218, 40);
            this.LbIc.Name = "LbIc";
            this.LbIc.Size = new System.Drawing.Size(143, 23);
            this.LbIc.TabIndex = 16;
            this.LbIc.Text = "Intercept:";
            this.LbIc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbLb
            // 
            this.LbLb.Location = new System.Drawing.Point(218, 68);
            this.LbLb.Name = "LbLb";
            this.LbLb.Size = new System.Drawing.Size(143, 23);
            this.LbLb.TabIndex = 17;
            this.LbLb.Text = "Left bound (0-255):";
            this.LbLb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTb
            // 
            this.LbTb.Location = new System.Drawing.Point(218, 96);
            this.LbTb.Name = "LbTb";
            this.LbTb.Size = new System.Drawing.Size(143, 23);
            this.LbTb.TabIndex = 18;
            this.LbTb.Text = "Top bound (0-255):";
            this.LbTb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTw
            // 
            this.LbTw.Location = new System.Drawing.Point(423, 14);
            this.LbTw.Name = "LbTw";
            this.LbTw.Size = new System.Drawing.Size(143, 23);
            this.LbTw.TabIndex = 19;
            this.LbTw.Text = "Target width (0-63):";
            this.LbTw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTh
            // 
            this.LbTh.Location = new System.Drawing.Point(423, 40);
            this.LbTh.Name = "LbTh";
            this.LbTh.Size = new System.Drawing.Size(143, 23);
            this.LbTh.TabIndex = 20;
            this.LbTh.Text = "Target height (0-63):";
            this.LbTh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LbTi
            // 
            this.LbTi.Location = new System.Drawing.Point(423, 68);
            this.LbTi.Name = "LbTi";
            this.LbTi.Size = new System.Drawing.Size(143, 23);
            this.LbTi.TabIndex = 21;
            this.LbTi.Text = "Target interval (0-255):";
            this.LbTi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Controls.Add(this.LbTi);
            this.Controls.Add(this.LbTh);
            this.Controls.Add(this.LbTw);
            this.Controls.Add(this.LbTb);
            this.Controls.Add(this.LbLb);
            this.Controls.Add(this.LbIc);
            this.Controls.Add(this.LbSp);
            this.Controls.Add(this.LbTn);
            this.Controls.Add(this.LbTco);
            this.Controls.Add(this.LbCco);
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
        private System.Windows.Forms.Label LbCco;
        private System.Windows.Forms.Label LbTco;
        private System.Windows.Forms.Label LbTn;
        private System.Windows.Forms.Label LbSp;
        private System.Windows.Forms.Label LbIc;
        private System.Windows.Forms.Label LbLb;
        private System.Windows.Forms.Label LbTb;
        private System.Windows.Forms.Label LbTw;
        private System.Windows.Forms.Label LbTh;
        private System.Windows.Forms.Label LbTi;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

