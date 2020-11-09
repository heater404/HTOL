using HTOL.Packets;
using System;
using static HTOL.Enums;

namespace HTOL.Messages
{
    [MsgType(0x03)]
    public class TempRequest : Msg
    {
        public TempRequest(int num)
        {
            this.Data = new byte[4 + 4 * num];
            this.MsgType = GetMsgType();
        }

        public Int32 TempNum
        {
            set { this[0] = value; }
        }

        public Times TMP108Gain//
        {
            set { this[4] = (Int32)value; }
        }

        public Times VcselGain//Driver
        {
            set { this[8] = (Int32)value; }
        }

        public Times TempGain2
        {
            set { this[12] = (Int32)value; }
        }
    }

    [MsgType(0x04)]
    public class TempReply : Msg
    {
        public TempReply(byte[] data)
        {
            this.Data = data;
            this.MsgType = GetMsgType();
        }

        public float? TMP108
        {
            get { return GetTemp(0); }
        }

        public float? Vcsel
        {
            get { return GetTemp(1); }
        }

        #region alternative

        public Int32 TempNum
        {
            get { return (Int32)this[0]; }
        }

        public Int32 TempGain0//TMP108
        {
            get { return (Int32)this[4]; }
        }

        public Int32 TempValue0//TMP108
        {
            get { return (Int32)this[8]; }
        }

        public Int32 TempGain1//VcselDriver
        {
            get { return (Int32)this[12]; }
        }

        public Int32 TempValue1//TMP108
        {
            get { return (Int32)this[16]; }
        }

        public Int32 TempGain2//VcselDriver
        {
            get { return (Int32)this[20]; }
        }

        public Int32 TempValue2//TMP108
        {
            get { return (Int32)this[24]; }
        }

        #endregion alternative

        public float? GetTemp(int index)
        {
            if (index >= TempNum)
                return null;

            return this[(UInt32)(8 + 8 * index)] / (this[(UInt32)(4 + 8 * index)] * 1.0f);
        }
    }
}