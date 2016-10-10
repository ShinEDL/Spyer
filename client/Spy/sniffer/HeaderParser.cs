
namespace Sniffer
{
    using System;

    public class HeaderParser
    {
        public static byte ToByte(byte[] datagram, int offset, int length)
        {
            return (byte) ToUInt(datagram, offset, length);
        }

        public static int ToInt(byte[] datagram, int offset, int length)
        {
            return (int) ToUInt(datagram, offset, length);
        }

        public static uint ToUInt(byte[] datagram, int offset, int length)
        {
            uint num = 0;
            for (int i = 0; i < length; i++)
            {
                int num3 = (offset + i) % 8;
                int index = ((offset + i) - num3) / 8;
                byte num5 = datagram[index];
                int num4 = num5 >> (7 - num3);
                num4 &= 1;
                if (num4 > 0)
                {
                    num += (uint) Math.Pow(2.0, (double) ((length - i) - 1));
                }
            }
            return num;
        }

        public static ushort ToUShort(byte[] datagram, int offset, int length)
        {
            return (ushort) ToUInt(datagram, offset, length);
        }
    }
}

