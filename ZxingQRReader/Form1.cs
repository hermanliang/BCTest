using System;
using System.IO;
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
        KwCode128Reader reader = new KwCode128Reader();
        KwQRCodeWriter writer = new KwQRCodeWriter();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Bitmap image = new Bitmap(openFileDialog1.FileName);
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                Bitmap image = new Bitmap(streamReader.BaseStream);
                streamReader.Close();
                reader.decode(image);
                //reader.FilePath = openFileDialog1.FileName;
                //reader.decode();
                
                txResult.Text = reader.Text;
                pictureBox2.Image = image;
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Encoding encoder = Encoding.ASCII;
                //string msgText = encoder.GetString(new byte[] { 0x01, 0x00, 0x02, 0x03 });

                writer.encodeAndSave(textBox1.Text, saveFileDialog1.FileName);

                //writer.encodeAndSave(
                //    "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" + // 320bit
                //    "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" + // 640
                //    "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" + // 960
                //    "0123456789"
                //    , saveFileDialog1.FileName);

                //writer.encodeAndSave(
                //    "ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ" +
                //    "ABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJABCDEFGHIJ" +
                //    "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghij"
                //    , saveFileDialog1.FileName);

                //writer.encodeAndSave(msgText, saveFileDialog1.FileName);
            }
        }
    }
}
