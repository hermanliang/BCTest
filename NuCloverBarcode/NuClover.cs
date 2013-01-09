using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using KwBarcode;

namespace NuCloverBarcode
{
    public partial class NuClover : Form
    {
        private Bitmap mImage = null;
        private Size imgSize;
        private Bitmap mQRCodeImage = null;
        private FlowLayoutPanel[] ItemFlowPanels;
        private Panel[] ItemPanels;
        private Panel[] RCPanels;

        private TextBox[] RLUs, Concs;
        private int
            mCco,
            mTco,            
            mLb,
            mRb,
            mTb,
            mTw,
            mTh,
            mTi,
            mRows,
            mLines;
        private float
            mTCoC
            ;

        private string mTn;
        float[] fRLUs = new float[5];
        float[] fConcs = new float[5];
        long[] lRLUs = new long[5];
        long[] lConcs = new long[5];
        private long lTBandAppear = 1;

        private const int blockSize = 20;

        private long[] data;
        private int[] bits;

        public NuClover()
        {
            InitializeComponent();

            bits = new int[blockSize];
            for (int i = 0; i < bits.Length; i++) bits[i] = 32;
            //bits[blockSize - 1] = 24;

            String theVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "NuClover Barcode Generator (v." + theVersion + ")";

            ItemFlowPanels = new FlowLayoutPanel[]{
                flowItem1,
                flowItem2,
                flowItem3,
                flowItem4,
                flowItem5,
                flowItem6};
            ItemPanels = new Panel[]{
                panelItem1,
                panelItem2,
                panelItem3,
                panelItem4,
                panelItem5,
                panelItem6};
            RCPanels = new Panel[]{
                RCPanel1,
                RCPanel2,
                RCPanel3,
                RCPanel4,
                RCPanel5};
            RLUs = new TextBox[]{
                txT1RLU1,
                txT1RLU2,
                txT1RLU3,
                txT1RLU4,
                txT1RLU5};
            Concs = new TextBox[]{
                txT1Conc1,
                txT1Conc2,
                txT1Conc3,
                txT1Conc4,
                txT1Conc5};

            TxTb.TextChanged += onTargetParamChanged;
            TxLb.TextChanged += onTargetParamChanged;
            TxRb.TextChanged += onTargetParamChanged;
            TxTw.TextChanged += onTargetParamChanged;
            TxTh.TextChanged += onTargetParamChanged;
            TxTi.TextChanged += onTargetParamChanged;
            TxRows.TextChanged += onTargetParamChanged;
            TxLines.TextChanged += onTargetParamChanged;
            CBConcUnit.SelectedIndex = 0;
            

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
                p.Width = 2;
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
                        throw ex;
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
                mRb = int.Parse(TxRb.Text);
                mTb = int.Parse(TxTb.Text);
                mTw = int.Parse(TxTw.Text);
                mTh = int.Parse(TxTh.Text);
                mTi = int.Parse(TxTi.Text);
                mRows = int.Parse(TxRows.Text);
                mLines = int.Parse(TxLines.Text);
                if (mLb < 0 || mLb > 255|| mTb < 0 || mTb > 255) throw new Exception("mLb, mTb < 100");
                if (mRb < 0 || mRb > 255) throw new Exception();
                if (mTw < 0 || mTw > 255 || mTh < 0 || mTh > 255 || mTi < 0 || mTi > 255) throw new Exception("Target bound error");
                if (mRows < 1 || mLines < 2 || mLines > 6) throw new Exception();
                for (int i = 0; i < ItemPanels.Length; i++)
                {
                    if (i < mRows * (mLines - 1)) ItemPanels[i].Visible = true;
                    else ItemPanels[i].Visible = false;
                }
                
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
            data = new long[blockSize];
            byte[] dataBytes = new byte[4];
            // 0: [Version, Concentration Unit, null, T Band Appears]
            dataBytes[0] = 0;
            dataBytes[1] = (byte)CBConcUnit.SelectedIndex;

            dataBytes[3] = (byte)lTBandAppear;
            data[0] = dataBytes2Long(dataBytes);

            // 1 - 5: Product ID
            string[] prodID = splitString4char(txProdId.Text, 5);
            for (int i = 0; i < 5; i++)
            {
                data[i + 1] = BarcodeCore.Text256ToLong(prodID[i]);
            }

            // 6 - 8: Product Lot
            string[] prodLot = splitString4char(txProdLot.Text, 3);
            for (int i = 0; i < 3; i++)
            {
                data[i + 6] = BarcodeCore.Text256ToLong(prodLot[i]);
            }

            // 9: Expiration Date
            DateTime dateTime = dateTimePicker1.Value;
            dataBytes = new byte[4];
            dataBytes[0] = (byte)dateTime.Day;
            dataBytes[1] = (byte)dateTime.Month;
            byte[] expYear = BitConverter.GetBytes(dateTime.Year);
            dataBytes[2] = expYear[0];
            dataBytes[3] = expYear[1];
            data[9] = dataBytes2Long(dataBytes);

            // 10: [Left Bound, Top Bound, Target Width, Target Height]
            dataBytes = new byte[4];
            dataBytes[0] = (byte)mLb;
            dataBytes[1] = (byte)mTb;
            dataBytes[2] = (byte)mTw;
            dataBytes[3] = (byte)mTh;
            data[10] = dataBytes2Long(dataBytes);

            // 11: [Right Bound, Target C-T Interval, Target T-T Interval, Invalid Threshold (C Cutoff)]
            dataBytes = new byte[4];
            dataBytes[0] = (byte)mRb;
            dataBytes[1] = (byte)mTi;
            dataBytes[2] = (byte)mTi;
            dataBytes[3] = (byte)mCco;
            data[11] = dataBytes2Long(dataBytes);

            // 12: [Rows, Lines, T1 Cutoff RLU, T2 Cutoff RLU]
            dataBytes = new byte[4];
            dataBytes[0] = (byte)mRows;
            dataBytes[1] = (byte)mLines;
            dataBytes[2] = (byte)mTco;

            data[12] = dataBytes2Long(dataBytes);

            // 13: T1 Cutoff Concentration
            data[13] = BarcodeCore.FloatToLong(mTCoC);

            // 14: T1 Name
            data[14] = BarcodeCore.Text256ToLong(mTn);

            // 15 - 19: T1 RLU-Concentration Pairs x 5
            for (int i = 0; i < fRLUs.Length; i++)
            {
                dataBytes = new byte[4];
                byte[] tmpRLU = BarcodeCore.float16ToBytes(fRLUs[i]);
                byte[] tmpConc = BarcodeCore.float16ToBytes(fConcs[i]);
                dataBytes[0] = tmpRLU[0];
                dataBytes[1] = tmpRLU[1];
                dataBytes[2] = tmpConc[0];
                dataBytes[3] = tmpConc[1];
                data[i + 15] = dataBytes2Long(dataBytes);
            }

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
                    for (int i = 0; i < fRLUs.Length; i++)
                    {
                        try
                        {
                            fRLUs[i] = float.Parse(RLUs[i].Text);
                            fConcs[i] = float.Parse(Concs[i].Text);
                        }
                        catch
                        {
                            ItemFlowPanels[0].BackColor = Color.Red;
                        }
                    }
                    break;

                case 1:
                    try
                    {
                        mLb = int.Parse(TxLb.Text);
                        if (mLb < 0 || mLb > 255) throw new Exception();
                    }
                    catch { LbLb.ForeColor = Color.Red; }
                    try
                    {
                        mTb = int.Parse(TxTb.Text);
                        if (mTb < 0 || mTb > 255) throw new Exception();
                    }
                    catch { LbTb.ForeColor = Color.Red; }
                    try {
                        mTw = int.Parse(TxTw.Text);
                        if (mTw < 0 || mTw > 255) throw new Exception();
                    }
                    catch { LbTw.ForeColor = Color.Red; }
                    try {
                        mTh = int.Parse(TxTh.Text);
                        if (mTh < 0 || mTh > 255) throw new Exception();
                    }
                    catch { LbTh.ForeColor = Color.Red; }
                    try
                    {
                        mTi = int.Parse(TxTi.Text);
                        if (mTi < 0 || mTi > 255) throw new Exception();
                    }
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
                    if (ItemPanels != null)
                    {
                        for (int i = 0; i < ItemPanels.Length; i++)
                        {
                            if (i % 2 == 0) ItemFlowPanels[i].BackColor = Color.LightGray;
                            else ItemFlowPanels[i].BackColor = Color.WhiteSmoke;
                        }
                    }

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
            //int lineNum = 0;
            Rectangle[] rect = null;
            rect = new Rectangle[mRows * mLines];
            int leftBound = (int)((double)imgSize.Width / 2 - ((double)mLb / 255) * ((double)imgSize.Width / 2));
            int rightBound = (int)((double)imgSize.Width / 2 + ((double)mRb / 255) * ((double)imgSize.Width / 2));
            int topBound = (int)((double)imgSize.Height / 255 * mTb);
            double rowInterval = 0;
            if (mRows > 1)
                rowInterval = (double)(rightBound - leftBound - mTw * mRows) / (mRows - 1);
            for (int i = 0 ; i < mRows; i++)
                for (int j = 0; j < mLines; j++)
                {
                    rect[i * mLines + j] = new Rectangle(
                        leftBound + (int)((mTw + rowInterval) * i),
                        topBound + mTi * j,
                        mTw,
                        mTh);
                }

            return rect;
        }

