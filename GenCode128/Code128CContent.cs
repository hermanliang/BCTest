using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenCode128
{
    public class Code128CContent
    {
        #region Constants

        private const int cSHIFT = 98;
        private const int cCODEA = 101;
        private const int cCODEB = 100;

        private const int cSTARTA = 103;
        private const int cSTARTB = 104;
        private const int cSTARTC = 105;
        private const int cSTOP = 106;

        #endregion

        private int[] mCodeList;

        public int[] Codes
        {
            get { return mCodeList; }
        }

        public Code128CContent(string AsciiData)
        {
            mCodeList = StringToCode128(AsciiData);
        }

        private int[] StringToCode128(string AsciiData)
        {
            if (AsciiData.Length % 2 == 1)
            {
                AsciiData = "0" + AsciiData;
            }
            ArrayList codes = new ArrayList(AsciiData.Length / 2 + 3);
            codes.Add(cSTARTC);
            for (int i = 0; i < AsciiData.Length / 2; i++)
            {
                codes.Add(int.Parse(AsciiData.Substring(i * 2, 2)));
            }
            int checksum = (int)codes[0];
            for (int i = 1; i < codes.Count; i++)
            {
                checksum += i * (int)codes[i];
            }
            codes.Add(checksum % 103);
            codes.Add(cSTOP);

            int[] result = codes.ToArray(typeof(int)) as int[];
            return result;
        }
    }
}
