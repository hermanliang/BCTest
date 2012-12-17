using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KwBarcode;
using GenCode128;

namespace NuCloverBarcode
{
    public partial class NuClover : Form
    {
        private KwQRCodeWriter writer;
        private Bitmap mImage = null;
        private Image mBarcodeImage = null;
        private Bitmap mQRCodeImage = null;
        private int
            mYear,
            mMonth,
            mDate,
            mCco,
            mTco,            
            mLb,
            mTb,
            mTw,
            mTh,
            mTi;
        private float
            mSp,
            mIc;
        private string mTn;

        private long
            lVer = 0,
            lEqu = 1,
            lYear,
            lMonth,
            lDate,
            lCco,
            lTco,
            lTn,
            lSp,
            lIc,
            lLb,
            lTw,
            lTh,
            lTi,
            lTb;

        private int[] bits = {
            2,
            2,
            4,
            4,
            5,
            5,
            5,
            24,
            32,
            32,
            8,
            6,
            6,
            8,
            8
        };

        public NuClover()
        {
            InitializeComponent();
            String theVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "NuClover Barcode Generator (v." + theVersion + ")";

            writer = new KwQRCodeWriter();
            TxTb.TextChanged += onTargetParamChanged;
            TxLb.TextChanged += onTargetParamChanged;
            TxTw.TextChanged += onTargetParamChanged;
            TxTh.TextChanged += onTargetParamChanged;
            TxTi.TextChanged += onTargetParamChanged;
            comboBox1.SelectedIndexChanged += onTargetParamChanged;
            comboBox1.SelectedIndex = 0;

            generateBarcodeImage(this, new EventArgs());
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mImage = new Bitmap(openFileDialog1.FileName);
                pictureBox2.Image = drawTargetZoneOnImage(mImage);

            }
        }

        private Bitmap drawTargetZoneOnImage(Bitmap image)
        {            
            if (image == null)
                return null;

            Bitmap retImage = (Bitmap)image.Clone();
            if (gatherTargetDate())
            {
                Rectangle[] rect = getTargetRect();
                Graphics g1 = Graphics.FromImage(retImage);
                Pen p = new Pen(Color.Red);
                p.Width = 3;
                g1.DrawRectangles(p, rect);
            }

            return retImage;
        }

        private bool gatherAllDate()
        {
            if (!gatherInfoDate())
                return false;
            if (!gatherTargetDate())
                return false;
            return true;
        }

        private bool gatherInfoDate()
        {
            try
            {
                lVer = comboBox1.SelectedIndex;
                DateTime dateTime = dateTimePicker1.Value;
                mYear = dateTime.Year - 2012;
                mMonth = dateTime.Month;
                mDate = dateTime.Day;
                mCco = int.Parse(TxCco.Text);
                mTco = int.Parse(TxTco.Text);
                mTn = TxTn.Text;
                mSp = float.Parse(TxSp.Text);
                mIc = float.Parse(TxIc.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errScanning(0);
                return false;
            }
            setLabelDefault(0);
            return true;
        }

        private bool gatherTargetDate()
        {
            try
            {
                mLb = int.Parse(TxLb.Text);
                mTb = int.Parse(TxTb.Text);
                mTw = int.Parse(TxTw.Text);
                mTh = int.Parse(TxTh.Text);
                mTi = int.Parse(TxTi.Text);
                mLb = mLb < 100 ? 100 : mLb;
                mTb = mTb < 100 ? 100 : mTb;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errScanning(1);
                return false;
            }
            setLabelDefault(1);
            return true;
        }

        private void errScanning(int type)
        {
            switch (type)
            {
                case 0:
                    try { mCco = int.Parse(TxCco.Text); }
                    catch { LbCco.ForeColor = Color.Red; }
                    try { mTco = int.Parse(TxTco.Text); }
                    catch { LbTco.ForeColor = Color.Red; }
                    try { mTn = TxTn.Text; }
                    catch { LbTn.ForeColor = Color.Red; }
                    try { mSp = float.Parse(TxSp.Text); }
                    catch { LbSp.ForeColor = Color.Red; }
                    try { mIc = float.Parse(TxIc.Text); }
                    catch { LbIc.ForeColor = Color.Red; }
                    break;

                case 1:
                    try { mLb = int.Parse(TxLb.Text); }
                    catch { LbLb.ForeColor = Color.Red; }
                    try { mTb = int.Parse(TxTb.Text); }
                    catch { LbTb.ForeColor = Color.Red; }
                    try { mTw = int.Parse(TxTw.Text); }
                    catch { LbTw.ForeColor = Color.Red; }
                    try { mTh = int.Parse(TxTh.Text); }
                    catch { LbTh.ForeColor = Color.Red; }
                    try { mTi = int.Parse(TxTi.Text); }
                    catch { LbTi.ForeColor = Color.Red; }
                    break;

            }
        }

        private void setLabelDefault(int type)
        {
            switch (type)
            {
                case 0:
                    LbCco.ForeColor = Color.Black;
                    LbTco.ForeColor = Color.Black;
                    LbTn.ForeColor = Color.Black;
                    LbSp.ForeColor = Color.Black;
                    LbIc.ForeColor = Color.Black;
                    break;

                case 1:
                    LbLb.ForeColor = Color.Black;
                    LbTb.ForeColor = Color.Black;
                    LbTw.ForeColor = Color.Black;
                    LbTh.ForeColor = Color.Black;
                    LbTi.ForeColor = Color.Black;
                    break;

            }
        }

        private Rectangle[] getTargetRect()
        {
            Rectangle[] rect = null;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    rect = new Rectangle[2];
                    rect[0] = new Rectangle(mLb, mTb, mTw, mTh);
                    rect[1] = new Rectangle(mLb, mTb + mTi, mTw, mTh);
                    break;
                case 1:
                    rect = new Rectangle[4];
                    rect[0] = new Rectangle(mLb, mTb, mTw, mTh);
                    rect[1] = new Rectangle(mLb, mTb + mTi, mTw, mTh);
                    rect[2] = new Rectangle(mLb, mTb + mTi * 2, mTw, mTh);
                    rect[3] = new Rectangle(mLb, mTb + mTi * 3, mTw, mTh);
                    break;

            }
            
            return rect;
        }

