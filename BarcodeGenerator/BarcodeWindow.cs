using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace BarcodeGenerator
{
    public partial class BarcodeWindow : Form
    {
        private Image mImage;
        public BarcodeWindow(Image image)
        {
            InitializeComponent();
            mImage = image;
            pictureBox1.Image = mImage;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mImage.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }

    }
}
