using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;
using com.google.zxing.qrcode.decoder;

namespace KwBarcode
{
    [Guid("69F37639-F632-433F-AA01-1BC326FD1D6F")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("KwQRCodeWriter")]
    public class KwQRCodeWriter:IQRCode
    {
        private QRCodeWriter writer;
        private string filePath = "";
        private string text = "";
        byte[] rawByte = null;
        private int size = 256;

        public KwQRCodeWriter()
        {
            writer = new QRCodeWriter();
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Text
        {
            get { return text; }
        }

        public byte[] RawByte
        {
            get { return rawByte; }
        }

        public void encodeAndSave(string text, string path)
        {
            try
            {
                this.filePath = path;
                textToQRImage(text).Save(path, ImageFormat.Bmp);                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Bitmap textToQRImage(string text)
        {
            try
            {
                ByteMatrix byteMatrix = writer.encode(
                        text,
                        BarcodeFormat.QR_CODE,
                        this.size,
                        this.size);

                return byteMatrix.ToBitmap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
