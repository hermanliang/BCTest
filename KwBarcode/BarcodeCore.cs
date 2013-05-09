/*
 * File: BarcodeCore.cs
 * Company: Kaiwood
 * Description: Barcode encoder and decoder with COM Interop 
 * Version: v.1.0
 * Author: Herman Liang
 * E-Mail: herman@kaiwood.com.tw
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KwBarcode
{
    /// <summary>
    /// COM Interop Interface
    /// </summary>
    [Guid("2E496F0F-22D0-4CEE-AD35-49142EE08D00")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface mInterface
    {
        string[] Barcodes { get; }
        int[] inValues { get; }
        int[] outValues { get; }
        int[] Bits { get; }
        
        void addBit(int bit);
        void addValue(int value);
        void addBarcode(string barcode);
        bool setBarcodes(string barcodeStrings);
        int textToInt(string text);
        string intToText(int value);
        int text128ToInt(string text);
        string intToText128(int value);
        int floatToInt(float value);
        float intToFloat(int value);
        void initBarcodeEncoder();
        void initBarcodeDecoder();
    }

    [Guid("7C6D4FCD-073B-4585-943F-BCF0BB6404FD")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("BarcodeCore")]
    public class BarcodeCore : mInterface
    {
        public static string numberText = " 0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        #region COM Interop Members

        private List<string> mBarcodes;
        private Dictionary<char, int> textTable = new Dictionary<char, int>();
        private List<int> mBitsList;
        private List<long> mValuesList;
        private int[] mDecValues;        

        public BarcodeCore()
        {
            mBitsList = new List<int>();
            mValuesList = new List<long>();
            mBarcodes = new List<string>();
            textTable = getTextTable();
        }

        public void addBit(int bit)
        {
            mBitsList.Add(bit);
        }

        public void addValue(int value)
        {
            long v1 = value & 0xFFFFFFFF;
            mValuesList.Add(v1);
        }

        public void addBarcode(string barcode)
        {
            mBarcodes.Add(barcode);
        }

        public bool setBarcodes(string barcodeString)
        {
            if (barcodeString.Length % 10 != 0 || barcodeString.Length == 0)
                return false;
            try
            {
                mBarcodes = getBarcodeLists(barcodeString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void initBarcodeEncoder()
        {   
            mBarcodes = BarcodeEncoder(mBitsList.ToArray(), mValuesList.ToArray());
        }

        public void initBarcodeDecoder()
        {
            List<int[]> formats = getFormatData(mBitsList.ToArray());            
            mDecValues = BarcodeDecoder(mBarcodes, formats);
        }

        public string[] Barcodes
        {
            get { return mBarcodes.ToArray(); }
        }

        public int[] outValues
        {
            get { return mDecValues; }
        }

        public int[] inValues
        {
            get
            {
                if (mValuesList.Count == 0)
                    return null;
                long[] values = mValuesList.ToArray();
                int[] ret = new int[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    byte[] lBytes = BitConverter.GetBytes(values[i]);
                    ret[i] = BitConverter.ToInt32(lBytes, 0);
                }
                return ret;
            }
        }

        public int[] Bits
        {
            get { return mBitsList.ToArray(); }
        }

        public int textToInt(string text)
        {
            return (int)(TextToLong(text) & 0xFFFFFFFF);
        }

        public string intToText(int value)
        {
            return IntToText(value);
        }

        public int text128ToInt(string text)
        {
            return (int)(Text128ToLong(text) & 0xFFFFFFFF);
        }

        public string intToText128(int value)
        {
            return IntToText128(value);
        }

        public int floatToInt(float value)
        {
            return (int)(FloatToLong(value) & 0xFFFFFFFF);
        }

        public float intToFloat(int value)
        {
            return IntToFloat(value);
        }

        #endregion

        #region Static Methods

        public static void BarcodeEncoder(int[] bit, long[] values, out List<int[]> format, out List<string> barcodes)
        {
            format = new List<int[]>();
            barcodes = new List<string>();
            if (bit.Length != values.Length)
                goto Exit;

            int singleBlockSum = 0;
            long code = 0;
            List<int> singleBlock = new List<int>();
            long sum = 0;

            for (int i = 0; i < bit.Length; i++)
            {
                if (!validValue(values[i], bit[i]))
                    goto Exit;

                singleBlockSum += bit[i];
                sum += values[i];
                if (singleBlockSum > 32)
                {
                    int dev = singleBlockSum - 32;
                    int last = bit[i] - dev;
                    singleBlock.Add(last);
                    format.Add(singleBlock.ToArray());
                    long mask = (long)Math.Pow(2, last) - 1;
                    code = code | (values[i] & mask) << (singleBlockSum - bit[i]);
                    barcodes.Add(code.ToString("0000000000"));
                    code = 0;
                    code = code | (values[i] >> last);
                    singleBlock.Clear();
                    singleBlock.Add(dev);
                    singleBlockSum = dev;
                }
                else if (singleBlockSum == 32)
                {
                    code = code | (values[i] << (singleBlockSum - bit[i]));
                    singleBlock.Add(bit[i]);
                    format.Add(singleBlock.ToArray());
                    singleBlock.Clear();
                    singleBlock.Add(0);
                    singleBlockSum = 0;
                    barcodes.Add(code.ToString("0000000000"));
                    code = 0;
                }
                else
                {
                    code = code | (values[i] << (singleBlockSum - bit[i]));
                    singleBlock.Add(bit[i]);
                }

            }

            byte checksum = (byte)(sum % 256);
            singleBlockSum += 8;
            if (singleBlockSum > 32)
            {
                int dev = singleBlockSum - 32;
                int last = 8 - dev;
                singleBlock.Add(last);
                long mask = (long)Math.Pow(2, last) - 1;
                code = code | ((long)checksum & mask) << (singleBlockSum - 8);
                barcodes.Add(code.ToString("0000000000"));
                format.Add(singleBlock.ToArray());

                code = 0;
                singleBlock.Clear();
                singleBlock.Add(dev);
                code = code | ((long)checksum >> last);
                format.Add(singleBlock.ToArray());
                barcodes.Add(code.ToString("0000000000"));
            }
            else
            {
                singleBlock.Add(8);
                format.Add(singleBlock.ToArray());
                code = code | ((long)checksum << (singleBlockSum - 8));
                barcodes.Add(code.ToString("0000000000"));
                singleBlock.Clear();
            }

            return;

        Exit:
            barcodes = null;
            format = null;
            return;

        }

        public static List<string> BarcodeEncoder(int[] bit, long[] values)
        {
            List<int[]> oFormat;
            List<string> barcodes;
            BarcodeEncoder(bit, values, out oFormat, out barcodes);
            return barcodes;
        }

        public static void BarcodeDecoder(List<string> barcodes, List<int[]> format, out int[] values, out int[] oriFormat)
        {
            values = null;
            oriFormat = null;
            if (barcodes.Count != format.Count)
                goto Exit;

            List<int> v1 = new List<int>();
            List<int> f1 = new List<int>();
            int i, j;
            int singleBlockSum = 0;
            long value = 0;
            long mask;
            long code;
            long sum = 0;
            int last = 0;
            for (i = 0; i < barcodes.Count; i++)
            {
                string barcode = barcodes[i];
                if (!long.TryParse(barcode, out code))
                    goto Exit;
                int[] size = format[i];
                for (j = 0; j < size.Length; j++)
                {
                    int bit = size[j];
                    mask = (long)Math.Pow(2, bit) - 1;
                    singleBlockSum += bit;
                    if (singleBlockSum != 32)
                    {
                        if (i != 0 && j == 0)
                        {
                            long tempV = (code >> (singleBlockSum - bit) & mask);
                            value = value | (tempV << last);
                            v1.Add(BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
                            f1.Add(bit + last);
                            sum += value;
                        }
                        else
                        {
                            value = (int)((code >> (singleBlockSum - bit)) & mask);
                            v1.Add(BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
                            f1.Add(bit);
                            sum += value;
                        }
                    }
                    else if (singleBlockSum == 32)
                    {
                        value = (code >> (singleBlockSum - bit) & mask);
                        last = bit;
                        singleBlockSum = 0;
                        if (i == barcodes.Count - 1 && j == size.Length - 1)
                        {
                            v1.Add(BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
                            f1.Add(bit);
                            sum += value;
                        }
                    }
                }
            }
            int checksum = v1[v1.Count - 1];
            sum -= checksum;
            if (sum % 256 != checksum)
                goto Exit;


            values = v1.ToArray();
            oriFormat = f1.ToArray();
            return;

        Exit:
            values = null;
            oriFormat = null;
            return;

        }

        public static int[] BarcodeDecoder(List<string> barcodes, List<int[]> format)
        {
            int[] values = null;
            int[] oriFormat = null;
            BarcodeDecoder(barcodes, format, out values, out oriFormat);
            return values;
        }

        public static int[] BarcodeDecoder(string barcodes, int[] bitsWithoutChecksum)
        {
            int[] values = null;

            try
            {
                List<int[]> format = getFormatData(bitsWithoutChecksum);
                List<string> BCs = getBarcodeLists(barcodes);
                values = BarcodeDecoder(BCs, format);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return values;
        }

        public static List<int[]> getFormatData(int[] bit)
        {
            List<int[]> format = new List<int[]>();
            List<int> singleBlock = new List<int>();
            int singleBlockSum = 0;

            for (int i = 0; i < bit.Length; i++)
            {
                singleBlockSum += bit[i];
                if (singleBlockSum > 32)
                {
                    int dev = singleBlockSum - 32;
                    int last = bit[i] - dev;
                    singleBlock.Add(last);
                    format.Add(singleBlock.ToArray());
                    singleBlock.Clear();
                    singleBlock.Add(dev);
                    singleBlockSum = dev;
                }
                else if (singleBlockSum == 32)
                {
                    singleBlock.Add(bit[i]);
                    format.Add(singleBlock.ToArray());
                    singleBlock.Clear();
                    singleBlock.Add(0);
                    singleBlockSum = 0;
                }
                else
                {
                    singleBlock.Add(bit[i]);
                }
            }
            singleBlockSum += 8;
            if (singleBlockSum > 32)
            {
                int dev = singleBlockSum - 32;
                int last = 8 - dev;
                singleBlock.Add(last);
                format.Add(singleBlock.ToArray());

                singleBlock.Clear();
                singleBlock.Add(dev);
                format.Add(singleBlock.ToArray());
            }
            else
            {
                singleBlock.Add(8);
                format.Add(singleBlock.ToArray());
                singleBlock.Clear();
            }
            return format;
        }

        public static List<string> getBarcodeLists(string barcodes)
        {
            List<string> oBarcodes = new List<string>();
            if (barcodes.Length % 10 != 0 || barcodes.Length == 0)
                throw new Exception("Barcode length error!");

            for (int i = 0; i < barcodes.Length / 10; i++)
            {
                oBarcodes.Add(barcodes.Substring(i * 10, 10));
            }
            return oBarcodes;
        }

        public static bool validValue(long value, int bit)
        {

            if (value > Math.Pow(2, bit) - 1)
            {
                return false;
            }
            return true;
        }

        public static long TextToLong(string text)
        {
            long ret = 0;
            if (text.Length > 4)
            {
                text = text.Substring(0, 4);
            }
            int textIdx;
            Dictionary<char, int> table = getTextTable();
            for (int j = 0; j < text.Length; j++)
            {
                if (table.TryGetValue(text[j], out textIdx))
                {
                    ret = ret | ((long)textIdx << j * 6);
                }
                else
                {
                    ret = ret | (0L << j * 6);
                }
            }
#if DEBUG
            byte[] fByte = BitConverter.GetBytes(ret);
#endif
            return ret;
        }

        public static string IntToText(int value)
        {
            string word = "";
            int mask = 0x3F;
            for (int j = 0; j < 4; j++)
            {
                int idx = (value >> 6 * j) & mask;
                word += numberText[idx];
            }
            return word;
        }

        public static long Text256ToLong(string text)
        {
            long ret = 0;
            byte[] textBytes = Encoding.ASCII.GetBytes(text);
            int maxIndex = textBytes.Length < 4 ? textBytes.Length : 4;
            for (int i = 0; i < 4; i++)
            {
                if (i >= maxIndex)
                {
                    ret = ret | (32L << i * 8);
                }
                else
                {
                    ret = ret | ((long)textBytes[i] << i * 8);
                }
            }
            return ret;
        }

        public static string IntToText256(int value)
        {
            Encoding encoder = Encoding.ASCII;
            string word = "";
            byte[] intBytes = BitConverter.GetBytes(value);
            word = encoder.GetString(intBytes);
            return word;
        }

        /// <summary>
        /// 轉換 7-bit (英數字含標點) 為整數，最大為 4 個字 (28-bit)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static long Text128ToLong(string text)
        {
            long ret = 0;
            if (text.Length > 4)
            {
                text = text.Substring(0, 4);
            }
            Encoding encoder = Encoding.ASCII;
            byte[] codes = encoder.GetBytes(text);
            if (codes.Length > 4)
            {
                return 32; // space
            }

            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i] < 128)
                {
                    ret = ret | ((long)codes[i] << i * 7);
                }
                else
                {
                    ret = ret | (0L << i * 7);
                }
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToText128(int value)
        {
            Encoding encoder = Encoding.ASCII;
            string word = "";
            int mask = 0x7F;
            for (int i = 0; i < 4; i++)
            {
                byte idx = (byte)((value >> 7 * i) & mask);
                if (idx != 0)
                {
                    word += encoder.GetString(new byte[] { idx });
                }                
            }
            return word;
        }

        public static long FloatToLong(float value)
        {   
            byte[] fByte = BitConverter.GetBytes(value);
            long ret = BitConverter.ToUInt32(fByte, 0);            
            return ret;
        }

        public static float IntToFloat(int value)
        {
            byte[] iByte = BitConverter.GetBytes(value);
            float ret = BitConverter.ToSingle(iByte, 0);
            return ret;
        }

        public static Dictionary<char, int> getTextTable()
        {
            Dictionary<char, int> table = new Dictionary<char, int>();
            for (int i = 0; i < numberText.Length; i++)
            {
                table[numberText[i]] = i;
            }
            return table;
        }

        static public byte[] float16ToBytes(double value)
        {
            ushort v = EncodeFloat16(value);
            return BitConverter.GetBytes(v);
        }

        static public ushort EncodeFloat16(double value)
        {
            int cnt = 0;
            while (value != Math.Round(value))
            {
                if (value * 10 >= 4095) break;
                value *= 10;
                cnt ++;
                if (cnt == 15) break;
            }
            //while (Math.Round(value * 8) < 4096 && value != 0)
            //{
            //    value *= 8;
            //    cnt++;
            //}
            return (ushort)((cnt << 12) + (int)Math.Round(value));
        }

        static public double DecodeFloat16(ushort value)
        {
            int cnt = value >> 12;
            double result = value & 0xfff;
            while (cnt > 0)
            {
                result /= 10;
                //result /= 8;
                cnt--;
            }
            return result;
        }

        static public int bytes2Uint16(byte[] dataBytes, int startIndex)
        {
            int retValue = 0;
            retValue = retValue | (dataBytes[startIndex] & 0xff) | ((dataBytes[startIndex + 1] << 8) & 0xff00);
            return retValue;
        }

        #endregion
    }
}
