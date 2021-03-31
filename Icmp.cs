using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracert
{
    class Icmp
    {
        public byte Type;
        public byte Code;
        public ushort CheckSum;
        public byte[] Message = new byte[1024];
        public int MessageSize;

        public Icmp(byte[] data, int size)
        {
            Type = data[20];
            Code = data[21];
            CheckSum = BitConverter.ToUInt16(data, 22);
            MessageSize = size - 24;
            Buffer.BlockCopy(data, 24, Message, 0, MessageSize);
        }

        public Icmp()
        {
            Type = 8;
        }

        public byte[] GetPacketBytes()
        {
            byte[] data = new byte[MessageSize + 4];
            Buffer.BlockCopy(BitConverter.GetBytes(Type), 0, data, 0, 1);
            Buffer.BlockCopy(BitConverter.GetBytes(Code), 0, data, 1, 1);
            Buffer.BlockCopy(BitConverter.GetBytes(CheckSum), 0, data, 2, 2);
            Buffer.BlockCopy(Message, 0, data, 4, MessageSize);
            return data;
        }

        //Вычисление контрольной суммы (RFC 1071)
        public void GetCheckSum()
        {
            long checkSum = 0;
            byte[] data = GetPacketBytes();
            int index = 0, count = MessageSize;

            while (count > 1)
            {
                checkSum += Convert.ToUInt32(BitConverter.ToUInt16(data, index));
                count -= 2;
                index += 2;
            }
            if (count > 0)
                checkSum += Convert.ToUInt32(BitConverter.ToUInt16(data, index));
            while (checkSum >> 16 != 0)
                checkSum = (checkSum & 0xffff) + (checkSum >> 16);

            CheckSum = (ushort)(~checkSum);
        }

    }
}

