using HTOL.Messages;
using HTOL.Packets;
using System;
using System.Collections.Generic;
using System.Threading;
using static HTOL.Enums;
#region File Information

/* ----------------------------------------------------------------------
* File Name: Communication
* Create Author: ZDK
* Create DateTime: 4/26/2020 7:13:26 PM
* Description:
*----------------------------------------------------------------------*/

#endregion File Information

namespace HTOL.Common
{
    public delegate void RcvProcess(Packet pkt);

    public struct CMD_PROCESS_MAP
    {
        public UInt32 type;
        public RcvProcess process;
    };

    public class Communication
    {
        private readonly CyUSBClient client = null;
        public const UInt32 maxRWLength = 10;
        public static Communication Instacne { get; } = new Communication();

        private readonly AutoResetEvent waitHandle = new AutoResetEvent(false);

        private List<CMD_PROCESS_MAP> RegisterRecvProcess()
        {
            List<CMD_PROCESS_MAP> procList = new List<CMD_PROCESS_MAP>();
            CMD_PROCESS_MAP item;

            Type t = typeof(Communication);
            var ms = t.GetMethods();
            foreach (var m in ms)
            {
                foreach (var attr in m.GetCustomAttributes(false))
                {
                    if (attr is MsgTypeAttribute)
                    {
                        item.type = (uint)(attr as MsgTypeAttribute).MsgType;
                        item.process = (RcvProcess)m.CreateDelegate(typeof(RcvProcess), this);
                        procList.Add(item);
                        break;
                    }
                }
            }
            return procList;
        }

        public event EventHandler<ToFReply> RecvRegsHanlder;
        public event EventHandler<float> RecvTempHandler;

        private Communication()
        {
            client = new CyUSBClient(RegisterRecvProcess());
        }

        public bool CreatClient()
        {
            if (null != client)
            {
                return client.Open();
            }

            return false;
        }

        public bool DestoryClient()
        {
            if (null != client)
            {
                client.Close();
            }

            return true;
        }

        public UInt32 ReadRegs(UInt16 startReg, UInt32 len)
        {
            UInt32 readLen = 0;
            while (0 < len)//根据读写的最大长度分段
            {
                uint curReadLen;
                if (len > maxRWLength)
                    curReadLen = maxRWLength;
                else
                    curReadLen = len;

                uint ret = ReadReg(startReg, curReadLen);

                readLen += ret;
                startReg += (UInt16)ret;
                len -= ret;
            }
            return readLen;
        }

        public UInt32 ReadReg(UInt16 startReg, UInt32 len)//返回读写寄存器的长度
        {
            if (null == client)
                return 0;

            Msg msg = new ToFRequest(16)
            {
                Tt = TofType.R_reg,
                TofChipID = ToFRequest.ChipID,
                Num = (Int32)len,
                Addr = startReg,
            };
            if (0 < client.SendMsg(msg))
            {
                return len;
            }

            return 0;
        }

        public UInt32 ReadReg(UInt16 startReg, UInt32 len, TcaAddr tcaAddr, TcaChannel tcaChannel, ChipAddr chipAddr)//返回读写寄存器的长度
        {
            if (null == client)
                return 0;

            Msg msg = new ToFRequest(16)
            {
                Tt = TofType.R_reg,
                Num = (Int32)len,
                Addr = startReg,
            };
            ((ToFRequest)msg).SetTofChipID(tcaAddr, tcaChannel, chipAddr);
            if (0 < client.SendMsg(msg))
            {
                return len;
            }

            return 0;
        }

        public UInt32 WriteRegs(UInt16 startReg, UInt32[] regs, UInt32 len)
        {
            UInt32 writeLen = 0;
            UInt32 ret = 0;
            while (0 < len)//根据读写的最大长度分段
            {
                uint curWriteLen;
                if (len > maxRWLength)
                    curWriteLen = maxRWLength;
                else
                    curWriteLen = len;

                ret = WriteReg(startReg, regs, curWriteLen);

                writeLen += ret;
                startReg += (UInt16)ret;
                len -= ret;
            }
            return writeLen;
        }

