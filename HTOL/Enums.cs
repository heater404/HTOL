using System;

namespace HTOL
{
    public class Enums
    {
        public enum Times : Int32
        {
            Ten = 10,
            Hundred = 100,
            Thousand = 1000,
        }

        public enum TofType
        {
            R_reg = 0,
            W_reg = 1,
            ChipEnable = 2,
            TransEn = 3,
            Arg_set = 4,
            Reset = 5,
        }

        public enum LogType
        {
            Error = 0,
            Warning,
            Info,
        }

        public enum SiteStatus
        {
            Stop,
            Run,
            Error,
        }

        public enum TcaAddr : byte
        {
            _0x70=0x70,
            _0x71=0x71,
        }

        public enum TcaChannel : byte
        {
            Channel_0=0,
            Channel_1,
            Channel_2,
            Channel_3,
            Channel_4,
            Channel_5,
        }

        public enum ChipAddr : byte
        {
            _0x10 = 0x10,
            _0x20 = 0x20,
        }
    }
}