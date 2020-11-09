using System;

namespace HTOL.Packets
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MsgTypeAttribute : Attribute
    {
        public Int32 MsgType;

        public MsgTypeAttribute(Int32 msgType)
        {
            this.MsgType = msgType;
        }
    }
}