        #endregion

        #region Event Handler

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mImage = new Bitmap(openFileDialog1.FileName);
                imgSize = mImage.Size;
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
                List<string> barcodes = BarcodeCore.BarcodeEncoder(bits, data);
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
                    mQRCodeImage = KwQRCodeWriter.textToQRImage(BC, QR_CORRECT_LEV.M);
#if DEBUG
                    int[] retData = BarcodeCore.BarcodeDecoder(BC, bits);
                    byte[] dataBytes;
                    // 0: [Version, Concentration Unit, RLU-Conc Points, T Band Appears]
                    dataBytes = int2DataBytes(retData[0]);
                    int dVersion = dataBytes[0];
                    int dConcUnit = dataBytes[1];
                    //int dRLUConcPts = dataBytes[2];
                    int dTband = dataBytes[3];

                    // 1 - 5: Product ID
                    string dProdID = "";
                    for (int i = 1; i < 6; i++)                    
                    {
                        dProdID += BarcodeCore.IntToText256(retData[i]);
                    }

                    // 6 - 8: Product Lot
                    string dProdLot = "";
                    for (int i = 6; i < 9; i++)
                    {
                        dProdLot += BarcodeCore.IntToText256(retData[i]);
                    }

                    // 9: Expiration Date
                    dataBytes = int2DataBytes(retData[9]);
                    int dDay = dataBytes[0];
                    int dMonth = dataBytes[1];
                    int dYear = BitConverter.ToUInt16(dataBytes, 2);

