using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KwBarcode;

namespace NuCloverBarcode
{
    public partial class NuCloverDecoder : Form
    {
        public NuCloverDecoder()
        {
            InitializeComponent();
        }

        private void NuCloverDecoder_Load(object sender, EventArgs e)
        {
            Bitmap casImg = Properties.Resources.CasCode;
            Bitmap lotImg1 = Properties.Resources.KwDumpA;
            Bitmap lotImg2 = Properties.Resources.KwDumpB;
            KwQRCodeReader reader = new KwQRCodeReader();
            reader.decode(casImg);
            String casMsg = reader.Text;
            String[] casHexArr = getHexArray(casMsg, null);
            String casPrefix = casHexArr[0];

            reader.decode(lotImg1);
            String lotMsg1 = reader.Text;
            reader.decode(lotImg2);
            String lotMsg2 = reader.Text;
            String lotMsg = lotMsg1.Substring(2) + lotMsg2.Substring(2);
            
            
            if (casPrefix.Substring(0, 1).Equals("C"))
            {
                int clientIndex = Convert.ToInt32(casHexArr[1], 16);
                double chipWidth = (double)Convert.ToInt32(casHexArr[2], 16) + double.Parse(casHexArr[3]) / 10.0;
                double chipHeight = (double)Convert.ToInt32(casHexArr[4], 16) + double.Parse(casHexArr[5]) / 10.0;
                double stdWidth = (double)Convert.ToInt32(casHexArr[6], 16) / 10.0;
                double cLinePos = (double)Convert.ToInt32(casHexArr[7], 16) / 10.0;
                double tcSpace = (double)Convert.ToInt32(casHexArr[8], 16) / 10.0;
                double wholeRange = (double)Convert.ToInt32(casHexArr[9], 16) + double.Parse(casHexArr[10]) / 10.0;
                int value = Convert.ToInt32(casHexArr[11], 16);
                int type = value / 100;
                int moveDir = (value - type * 100) / 10;
                int sampleNo = value % 10;
                int invalid = Convert.ToInt32(casHexArr[12], 16);
                double moveX = (double)Convert.ToInt32(casHexArr[13], 16) / 10.0;
                double moveY = (double)Convert.ToInt32(casHexArr[14], 16) / 10.0;
            }
            String fieldValue = getFieldText(lotMsg, "60");
            String[] prodHexArr = getHexArray(fieldValue, null);
            byte[] prodByteArr = hex2byteArr(prodHexArr);
            Encoding encoder = Encoding.Default;
            String prodCode = encoder.GetString(prodByteArr);
            
            lotMsg = lotMsg.Substring(fieldValue.Length + 2);
            fieldValue = getFieldText(lotMsg, "60");
            prodHexArr = getHexArray(fieldValue, null);
            prodByteArr = hex2byteArr(prodHexArr);
            String prodLot = encoder.GetString(prodByteArr);

            lotMsg = lotMsg.Substring(fieldValue.Length + 2);
            fieldValue = getFieldText(lotMsg, "605E");
            prodHexArr = getHexArray(fieldValue, null);
            prodByteArr = hex2byteArr(prodHexArr);
            String prodID = encoder.GetString(prodByteArr);

            lotMsg = lotMsg.Substring(fieldValue.Length + 4);
            String[] lotHexArr = getHexArray(lotMsg, null);

            int v1 = Convert.ToInt32(lotHexArr[0], 16);
            int expYY = 2010 + ((v1 >> 4) & 0x0F);
            int expMM = v1 & 0x0F;
            v1 = Convert.ToInt32(lotHexArr[1], 16);
            int expDD = v1 % 50;
            int items = v1 / 50;
            v1 = Convert.ToInt32(lotHexArr[2], 16);
            int unitType = v1 / 10;
            int expNo = v1 % 10;
            String[] itemNames = new String[items];
            double[,] RLU = new double[items, expNo];
            double[,] Conc = new double[items, expNo];
            for (int i = 0; i < items; i++)
            {
                int idx1 = (i + i * expNo) * 4 + 3;
                byte[] nameByte = hex2byteArr(getArrRange(lotHexArr, idx1, 4));
                itemNames[i] = (encoder.GetString(nameByte)).Replace("\0","");
                for (int j = 0; j < expNo; j++)
                {
                    int tempV1 = int.Parse(lotHexArr[j * 4 + idx1 + 4]);
                    int tempV2 = int.Parse(lotHexArr[j * 4 + idx1 + 5].Substring(0, 1));
                    int multi1 = Convert.ToInt32(lotHexArr[j * 4 + idx1 + 5], 16) & 0x0F;
                    multi1 = multi1 > 9 ? (-1) * multi1 + 9 : multi1;
                    int tempV3 = int.Parse(lotHexArr[j * 4 + idx1 + 6]);
                    int tempV4 = int.Parse(lotHexArr[j * 4 + idx1 + 7].Substring(0, 1));
                    int multi2 = Convert.ToInt32(lotHexArr[j * 4 + idx1 + 7], 16) & 0x0F;
                    multi2 = multi2 > 9 ? (-1) * multi2 + 9 : multi2;
                    RLU[i, j] = (double)tempV1 / 10.0 + (double)tempV2 / 100.0;
                    RLU[i, j] *= Math.Pow(10, multi1);
                    Conc[i, j] = (double)tempV3 / 10.0 + (double)tempV4 / 100.0;
                    Conc[i, j] *= Math.Pow(10, multi2);
                }
            }
        }

        private String[] getArrRange(String[] hexArr, int startIndex, int length)
        {
            String[] retString = new String[length];
            for (int i = 0; i < length; i++)
            {
                retString[i] = hexArr[i + startIndex];
            }
            return retString;
        }

        private String[] getHexArray(String hexText, List<String> hexArray)
        {
            if (hexArray == null) hexArray = new List<string>();
            if (hexText.Equals(""))
                return hexArray.ToArray();
            else
            {
                hexArray.Add(hexText.Substring(0, 2));
                return getHexArray(hexText.Substring(2), hexArray);
            }
        }

        private byte[] hex2byteArr(String[] hexArr)
        {
            byte[] retArr = new byte[hexArr.Length];
            for (int i = 0; i < hexArr.Length; i++)
            {
                retArr[i] = (byte)Convert.ToInt32(hexArr[i], 16);
            }
            return retArr;
        }

        private String getFieldText(String hexString, String divider)
        {
            if (hexString.Equals("") || hexString.Substring(0, divider.Length).Equals(divider))
            {
                return "";
            }
            else
            {   
                return hexString.Substring(0, 2) + getFieldText(hexString.Substring(2), divider);
            }
        }
    }
}
