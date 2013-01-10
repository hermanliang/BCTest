using System;
using System.Runtime.InteropServices;

namespace KwBarcode
{
    interface IQRCode
    {
        [DispId(0x01)]
        string FilePath { get; set; }
        [DispId(0x02)]
        byte[] RawByte { get; }
        [DispId(0x03)]
        string Text { get; }
    }
}
