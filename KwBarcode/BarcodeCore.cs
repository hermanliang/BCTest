using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KwBarcode
{
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
        int textToInt(string text);
        string intToText(int value);
        int floatToInt(float value);
        float intToFloat(int value);
        void initBarcodeEncoder();
        void initBarcodeDecoder();
    }

    [Guid("7C6D4FCD-073B-4585-943F-BCF0BB6404FD")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("BarcodeCore")]
    public class BarcodeCore : mInterface
    {
        public static string numberText = " 0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        #region COM Interop Members

        private List<int[]> mFormat;
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
            for (int i = 0; i < numberText.Length; i++)
            {
                textTable[numberText[i]] = i;
            }
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

        public void initBarcodeEncoder()
        {
            BarcodeEncoder(mBitsList.ToArray(), mValuesList.ToArray(), out mFormat, out mBarcodes);
        }

        public void initBarcodeDecoder()
        {
            List<int[]> formats = getFormatData(mBitsList.ToArray());
            int[] oriFormat;
            BarcodeDecoder(mBarcodes, formats, out mDecValues, out oriFormat);
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
            int ret = 0;
            if (text.Length > 4)
            {
                return 0;
            }
            int textIdx;
            for (int j = 0; j < text.Length; j++)
            {
                if (textTable.TryGetValue(text[j], out textIdx))
                {
                    ret = ret | (textIdx << j * 6);
                }
                else
                {
                    ret = ret | (0 << j * 6);
                }
            }
#if DEBUG
            byte[] fByte = BitConverter.GetBytes(ret);
#endif
            return ret;
        }

        public string intToText(int value)
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

        public int floatToInt(float value)
        {
            byte[] fByte = BitConverter.GetBytes(value);
            int ret = BitConverter.ToInt32(fByte, 0);
            return ret;
        }

        public float intToFloat(int value)
        {
            byte[] iByte = BitConverter.GetBytes(value);
            float ret = BitConverter.ToSingle(iByte, 0);
            return ret;
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

        public static bool validValue(long value, int bit)
        {

            if (value > Math.Pow(2, bit) - 1)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
