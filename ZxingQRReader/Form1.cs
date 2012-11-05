using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KwBarcode;

namespace ZxingQRReader
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        KwQRCodeReader reader = new KwQRCodeReader();
        KwQRCodeWriter writer = new KwQRCodeWriter();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {   
                Bitmap image = new Bitmap(openFileDialog1.FileName);
                reader.decode(image);
                txResult.Text = reader.Text;
                pictureBox2.Image = image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                writer.encodeAndSave(textBox1.Text, saveFileDialog1.FileName);
            }
        }
    }
}
