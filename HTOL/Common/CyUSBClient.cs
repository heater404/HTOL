using CyUSB;
using HTOL.Packets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTOL.Common
{
    public class CyUSBClient : IClient
    {
        public const UInt32 MAX_PKT_LEN = 65000;
        private USBDeviceList usbDevices = null;
        private CyFX3Device device = null;
        private bool isConnect = false;
        private const int VID = 0x04B4;
        private const int PID = 0x00F0;
        private CyBulkEndPoint bepOut = null;
        private CyBulkEndPoint bepIn83 = null;
        private readonly BlockingCollection<byte[]> recvDatas = new BlockingCollection<byte[]>(3000);
        private readonly List<CMD_PROCESS_MAP> procList = null;

        private CyFX3Device FindCyDevice(short vid, short pid)
        {
            CyFX3Device device = null;
            for (int i = 0; i < 5; i++)
            {
                usbDevices = new USBDeviceList(CyConst.DEVICES_CYUSB);
                if (0 != usbDevices.Count)
                    break;
            }

            if (0 == usbDevices.Count)
                return device;

            usbDevices.DeviceRemoved += UsbDevices_DeviceRemoved;

            foreach (var item in usbDevices)
            {
                device = item as CyFX3Device;
                if (null != device)
                    if (device.VendorID == vid && device.ProductID == pid)
                    {
                        return device;
                    }
            }
            return device;
        }

        private void UsbDevices_DeviceRemoved(object sender, EventArgs e)
        {
            isConnect = false;
            USBEventArgs usbEvent = e as USBEventArgs;
            //Console.WriteLine(usbEvent.FriendlyName.ToString() + " Removed");
            NLogHelper.DebugLog(usbEvent.FriendlyName.ToString() + " Removed");
        }

        public CyUSBClient(List<CMD_PROCESS_MAP> procList)
        {
            this.procList = procList;
        }

        public bool Close()
        {
            if (device != null)
                device.Dispose();
            device = null;
            bepOut = null;
            bepIn83 = null;
            isConnect = false;

            return true;
        }

        public bool Open()
        {
            device = FindCyDevice(VID, PID);
            if (null == device)
                return false;

            foreach (CyUSBEndPoint ept in device.EndPoints)
            {
                if (!ept.bIn && (ept.Attributes == 2) && ept.Address == 0x02)
                {
                    bepOut = ept as CyBulkEndPoint;
                }

                if (ept.bIn && (ept.Attributes == 2) && ept.Address == 0x83)
                {
                    bepIn83 = ept as CyBulkEndPoint;
                }
            }

            //he Attributes member indicates the type of endpoint per the following list.
            //0: Control
            //1: Isochronous
            //2: Bulk
            //3: Interrupt
            isConnect = true;
            Task.Run(() => Bep83USBRecv());
            //Thread t83Recv = new Thread(Bep83USBRecv);
            //t83Recv.Priority = ThreadPriority.Highest;
            //t83Recv.IsBackground = true;
            //t83Recv.Start();

            Task.Run(() => AnalyseBepIn83Packet());
            //Task.Run(() => AnalyseBepIn82Packet());
            return true;
        }

        private void Bep83USBRecv()
        {
            if (null == bepIn83)
                return;

            int len = 1024 * 48;
            byte[] recv = new byte[len];
            bepIn83.TimeOut = 300;
            bepIn83.XferSize = 1024 * 48;
            try
            {
                while (isConnect)
                {
                    len = 1024 * 48;
                    recv = new byte[len];
                    if (bepIn83.XferData(ref recv, ref len))
                    {
                        //这里的len是实际读到的数据长度，recv只是一个buffer,所以Buffer中的数据不一定全部是收到的数据。
                        recvDatas.Add(recv.Take(len).ToArray<byte>());
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorLog(ex,"Beq83USBRecv");
            }
        }

        private void AnalyseBepIn83Packet()
        {
            while (isConnect)
            {
                if (!recvDatas.TryTake(out byte[] recvData, -1))
                    continue;

                if (0 == RecvOnePkt(recvData, out Packet pkt))
                {
                    foreach (CMD_PROCESS_MAP proc in procList)
                        if (pkt.Msg.MsgType == proc.type)
                            proc.process?.Invoke(pkt);
                }
            }
        }

        /// <summary>
        /// 收包这里不做分包合并，每一包都触发委托处理
        /// 在委托里面根据需要进行合包，分包信息在Header里面有，所以委托参数从Msg改为Packet，
        /// </summary>
        /// <param name="data">skt收到的报文字节</param>
        /// <param name="pkt">返回的自定义Packet类型对象</param>
        /// <returns>是否接收正确 0表示正常， 非0表示异常</returns>
        private int RecvOnePkt(byte[] data, out Packet pkt)
        {
            //Console.WriteLine("RecvOnePkt:");
            //Console.WriteLine("PktSN = " + pkt.Header.PktSN.ToString());
            //Console.WriteLine("MsgSn = " + pkt.Header.MsgSn.ToString());
            //Console.WriteLine("MsgType = 0x" + pkt.Header.MsgType.ToString("x"));
            //Console.WriteLine("MsgLen = " + pkt.Header.MsgLen.ToString());
            //Console.WriteLine("TotalMsgLen = " + pkt.Header.TotalMsgLen.ToString());

            pkt = new Packet(data);

            if (pkt.Data.Length < pkt.Header.MsgLen + Header.Length)
            {
                NLogHelper.DebugLog("pkt.Length<pkt.MsgLen + pkt.headerLen");
                return -1;
            }

            if (pkt.Msg.Length < pkt.Header.TotalMsgLen)
            {
                NLogHelper.DebugLog("msg.data.Length < pkt.TotalMsgLen");
                return -2;
            }

            return 0;   // rcv done
        }

        /// <summary>
        /// 分包发送的Msg,这里定义的Msg的内容应该包含了全部的字节，在这个函数里面
        /// 会自己进行分包，定义Msg的时候不需要管长度，是多大就定义多大
        /// </summary>
        /// <param name="msg">需要被分包的Msg</param>
        /// <param name="maxPktLen">可选参数，分包的长度，默认值为MAX_PKT_LEN</param>
        /// <returns>表示发送出去的报文字节长度</returns>
        public int SendMsg(Msg msg, int maxPktLen = (int)MAX_PKT_LEN)
        {
            if (null == device)
            {
                Console.WriteLine("Client haven't started");
                return 0;
            }

            if (Header.Length > maxPktLen)
            {
                Console.WriteLine("Header.Length>maxPktLen");
                return 0;
            }

            if (maxPktLen > MAX_PKT_LEN)
            {
                Console.WriteLine("maxPktLen > MAX_PKT_LEN");
                return 0;
            }

            List<Msg> msgs = msg.SplitMsg(maxPktLen);

            int sendLen = 0;
            Random random = new Random();
            var sn = random.Next(Int32.MinValue, Int32.MaxValue);
            for (int i = 0; i < msgs.Count; i++)
            {
                Header header = new Header
                {
                    PktSN = sn,
                    TotalMsgLen = msg.Length,
                    MsgSn = i,
                    MsgType = msg.MsgType,
                    MsgLen = msgs[i].Length,
                    Timeout = -1,
                };
                header.Identify = header.GetIdentify();

                Packet pkt = new Packet(msgs[i], header);
                this.USBSend(pkt.Data);
                sendLen += pkt.Data.Length;
            }

            return sendLen;
        }

        private int USBSend(byte[] data)
        {
            if (null == bepOut)
                return 0;

            int len = data.Length;

            if (bepOut.XferData(ref data, ref len))
            {
                //Console.WriteLine($"Send One Msg MsgType:0x{BitConverter.ToInt32(data, 44):X2}");
                NLogHelper.DebugLog($"Send One Msg MsgType:0x{BitConverter.ToInt32(data, 44):X2}");
            }

            return len;
        }
    }
}