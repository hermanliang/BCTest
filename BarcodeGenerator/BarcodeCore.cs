using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodeGenerator
{
    public class BarcodeCore
    {
        public static string numberText = " 0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static void BarcodeEncoder(int[] bit, int[] values, out List<int[]> format, out List<string> barcodes)
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
                    code = code | ((long)values[i] & mask) << (singleBlockSum - bit[i]);
                    barcodes.Add(code.ToString("0000000000"));
                    code = 0;
                    code = code | ((long)values[i] >> last);
                    singleBlock.Clear();
                    singleBlock.Add(dev);
                    singleBlockSum = dev;
                }
                else if (singleBlockSum == 32)
                {
                    code = code | ((long)values[i] << (singleBlockSum - bit[i]));
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
                    code = code | ((long)values[i] << (singleBlockSum - bit[i]));
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
            int value = 0;
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
                            int tempV = (int)((code >> (singleBlockSum - bit)) & mask);
                            value = value | (tempV << last);
                            v1.Add(value);
                            f1.Add(bit + last);
                            sum += value;
                        }
                        else
                        {
                            value = (int)((code >> (singleBlockSum - bit)) & mask);
                            v1.Add(value);
                            f1.Add(bit);
                            sum += value;
                        }
                    }
                    else if (singleBlockSum == 32)
                    {
                        value = (int)((code >> (singleBlockSum - bit)) & mask);
                        last = bit;
                        singleBlockSum = 0;
                        if (i == barcodes.Count - 1 && j == size.Length - 1)
                        {
                            v1.Add(value);
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

        public static bool validValue(int value, int bit)
        {
            
            if (value > Math.Pow(2, bit) - 1)
            {
                return false;
            }
            return true;
        }
    }
}
