/*
 * File: KwQRCodeReader.cs
 * Company: Kaiwood
 * Description: QRCode decoder with COM Interop 
 * Version: v.1.0
 * Author: Herman Liang
 * E-Mail: herman@kaiwood.com.tw
 */
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;

namespace KwBarcode
{
    [Guid("F7872A5E-02C9-4DA9-8CE5-A40A82DB463D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface QRInterface
    {
        string FilePath { get; set; }
        string Text { get; }
        byte[] RawByte { get; }
        void decode();
    }

    [Guid("3E73EE86-F46D-411D-BE6F-87060B7E6E6A")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("KwQRCodeReader")]
    public class KwQRCodeReader : QRInterface
    {
        private string filePath = "";
        private string text = "";
        private int counter = 0;
        byte[] rawByte = null;

        public KwQRCodeReader()
        {
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
            try
            {
                Bitmap image = new Bitmap(filePath);
                Result result = ProcessQRReader(image);
                text = result.Text;
                rawByte = new byte[result.RawBytes.Length];
                for (int i = 0; i < result.RawBytes.Length; i++)
                {
                    rawByte[i] = (byte)result.RawBytes[i];
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
            QRCodeReader reader = new QRCodeReader();
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
    }
}
