using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyntronClient
{
    public partial class MainForm : Form
    {
        private Bitmap mImage = null;
        private int
            mLb,
            mRb,
            mTb,
            mTw,
            mTh,
            mCTi,
            mTTi,
            mRows,
            mLines;

        public MainForm()
        {
            InitializeComponent();
            modeList.SelectedIndex = 0;
        }

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

        private bool gatherTargetData()
        {
            //setLabelDefault(1);
            try
            {
                mLb = int.Parse(TxLb.Text);
                mRb = int.Parse(TxRb.Text);
                mTb = int.Parse(TxTb.Text);
                mTw = int.Parse(TxTw.Text);
                mTh = int.Parse(TxTh.Text);
                mCTi = int.Parse(TxCTi.Text);
                mTTi = int.Parse(TxTTi.Text);
                //if (mLb < 0 || mLb > 255 || mTb < 0 || mTb > 255) throw new Exception("mLb, mTb < 100");
                //if (mRb < 0 || mRb > 255) throw new Exception();
                //if (mTw < 0 || mTw > 255 || mTh < 0 || mTh > 255 || mCTi < 0 || mCTi > 255) throw new Exception("Target bound error");
                //if (mRows < 1 || mLines < 2 || mLines > 6) throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //errScanning(1);
                return false;
            }
            return true;
        }

        private Rectangle[] getTargetRect()
        {
            //int lineNum = 0;
            int mode = modeList.SelectedIndex;
            Rectangle[] rect = null;
            rect = new Rectangle[mRows * mLines];
            int leftBound = mLb;
            int rightBound = mRb;
            int topBound = mTb;
            double rowInterval = 0;
            if (mRows > 1)
                rowInterval = (double)(rightBound - leftBound - mTw * mRows) / (mRows - 1);
            for (int i = 0; i < mRows; i++)
                for (int j = 0; j < mLines; j++)
                {   
                    if (mode == 0 && j == 4)
                    {
                        rect[i * mLines + j] = new Rectangle(
                        leftBound + (int)((mTw + rowInterval) * i),
                        topBound + mCTi * 2 + mTTi * (j - 2),
                        mTw,
                        mTh);
                        continue;
                    }
                    if (j <= 1)
                    {
                        rect[i * mLines + j] = new Rectangle(
                            leftBound + (int)((mTw + rowInterval) * i),
                            topBound + mCTi * j,
                            mTw,
                            mTh);
                    }
                    else
                    {
                        rect[i * mLines + j] = new Rectangle(
                            leftBound + (int)((mTw + rowInterval) * i),
                            topBound + mCTi + mTTi * (j - 1),
                            mTw,
                            mTh);
                    }
                }

            return rect;
        }

        private void modeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (modeList.SelectedIndex)
            {
                case 0:
                    mRows = 1;
                    mLines = 5;
                    break;
                case 1:
                    mRows = 2;
                    mLines = 4;
                    break;
                default:
                    mRows = 1;
                    mLines = 5;
                    break;
            }
            pictureBox2.Image = drawTargetZoneOnImage(mImage);
        }
    }

}
