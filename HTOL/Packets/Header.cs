using System;
using System.Collections.Generic;
using System.Linq;

namespace HTOL.Packets
{
    public class Header : UserData
    {
        public const Int32 Length = 56;

        public Header()
        {
            this.Data = new byte[Length];
        }

        public Header(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Identify
        {
            get 
            {
                byte[] identify = new byte[32];
                for (int i = 0; i < identify.Length; i++)
                {
                    identify[i] = this.Data[i];
                }
                return identify;
            }
            set
            {
                if (value.Length != 32)
                {
                    Console.WriteLine("Set Identify Length != 32"); 
                    return;
                }
                    
                for (int i = 0; i < value.Length; i++)
                {
                    this.Data[i] = value[i];
                }
            }
        }

        // pkt Header
        public Int32 PktSN
        {
            get { return this[32]; }
            set { this[32] = value; }
        }

        public Int32 TotalMsgLen
        {
            get { return this[36]; }
            set { this[36] = value; }
        }

        public Int32 MsgSn
        {
            get { return this[40]; }
            set { this[40] = value; }
        }

        public Int32 MsgType
        {
            get { return this[44]; }
            set { this[44] = value; }
        }

        public Int32 MsgLen
        {
            get { return this[48]; }
            set { this[48] = value; }
        }//msgLen后面实际数据域字段长度

        public Int32 Timeout
        {
            get { return this[52]; }
            set { this[52] = value; }
        }

        public byte[] GetIdentify()
        {
            List<byte> identify = new List<byte>();
            foreach (var item in Enumerable.Range(0x10, 32))
            {
                identify.Add((byte)item);
            }

            return identify.ToArray<byte>();
        }
    }
}