                    // 10: [Left Bound, Top Bound, Target Width, Target Height]
                    dataBytes = int2DataBytes(retData[10]);
                    int dLb = dataBytes[0];
                    int dTb = dataBytes[1];
                    int dTw = dataBytes[2];
                    int dTh = dataBytes[3];

                    // 11: [Right Bound, Target C-T Interval, Target T-T Interval, Invalid Threshold (C Cutoff)]
                    dataBytes = int2DataBytes(retData[11]);
                    int dRb = dataBytes[0];
                    int dTCTi = dataBytes[1];
                    int dTTTi = dataBytes[2];
                    int dCco = dataBytes[3];

                    // 12: [Rows, Lines, T1 Cutoff RLU, T2 Cutoff RLU]
                    dataBytes = int2DataBytes(retData[12]);
                    int dRows = dataBytes[0];
                    int dLines = dataBytes[1];
                    int dTco = dataBytes[2];

                    // 13: T1 Cutoff Concentration
                    float dTcoConc = BarcodeCore.IntToFloat(retData[13]);

                    // 14: T1 Name
                    string dTName = BarcodeCore.IntToText256(retData[14]);

                    // 15 - 19: T1 RLU-Concentration Pairs x 4
                    double[] dRLU = new double[5];
                    double[] dConc = new double[5];
                    for (int i = 0; i < 5; i++)
                    {
                        dataBytes = int2DataBytes(retData[15 + i]);
                        dRLU[i] = BarcodeCore.DecodeFloat16(BitConverter.ToUInt16(dataBytes,0));
                        dConc[i] = BarcodeCore.DecodeFloat16(BitConverter.ToUInt16(dataBytes, 2));
                        Console.WriteLine(dRLU[i].ToString() + "\t" + dConc[i].ToString());
                    }
                    Console.WriteLine("Check Complete!");

#endif
                }
                else
                {
                    mQRCodeImage = null;
                }
            }
            else
            {
                mQRCodeImage = null;
            }
            pictureBox3.Image = mQRCodeImage;
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

        

        static public long dataBytes2Long(byte[] dataBytes)
        {
            if (dataBytes == null) throw new Exception("Null target error!");
            if (dataBytes.Length < 4)
            {
                byte[] tmpArray = new byte[4];
                for (int i = 0; i < dataBytes.Length; i++) tmpArray[i] = dataBytes[i];
                dataBytes = tmpArray;
            }
            return (long)BitConverter.ToUInt32(dataBytes, 0);
        }

        static public byte[] int2DataBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        static public string[] splitString4char(string text, int blocks)
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

        

    }
}
