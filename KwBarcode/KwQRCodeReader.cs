/*
 * File: KwQRCodeReader.cs
 * Company: Kaiwood
 * Description: QRCode decoder with COM Interop 
 * Version: v.1.0
 * Author: Herman Liang
 * E-Mail: herman@kaiwood.com.tw
 */
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;
using com.google.zxing.oned;

namespace KwBarcode
{
    [Guid("90A019A1-D080-4331-B20F-AD4504236398")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IQRReader
    {
        string FilePath { get; set; }
        byte[] RawByte { get; }
        string Text { get; }
        void decode();
        void decode(Bitmap image);
    }


    [Guid("3E73EE86-F46D-411D-BE6F-87060B7E6E6A")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KwQRCodeReader")]
    public class KwQRCodeReader : IQRReader
    {
        private Code128Reader reader;
        private string filePath = "";
        private string text = "";
        private int counter = 0;
        byte[] rawByte = null;

        public KwQRCodeReader()
        {
            reader = new Code128Reader();
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public string Text
        {
            get { return text; }
        }

        public byte[] RawByte
        {
            get { return rawByte; }
        }

        public void decode()
        {
            //Bitmap image = new Bitmap(filePath);
            StreamReader reader = new StreamReader(filePath);
            Bitmap image = new Bitmap(reader.BaseStream);
            reader.Close();
            decode(image);
        }

        public void decode(Bitmap image)
        {
            text = "";
            rawByte = null;
            try
            {
                Result result = ProcessQRReader(image);
                text = result.Text;

                ICollection keyColl = result.ResultMetadata.Keys;
                foreach (object key in keyColl)
                {
                    object v1 = result.ResultMetadata[key];
                    if (v1.GetType().Equals(typeof(string)))
                        continue;
                    ArrayList data = (ArrayList)v1;
                    this.rawByte = (byte[])data[0];
                    Console.WriteLine(key);
                    break;
                }
            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine(fnfex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private Bitmap paddingImage(Bitmap image, int padSize)
        {
            Bitmap padImage = new Bitmap(image.Width + padSize * 2, image.Height + padSize * 2);
            Graphics g1 = Graphics.FromImage(padImage);
            Brush brush = Brushes.White;
            g1.FillRectangle(brush, 0, 0, padImage.Width, padImage.Height);
            g1.DrawImage(image, padSize, padSize);
            return padImage;
        }
        
        private Result ProcessQRReader(Bitmap image)
        {
            //image = preProcessImage(image);
            //image = new Bitmap("d:\\AMA_QR.bmp");
            LuminanceSource ls = new RGBLuminanceSource(image, image.Width, image.Height);
            Binarizer hb = new HybridBinarizer(ls);
            Result result = null;
            BinaryBitmap bbitmap = new BinaryBitmap(hb);
            try
            {
                result = reader.decode(bbitmap);
            }
            catch (ReaderException rex)
            {
                Console.WriteLine(rex.Message);
                if (counter == 0)
                {
                    image = paddingImage(image, 10);
                    counter = 1;
                    return ProcessQRReader(image);
                }
                else
                {
                    counter = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            counter = 0;
            return result;
        }

        private Bitmap preProcessImage(Bitmap image)
        {
            if (image == null)
                return null;
            int minLen = int.MaxValue;
            minLen = minLen > image.Height ? image.Height : minLen;
            minLen = minLen > image.Width ? image.Width : minLen;
            //if (minLen > 256)
            //{
                image = new Bitmap(
                    image,
                    new Size(
                        (int)((double)image.Width / minLen * 256),
                        (int)((double)image.Height / minLen * 256)
                    )
                );
            //}
            return image;
        }
    }
}
