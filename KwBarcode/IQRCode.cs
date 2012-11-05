using System;
namespace KwBarcode
{
    interface IQRCode
    {
        string FilePath { get; set; }
        byte[] RawByte { get; }
        string Text { get; }
    }
}