        private void onTargetParamChanged(object sender, EventArgs e)
        {
            pictureBox2.Image = drawTargetZoneOnImage(mImage);
        }

        private Bitmap generateQRCodeImage(string BC)
        {   
            if (BC == null || BC.Equals(""))
                return null;
            return writer.textToQRImage(BC);
        }

        private void generateBarcodeImage(object sender, EventArgs e)
        {
            if (gatherAllDate())
            {
                convertAllDataToLong();
                long[] values = {
                    lVer,
                    lEqu,
                    lYear,
                    lMonth,
                    lDate,
                    lCco,
                    lTco,
                    lTn,
                    lSp,
                    lIc,
                    lLb,
                    lTw,
                    lTh,
                    lTi,
                    lTb
                };
                List<string> barcodes = BarcodeCore.BarcodeEncoder(bits, values);
                string BC = "";
                if (barcodes != null)
                {
                    foreach (string barcode in barcodes)
                    {
                        BC += barcode;
                    }
                    mBarcodeImage = Code128Rendering.MakeBarcodeImage(BC, 5, true);
                    mQRCodeImage = writer.textToQRImage(BC);
                }
                else
                {
                    mBarcodeImage = null;
                    mQRCodeImage = null;
                }
            }
            else
            {
                mBarcodeImage = null;
                mQRCodeImage = null;
            }
            
            pictureBox1.Image = mBarcodeImage;
            pictureBox3.Image = mQRCodeImage;
        }

        private void convertAllDataToLong()
        {
            lYear = (long)mYear;
            lMonth = (long)mMonth;
            lDate = (long)mDate;
            lCco = (long)mCco;
            lTco = (long)mTco;
            lTn = BarcodeCore.TextToLong(mTn);
            lSp = BarcodeCore.FloatToLong(mSp);
            lIc = BarcodeCore.FloatToLong(mIc);
            lLb = (long)(mLb - 100);
            lTw = (long)mTw;
            lTh = (long)mTh;
            lTi = (long)mTi;
            lTb = (long)(mTb - 100);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mBarcodeImage != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    mBarcodeImage.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    MessageBox.Show("Complete", "Message");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cb.Text = "+/-";
                lEqu = 1;
            }
            else
            {
                cb.Text = "-/+";
                lEqu = 0;
            }
            generateBarcodeImage(sender, e);
        }

        private void saveQRCode_Click(object sender, EventArgs e)
        {
            if (mQRCodeImage != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    mQRCodeImage.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    MessageBox.Show("Complete", "Message");
                }
            }
        }
    }
}
