using System;

namespace HTOL.Packets
{
    public abstract class UserData
    {
        private byte[] data;

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        public Int32 this[UInt32 offset]
        {
            get
            {
                if (null == data || data.Length < offset + 4)
                {
                    Console.WriteLine("get: data buffer overflow!!! @ " + offset);
                    return 0;
                }
                return BitConverter.ToInt32(data, (int)offset);
            }
            set
            {
                if (null != data && data.Length >= offset + 4)
                    BitConverter.GetBytes(value).CopyTo(data, offset);
                else
                    Console.WriteLine("set: data buffer overflow!!! @ " + offset);
            }
        }
    }
}