        public UInt32 WriteReg(UInt16 startReg, UInt32[] vals, UInt32 len)
        {
            if (null == client)
                return 0;

            if (vals.Length < len)
            {
                return 0;
            }

            Msg msg = new ToFRequest((int)(16 + (4 * len)))
            {
                Tt = TofType.W_reg,
                TofChipID = ToFRequest.ChipID,
                Num = (Int32)len,
                Addr = startReg,
            };

            ((ToFRequest)msg).SetRegValue(vals);

            if (0 < client.SendMsg(msg))
            {
                return len;
            }
            return 0;
        }

        public int SetTofArg(int setType, int setValue)
        {
            if (null == client)
                return 0;

            Msg msg = new ToFRequest(16)
            {
                Tt = TofType.Arg_set,
                TofChipID = ToFRequest.ChipID,
                SetType = setType,
                SetValue = setValue,
            };

            return client.SendMsg(msg);
        }

        public int SetTofArg(int setType, int subType, int subValue)
        {
            if (null == client)
                return 0;

            Msg msg = new ToFRequest(20)
            {
                Tt = TofType.Arg_set,
                TofChipID = ToFRequest.ChipID,
                SetType = setType,
                Sub_Type = subType,
                Sub_Value = subValue,
            };

            return client.SendMsg(msg);
        }

        private bool resetFlag = false;

        public bool Reset()
        {
            if (null == client)
                return false;

            Msg msg = new ToFRequest(12)
            {
                Tt = TofType.Reset,
                TofChipID = ToFRequest.ChipID,
                ResetType = ToFRequest.RegReset,
            };

            this.resetFlag = false;
            if (0 < client.SendMsg(msg))
            {
                if (waitHandle.WaitOne(1500))
                    return resetFlag;
                else
                {
                    return false;
                }
            }
            return false;
        }

        public int CheckTemp()
        {
            if (null == client)
                return 0;

            Msg msg = new TempRequest(1)
            {
                TempNum = 1,
                TMP108Gain = Times.Thousand,
            };

            return client.SendMsg(msg);
        }

        /// <summary>
        /// 求不超过cycle的frameNum的最大因数
        /// </summary>
        /// <param name="frameNun"></param>
        /// <param name="cycle"></param>
        /// <returns></returns>
        private UInt32 MaxDivisor(UInt32 frameNun, UInt32 cycle)
        {
            UInt32 ret = 0;

            var min = Math.Min(frameNun, cycle);

            for (UInt32 i = min; i > 0; i--)
            {
                if (0 == frameNun % i)
                {
                    ret = i;
                    break;
                }
            }

            return ret;
        }

        //视频数据采集使能，先放在这，按道理应该放到U3D中
        public int TransEn(bool enable)
        {
            if (null == client)
                return 0;

            Msg msg = new ToFRequest(12)
            {
                Tt = TofType.TransEn,
                TofChipID = ToFRequest.ChipID,
                StreamEnable = enable
            };
            return client.SendMsg(msg);
        }

        [MsgType(0x02)]
        public void CmdProcTofReply(Packet pkt)
        {
            if (!(pkt.Msg is ToFReply msg))
                return;

            switch (msg.Tt)
            {
                case TofType.Reset:
                    this.resetFlag = msg.ResetAck;
                    waitHandle.Set();
                    break;

                case TofType.R_reg:
                    RecvRegsHanlder?.Invoke(this,msg);
                    break;

                default:
                    break;
            }
        }

        [MsgType(0x04)]
        public void CmdProcTempReply(Packet pkt)
        {
            if (!(pkt.Msg is TempReply msg))
                return;

            RecvTempHandler?.Invoke(this, msg.TMP108.Value);
        }
    }
}