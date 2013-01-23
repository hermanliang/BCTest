namespace SyntronClient
{
    partial class MainForm
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
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxRb = new System.Windows.Forms.TextBox();
            this.LbTi = new System.Windows.Forms.Label();
            this.LbTh = new System.Windows.Forms.Label();
            this.LbTw = new System.Windows.Forms.Label();
            this.LbTb = new System.Windows.Forms.Label();
            this.LbLb = new System.Windows.Forms.Label();
            this.TxCTi = new System.Windows.Forms.TextBox();
            this.TxLb = new System.Windows.Forms.TextBox();
            this.TxTb = new System.Windows.Forms.TextBox();
            this.TxTh = new System.Windows.Forms.TextBox();
            this.TxTw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxTTi = new System.Windows.Forms.TextBox();
            this.modeList = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(305, 9);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(128, 36);
            this.btnLoadImage.TabIndex = 26;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(175, 52);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(258, 303);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(28, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 25);
            this.label10.TabIndex = 59;
            this.label10.Text = "Right bound";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxRb
            // 
            this.TxRb.Location = new System.Drawing.Point(103, 116);
            this.TxRb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxRb.MaxLength = 3;
            this.TxRb.Name = "TxRb";
            this.TxRb.Size = new System.Drawing.Size(34, 21);
            this.TxRb.TabIndex = 2;
            this.TxRb.Text = "315";
            this.TxRb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxRb.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // LbTi
            // 
            this.LbTi.Location = new System.Drawing.Point(28, 214);
            this.LbTi.Name = "LbTi";
            this.LbTi.Size = new System.Drawing.Size(75, 25);
            this.LbTi.TabIndex = 57;
            this.LbTi.Text = "C-T Distance";
            this.LbTi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LbTh
            // 
            this.LbTh.Location = new System.Drawing.Point(28, 189);
            this.LbTh.Name = "LbTh";
            this.LbTh.Size = new System.Drawing.Size(75, 25);
            this.LbTh.TabIndex = 56;
            this.LbTh.Text = "Target height";
            this.LbTh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LbTw
            // 
            this.LbTw.Location = new System.Drawing.Point(28, 164);
            this.LbTw.Name = "LbTw";
            this.LbTw.Size = new System.Drawing.Size(75, 25);
            this.LbTw.TabIndex = 55;
            this.LbTw.Text = "Target width";
            this.LbTw.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LbTb
            // 
            this.LbTb.Location = new System.Drawing.Point(28, 139);
            this.LbTb.Name = "LbTb";
            this.LbTb.Size = new System.Drawing.Size(75, 25);
            this.LbTb.TabIndex = 54;
            this.LbTb.Text = "Start height";
            this.LbTb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LbLb
            // 
            this.LbLb.Location = new System.Drawing.Point(28, 89);
            this.LbLb.Name = "LbLb";
            this.LbLb.Size = new System.Drawing.Size(75, 25);
            this.LbLb.TabIndex = 53;
            this.LbLb.Text = "Left bound";
            this.LbLb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxCTi
            // 
            this.TxCTi.Location = new System.Drawing.Point(103, 216);
            this.TxCTi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxCTi.MaxLength = 3;
            this.TxCTi.Name = "TxCTi";
            this.TxCTi.Size = new System.Drawing.Size(34, 21);
            this.TxCTi.TabIndex = 6;
            this.TxCTi.Text = "70";
            this.TxCTi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxCTi.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // TxLb
            // 
            this.TxLb.Location = new System.Drawing.Point(103, 91);
            this.TxLb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxLb.MaxLength = 3;
            this.TxLb.Name = "TxLb";
            this.TxLb.Size = new System.Drawing.Size(34, 21);
            this.TxLb.TabIndex = 1;
            this.TxLb.Text = "205";
            this.TxLb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxLb.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // TxTb
            // 
            this.TxTb.Location = new System.Drawing.Point(103, 141);
            this.TxTb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxTb.MaxLength = 3;
            this.TxTb.Name = "TxTb";
            this.TxTb.Size = new System.Drawing.Size(34, 21);
            this.TxTb.TabIndex = 3;
            this.TxTb.Text = "165";
            this.TxTb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTb.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // TxTh
            // 
            this.TxTh.Location = new System.Drawing.Point(103, 191);
            this.TxTh.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxTh.MaxLength = 3;
            this.TxTh.Name = "TxTh";
            this.TxTh.Size = new System.Drawing.Size(34, 21);
            this.TxTh.TabIndex = 5;
            this.TxTh.Text = "55";
            this.TxTh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTh.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // TxTw
            // 
            this.TxTw.Location = new System.Drawing.Point(103, 166);
            this.TxTw.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxTw.MaxLength = 3;
            this.TxTw.Name = "TxTw";
            this.TxTw.Size = new System.Drawing.Size(34, 21);
            this.TxTw.TabIndex = 4;
            this.TxTw.Text = "45";
            this.TxTw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTw.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 25);
            this.label1.TabIndex = 61;
            this.label1.Text = "T-T Distance";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxTTi
            // 
            this.TxTTi.Location = new System.Drawing.Point(103, 241);
            this.TxTTi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TxTTi.MaxLength = 3;
            this.TxTTi.Name = "TxTTi";
            this.TxTTi.Size = new System.Drawing.Size(34, 21);
            this.TxTTi.TabIndex = 7;
            this.TxTTi.Text = "62";
            this.TxTTi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxTTi.TextChanged += new System.EventHandler(this.onTargetParamChanged);
            // 
            // modeList
            // 
            this.modeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeList.FormattingEnabled = true;
            this.modeList.Items.AddRange(new object[] {
            "2C3T",
            "2C5T"});
            this.modeList.Location = new System.Drawing.Point(12, 52);
            this.modeList.Name = "modeList";
            this.modeList.Size = new System.Drawing.Size(137, 23);
            this.modeList.TabIndex = 0;
            this.modeList.SelectedIndexChanged += new System.EventHandler(this.modeList_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 369);
            this.Controls.Add(this.modeList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxTTi);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.TxRb);
            this.Controls.Add(this.LbTi);
            this.Controls.Add(this.LbTh);
            this.Controls.Add(this.LbTw);
            this.Controls.Add(this.LbTb);
            this.Controls.Add(this.LbLb);
            this.Controls.Add(this.TxCTi);
            this.Controls.Add(this.TxLb);
            this.Controls.Add(this.TxTb);
            this.Controls.Add(this.TxTh);
            this.Controls.Add(this.TxTw);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "CHR620 Client";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxRb;
        private System.Windows.Forms.Label LbTi;
        private System.Windows.Forms.Label LbTh;
        private System.Windows.Forms.Label LbTw;
        private System.Windows.Forms.Label LbTb;
        private System.Windows.Forms.Label LbLb;
        private System.Windows.Forms.TextBox TxCTi;
        private System.Windows.Forms.TextBox TxLb;
        private System.Windows.Forms.TextBox TxTb;
        private System.Windows.Forms.TextBox TxTh;
        private System.Windows.Forms.TextBox TxTw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxTTi;
        private System.Windows.Forms.ComboBox modeList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

