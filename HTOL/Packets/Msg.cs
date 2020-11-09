using System;
using System.Collections.Generic;

namespace HTOL.Packets
{
    /// <summary>
    ///Request类型的子类继承Msg的时候，不用管其长度，后续在Send的时候会自动分包
    /// </summary>
    public abstract class Msg : UserData
    {
        public Int32 MsgType { get; set; }

        public Int32 Length
        {
            get
            {
                if (null == this.Data)
                    return 0;
                return this.Data.Length;
            }
        }

        public Int32 GetMsgType()
        {
            Type t = this.GetType();
            foreach (var att in t.GetCustomAttributes(true))
            {
                if (att is MsgTypeAttribute)
                {
                    return (att as MsgTypeAttribute).MsgType;
                }
            }
            return 0x00;
        }

        public List<Msg> SplitMsg(int maxPktLen)
        {
            int maxMsgLen = maxPktLen - Header.Length;
            int num = (int)Math.Ceiling(this.Data.Length / (maxMsgLen * 1.0));
            int lastMsgLen = this.Data.Length - maxMsgLen * (num - 1);

            List<Msg> msgs = new List<Msg>();

            for (int i = 0; i < num; i++)
            {
                Msg msg = (Msg)this.MemberwiseClone();

                byte[] data = new byte[(i == num - 1) ? lastMsgLen : maxMsgLen];
                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = this.Data[maxMsgLen * i + j];
                }

                msg.Data = data;

                msgs.Add(msg);
            }
            return msgs;
        }
    }
}