using HTOL.Packets;
using System;
using System.Collections.Generic;
using static HTOL.Enums;

namespace HTOL.Messages
{
    [MsgType(0x01)]
    public class ToFRequest : Msg
    {
        public ToFRequest(int msglen)
        {
            this.Data = new byte[msglen];
            this.MsgType = GetMsgType();
        }

        public TofType Tt
        {
            set { this[0] = (int)value; }
        }

        public const int ChipID = 0x10;

        public Int32 TofChipID
        {
            set { this[4] = value; }
        }

        public Int32 Num
        {
            set { this[8] = value; }
        }

        public Int32 Addr
        {
            set { this[12] = value; }
        }

        public void SetTofChipID(TcaAddr tcaAddr, TcaChannel tcaChannel, ChipAddr chipAddr)
        {
            TofChipID = (byte)tcaAddr | ((byte)tcaChannel) << 8 | ((byte)chipAddr) << 16;
        }

        public void SetRegValue(UInt32[] vals)
        {
            for (UInt16 i = 0; i < vals.Length; i++)
            {
                this[(UInt32)(16 + (4 * i))] = (Int32)vals[(UInt16)i];
            }
        }

        public bool StreamEnable
        {
            set
            {
                this[8] = value ? 1 : 0;
            }
        }

        public Int32 SetType
        {
            set { this[8] = (int)value; }
        }

        public Int32 Sub_Type
        {
            set { this[12] = value; }
        }

        public Int32 Sub_Value
        {
            set { this[16] = value; }
        }

        public const int TofMode = 0;
        public const int GrayMode = 1;
        public const int TofGray = 2;

        public Int32 SetValue
        {
            set { this[12] = value; }
        }

        public const int RegReset = 1;

        public Int32 ResetType
        {
            set { this[8] = value; }
        }
    }

    [MsgType(0x02)]
    public class ToFReply : Msg
    {
        public ToFReply(byte[] data)
        {
            this.Data = data;
            this.MsgType = GetMsgType();
        }

        public TcaAddr TcaAddr
        {
            get { return (TcaAddr)(this[4] & 0x000000FF); }
        }

        public TcaChannel TcaChannel
        {
            get { return (TcaChannel)((this[4] & 0x0000FF00)>>8); }
        }

        public ChipAddr ChipAddr
        {
            get { return (ChipAddr)((this[4] & 0x00FF0000)>>16); }
        }

        public TofType Tt
        {
            get { return (TofType)this[0]; }
        }

        public Int32 Num
        {
            get { return this[8]; }
        }

        public Int32 Addr
        {
            get { return this[12]; }
        }

        public Dictionary<UInt16, UInt32> GetRegValue()
        {
            Dictionary<UInt16, UInt32> values = new Dictionary<ushort, uint>();
            for (int i = 0; i < Num; i++)
            {
                values[(UInt16)(Addr + i)] = (UInt32)this[(UInt32)(16 + i)];
            }

            return values;
        }

        public bool ResetAck
        {
            get { return this[12] == 0; }
        }
    }
}