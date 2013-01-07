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
        private Panel[] RCPanels;
        private TextBox[] RLUs, Concs;
        private int
            mCco,
            mTco,            
            mLb,
            mTb,
            mTw,
            mTh,
            mTi;
        private float
            mTCoC
            ;

        private string mTn;
        float[] fRLUs = new float[8];
        float[] fConcs = new float[8];
        long[] lRLUs = new long[8];
        long[] lConcs = new long[8];
        private long
            lVer = 0,
            lPID1, lPID2, lPID3, lPID4, lPID5,
            lPLot1, lPLot2, lPLot3,
            lExpireDate1, lExpireDate2,
            lLb,
            lTb,
            lTw,
            lTh,
            lTi,
            lCco,
            lTBandAppear = 1,
            lUnit,
            lTn,
            lTco,
            lTcoC,
            lRluConcPts = 3
            ;
            

        private int[] bits = {
            6, // Version 0-63
            32, 32, 32, 32, 32, // Product ID
            32, 32, 32, // Product Lot
            32, 32, // Expireation Date
            8, // Left Bound
            8, // Top Bound
            6, // Target Width
            6, // Target Height
            8, // Target Interval
            8, // Invalid Threshold (C cut-off)
            1, // T Band Appears
            5, // Concentration Unit
            32, // T-Line Name
            8, // T cut-off RLU
            32, // T cut-off Concentration
            3,  // RLU-Conc. Points
            32, 32, // RLU-Conc. 1
            32, 32, // RLU-Conc. 2
            32, 32, // RLU-Conc. 3
            32, 32, // RLU-Conc. 4
            32, 32, // RLU-Conc. 5
            32, 32, // RLU-Conc. 6
            32, 32, // RLU-Conc. 7
            32, 32 // RLU-Conc. 8
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
            CBConcUnit.SelectedIndex = 0;
            RCPanels = new Panel[]{
                RCPanel1,
                RCPanel2,
                RCPanel3,
                RCPanel4,
                RCPanel5,
                RCPanel6,
                RCPanel7,
                RCPanel8};
            RLUs = new TextBox[]{
                txRLU1,
                txRLU2,
                txRLU3,
                txRLU4,
                txRLU5,
                txRLU6,
                txRLU7,
                txRLU8};
            Concs = new TextBox[]{
                txConc1,
                txConc2,
                txConc3,
                txConc4,
                txConc5,
                txConc6,
                txConc7,
                txConc8};

            generateBarcodeImage(this, new EventArgs());
        }

        /// <summary>
        /// gatherAllData -> gatherInfoData -> gatherTargetData -> convertAllDataToLong
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        #region Private Methods

        private Bitmap drawTargetZoneOnImage(Bitmap image)
        {
            if (image == null)
                return null;

            Bitmap retImage = (Bitmap)image.Clone();
            if (gatherTargetData())
            {
                Rectangle[] rect = getTargetRect();
                Graphics g1 = Graphics.FromImage(retImage);
                Pen p = new Pen(Color.Red);
                p.Width = 3;
                g1.DrawRectangles(p, rect);
            }

            return retImage;
        }

        private bool gatherAllData()
        {
            if (!gatherInfoData())
                return false;
            if (!gatherTargetData())
                return false;
            return true;
        }

        private bool gatherInfoData()
        {
            setLabelDefault(0);
            try
            {   
                mCco = int.Parse(TxCco.Text);
                mTco = int.Parse(TxTco.Text);
                mTn = TxTn.Text;
                mTCoC = float.Parse(TxTcoC.Text);
                for (int i = 0; i < fRLUs.Length; i++)
                {
                    fRLUs[i] = 0;
                    fConcs[i] = 0;
                    try
                    {
                        fRLUs[i] = float.Parse(RLUs[i].Text);
                        fConcs[i] = float.Parse(Concs[i].Text);
                    }
                    catch (Exception ex)
                    {
                        if (i <= lRluConcPts)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errScanning(0);
                return false;
            }
            return true;
        }

        private bool gatherTargetData()
        {
            setLabelDefault(1);
            try
            {
                mLb = int.Parse(TxLb.Text);
                mTb = int.Parse(TxTb.Text);
                mTw = int.Parse(TxTw.Text);
                mTh = int.Parse(TxTh.Text);
                mTi = int.Parse(TxTi.Text);
                if (mLb < 100 || mLb > 355|| mTb < 100 || mTb > 355) throw new Exception("mLb, mTb < 100");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errScanning(1);
                return false;
            }
            return true;
        }

        private void convertAllDataToLong()
        {
            long dateTime = dateTimePicker1.Value.Ticks;
            string[] prodID = splitString4char(txProdId.Text, 5);
            lPID1 = BarcodeCore.Text256ToLong(prodID[0]);
            lPID2 = BarcodeCore.Text256ToLong(prodID[1]);
            lPID3 = BarcodeCore.Text256ToLong(prodID[2]);
            lPID4 = BarcodeCore.Text256ToLong(prodID[3]);
            lPID5 = BarcodeCore.Text256ToLong(prodID[4]);
            string[] prodLot = splitString4char(txProdLot.Text, 3);
            lPLot1 = BarcodeCore.Text256ToLong(prodLot[0]);
            lPLot2 = BarcodeCore.Text256ToLong(prodLot[1]);
            lPLot3 = BarcodeCore.Text256ToLong(prodLot[2]);
            lExpireDate1 = dateTime & 0xFFFFFFFF;
            lExpireDate2 = (dateTime >> 32) & 0xFFFFFFFF;
            lCco = (long)mCco;
            lTco = (long)mTco;
            lTn = BarcodeCore.TextToLong(mTn);
            lLb = (long)(mLb - 100);
            lTw = (long)mTw;
            lTh = (long)mTh;
            lTi = (long)mTi;
            lTb = (long)(mTb - 100);
            lUnit = CBConcUnit.SelectedIndex;
            lTcoC = BarcodeCore.FloatToLong(mTCoC);

            for (int i = 0; i < fRLUs.Length; i++)
            {
                lRLUs[i] = BarcodeCore.FloatToLong(fRLUs[i]);
                lConcs[i] = BarcodeCore.FloatToLong(fConcs[i]);
            }
        }

        private string[] splitString4char(string text, int blocks)
        {
            string[] retValue = new string[blocks];
            for (int i = 0; i < blocks; i++)
            {
                retValue[i] = "";
                try
                {
                    if (text.Length > 4)
                    {
                        retValue[i] = text.Substring(0, 4);
                        text = text.Substring(4);
                    }
                    else if (text.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        retValue[i] = text;
                        text = "";
                    }

                }
                catch { continue; }
            }
            return retValue;
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
                    break;

                case 1:
                    try
                    {
                        mLb = int.Parse(TxLb.Text);
                        if (mLb < 100 || mLb > 355) throw new Exception();
                    }
                    catch { LbLb.ForeColor = Color.Red; }
                    try
                    {
                        mTb = int.Parse(TxTb.Text);
                        if (mTb < 100 || mTb > 355) throw new Exception();
                    }
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
            int lineNum = 0;
            Rectangle[] rect = null;
            switch (lineNum)
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

        private Bitmap generateQRCodeImage(string BC)
        {
            if (BC == null || BC.Equals(""))
                return null;
            return  KwQRCodeWriter.textToQRImage(BC);
        }

        #endregion

        #region Event Handler

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mImage = new Bitmap(openFileDialog1.FileName);
                pictureBox2.Image = drawTargetZoneOnImage(mImage);

            }
        }

        private void onTargetParamChanged(object sender, EventArgs e)
        {
            pictureBox2.Image = drawTargetZoneOnImage(mImage);
        }

        private void generateBarcodeImage(object sender, EventArgs e)
        {
            if (gatherAllData())
            {
                convertAllDataToLong();
                long[] values = {
                    lVer,
                    lPID1, lPID2, lPID3, lPID4, lPID5,
                    lPLot1, lPLot2, lPLot3,
                    lExpireDate1, lExpireDate2,
                    lLb,
                    lTb,
                    lTw,
                    lTh,
                    lTi,
                    lCco,
                    lTBandAppear,
                    lUnit,
                    lTn,
                    lTco,
                    lTcoC,
                    lRluConcPts,
                    lRLUs[0], lConcs[0],
                    lRLUs[1], lConcs[1],
                    lRLUs[2], lConcs[2],
                    lRLUs[3], lConcs[3],
                    lRLUs[4], lConcs[4],
                    lRLUs[5], lConcs[5],
                    lRLUs[6], lConcs[6],
                    lRLUs[7], lConcs[7]
                };
                List<string> barcodes = BarcodeCore.BarcodeEncoder(bits, values);
                string BC = "";
                if (barcodes != null)
                {
                    byte[] bcBytes = new byte[barcodes.Count * 4];
                    for (int i = 0; i < barcodes.Count; i++)
                    {
                        BC += barcodes[i];
                        long code = long.Parse(barcodes[i]);
                        byte[] codeBytes = BitConverter.GetBytes(code);
                        bcBytes[i * 4] = codeBytes[0];
                        bcBytes[i * 4 + 1] = codeBytes[1];
                        bcBytes[i * 4 + 2] = codeBytes[2];
                        bcBytes[i * 4 + 3] = codeBytes[3];
                    }
                    //foreach (string barcode in barcodes)
                    //{
                    //    BC += barcode;
                    //}
                    //mBarcodeImage = Code128Rendering.MakeBarcodeImage(BC, 5, true);
                    mQRCodeImage = KwQRCodeWriter.textToQRImage(BC);
                }
                else
                {
                    //mBarcodeImage = null;
                    mQRCodeImage = null;
                }
            }
            else
            {
                //mBarcodeImage = null;
                mQRCodeImage = null;
            }
            
            //pictureBox1.Image = mBarcodeImage;
            pictureBox3.Image = mQRCodeImage;
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
                lTBandAppear = 1;
            }
            else
            {
                cb.Text = "-/+";
                lTBandAppear = 0;
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

        #endregion

        private void btnAddRC_Click(object sender, EventArgs e)
        {
            lRluConcPts++;
            lRluConcPts = lRluConcPts > 7 ? 7 : lRluConcPts;
            RCPanels[lRluConcPts].Visible = true;
            generateBarcodeImage(sender, e);
        }

        private void btnRmRC_Click(object sender, EventArgs e)
        {
            lRluConcPts--;
            lRluConcPts = lRluConcPts < 1 ? 1 : lRluConcPts;
            RCPanels[lRluConcPts + 1].Visible = false;
            RLUs[lRluConcPts + 1].Text = "";
            Concs[lRluConcPts + 1].Text = "";
            lRLUs[lRluConcPts + 1] = 0;
            lConcs[lRluConcPts + 1] = 0;
            fRLUs[lRluConcPts + 1] = 0;
            fConcs[lRluConcPts + 1] = 0;
            generateBarcodeImage(sender, e);
        }


    }
}
