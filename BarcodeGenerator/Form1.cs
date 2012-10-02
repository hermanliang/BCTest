using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BarcodeGenerator
{
    public partial class Form1 : Form
    {
        private int[] dataSize = 
                        {
                            1,
                            2,
                            3,
                            4,
                            5,
                            6,
                            7,
                            8,
                            9,
                            10,
                            24
                        };
        
        private string[] sizeText = { 
                                "1 (0-1)",
                                "2 (0-3)",
                                "3 (0-7)",
                                "4 (0-15)",
                                "5 (0-31)",
                                "6 (0-63)",
                                "7 (0-127)",
                                "8 (0-255)",
                                "9 (0-511)",
                                "10 (0-1023)",
                                "24 (4英數字)"
                            };

        Dictionary<string, int> sizeTable = new Dictionary<string, int>();
        Dictionary<char, int> textTable = new Dictionary<char, int>();
        
        public Form1()
        {
            InitializeComponent();
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();            
            DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn tb2 = new DataGridViewTextBoxColumn();

            tb1.HeaderText = "Name";
            tb2.HeaderText = "Value";

            for (int i = 0; i < sizeText.Length; i++)
            {
                cbc.Items.Add(sizeText[i]);
                sizeTable[sizeText[i]] = dataSize[i];
            }

            for (int i = 0; i < BarcodeCore.numberText.Length; i++)
            {
                textTable[BarcodeCore.numberText[i]] = i;
            }

            cbc.HeaderText = "Size";
            
            dataGridView1.Columns.Add(tb1);
            dataGridView1.Columns.Add(cbc);
            dataGridView1.Columns.Add(tb2);
        }

        private void btnGen_Click(object sender, EventArgs e)
        {            
            List<int> data = new List<int>();
            List<int> size = new List<int>();

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                //DataGridViewComboBoxCell vcbc = (DataGridViewComboBoxCell)dataGridView1[1, i];
                //int index = vcbc.Items.IndexOf(vcbc.Value);
                string text, value;
                try
                {
                    text = dataGridView1[1, i].Value.ToString();
                    value = dataGridView1[2, i].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                int v = 0;
                int index;
                if (!sizeTable.TryGetValue(text, out index))
                {
                    MessageBox.Show("Error");
                    return;
                }
                else
                {
                    size.Add(index);
                    if (index != 24)
                    {
                        if (int.TryParse(value, out v))
                        {
                            data.Add(v);
                        }
                        else
                        {
                            MessageBox.Show("Error");
                            return;
                        }
                    }
                    else if (index == 24)
                    {
                        if (value.Length > 4)
                        {
                            MessageBox.Show("Error");
                            return;
                        }
                        int textIdx;
                        for (int j = 0; j < value.Length; j++)
                        {
                            if (textTable.TryGetValue(value[j], out textIdx))
                            {
                                v = v | (textIdx << j * 6);
                            }
                            else
                            {
                                MessageBox.Show("Error");
                                return;
                            }
                        }
                        data.Add(v);
                    }
                }
            }

            List<int[]> format;
            List<string> barcodes;
            BarcodeCore.BarcodeEncoder(size.ToArray(), data.ToArray(), out format, out barcodes);

            if (format == null)
            {
                MessageBox.Show("Error");
                return;
            }

            int[] deValues, deFormat;
            BarcodeCore.BarcodeDecoder(barcodes, format, out deValues, out deFormat);
            if (deValues == null)
            {
                MessageBox.Show("Error");
                return;
            }

            string decodeValue = "Barcodes:\r\n";
            foreach (string barcode in barcodes)
            {
                decodeValue += barcode + "\r\n";
            }
            decodeValue += "\r\nDecode Data:\r\n";
            for (int i = 0; i < deValues.Length; i++)
            {
                if (deFormat[i] == 24)
                {
                    string word = "";
                    int mask = 0x3F;
                    for (int j = 0; j < 4; j++)
                    {
                        int idx = (deValues[i] >> 6 * j) & mask;
                        word += BarcodeCore.numberText[idx];                        
                    }
                    decodeValue += word + "\r\n";
                }
                else
                {
                    decodeValue += deValues[i].ToString() + "\r\n";
                }
            }
            txInfo.Text = decodeValue;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8))
                {
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        string name = dataGridView1[0, i].Value.ToString();
                        DataGridViewComboBoxCell vcbc = (DataGridViewComboBoxCell)dataGridView1[1, i];
                        //int index = vcbc.Items.IndexOf(vcbc.Value);
                        string size = (string)vcbc.Value;
                        sw.WriteLine(name + "\t" + size);
                    }
                    sw.Flush();
                    sw.Close();
                }
                
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.UTF8))
                {
                    DataGridViewRowCollection rows = dataGridView1.Rows;
                    rows.Clear();
                    while (!sr.EndOfStream)
                    {
                        string text = sr.ReadLine();
                        string[] format = text.Split('\t');
                        if (format.Length != 2)
                            break;
                        rows.Add(new object[] {format[0], format[1] });
                    }

                }
            }
        }
    }
}
