using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#region File Information

/* ----------------------------------------------------------------------
* File Name: Packet
* Create Author: ZDK
* Create DateTime: 2020/6/28 16:05:14
* Description:
*----------------------------------------------------------------------*/

#endregion File Information

namespace HTOL.Packets
{
    public class Packet
    {
        private static readonly Dictionary<Int32, Type> msgTypeMap = new Dictionary<int, Type>();

        static Packet()
        {
            Type type = typeof(Msg);

            Assembly ass = Assembly.GetAssembly(type);

            Type[] types = ass.GetTypes();

            foreach (var item in types)
            {
                foreach (var att in item.GetCustomAttributes(true))
                {
                    if (att is MsgTypeAttribute)
                    {
                        msgTypeMap.Add((att as MsgTypeAttribute).MsgType, item);
                    }
                }
            }
        }

        //通过msg组包 之后send
        public Packet(Msg msg, Header header)
        {
            this.Msg = msg;
            this.Header = header;
        }

        ////收到数据后转换成Packet格式
        public Packet(byte[] data)
        {
            if (data.Length < Header.Length)
            {
                Console.WriteLine($"Error RecvData Length<{Header.Length}");
                return;
            }

            this.Data = data;
        }

        public Header Header { get; set; } = null;

        public Msg Msg { get; set; } = null;

        public byte[] Data
        {
            get
            {
                if (Header != null && Msg != null)
                {
                    return Header.Data.Concat(Msg.Data).ToArray<byte>();
                }

                return null;
            }
            private set
            {
                this.GetData(value);
            }
        }

        private void GetData(byte[] data)
        {
            if (this.Header == null)
                Header = new Header(data.Take(Header.Length).ToArray<byte>());

            if (Msg == null && this.Header != null)
            {
                foreach (var item in msgTypeMap)
                {
                    if (item.Key == this.Header.MsgType)
                    {
                        object o = Activator.CreateInstance(item.Value, new object[] { data.Skip(Header.Length).ToArray<byte>() });

                        Msg = o as Msg;
                        break;
                    }
                }
            }
        }
    }
}