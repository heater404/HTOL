using HTOL.Common;
using HTOL.Messages;
using HTOL.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using static HTOL.Enums;

namespace HTOL.ViewModel
{
    public class SitesViewModel
    {
        Communication comm = Communication.Instacne;
        public List<SiteModel> Sites { get; set; }

        public SitesViewModel()
        {
            #region Init

            Sites = new List<SiteModel>();

            SiteModel model = new SiteModel
            {
                ID=0,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_1,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 1,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_0,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 2,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_5,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 3,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_1,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 4,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_0,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 5,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_5,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 6,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_1,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 7,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_0,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 8,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_5,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 9,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_1,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 10,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_0,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 11,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_5,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 12,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_2,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 13,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_3,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 14,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_4,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 15,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_2,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

             model = new SiteModel
            {
                ID = 16,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_3,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

           model = new SiteModel
            {
                ID = 17,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_4,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 18,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_2,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 19,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_3,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 20,
                TcaAddr = TcaAddr._0x70,
                Channel = TcaChannel.Channel_4,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 21,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_2,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

            model = new SiteModel
            {
                ID = 22,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_3,
                ChipAddr = ChipAddr._0x10,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);

             model = new SiteModel
            {
                ID = 23,
                TcaAddr = TcaAddr._0x71,
                Channel = TcaChannel.Channel_4,
                ChipAddr = ChipAddr._0x1A,
                Status = SiteStatus.Stop,
                Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
            };
            Sites.Add(model);
            #endregion Init

            #region obsolete
            //foreach (TcaAddr tcaAddr in Enum.GetValues(typeof(TcaAddr)))
            //{
            //    foreach (ChipAddr chipAddr in Enum.GetValues(typeof(ChipAddr)))
            //    {
            //        foreach (TcaChannel tcaChannel in Enum.GetValues(typeof(TcaChannel)))
            //        {
            //            Sites.Add
            //                (
            //                   new SiteModel
            //                   {
            //                       TcaAddr = tcaAddr,
            //                       Channel = tcaChannel,
            //                       ChipAddr = chipAddr,
            //                       Status = SiteStatus.Stop,
            //                       Register = new List<RegisterModel>
            //                        {
            //                            new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
            //                        }
            //                   }
            //                );
            //        }
            //    }
            //}
            #endregion

            Communication.Instacne.RecvRegsHanlder += RecvRegsHanlder;
        }

        private void SetSiteStatus(TcaAddr tca, ChipAddr chip, TcaChannel channel, SiteStatus status)
        {
            SiteModel model = Sites.Find(s => s.TcaAddr == tca && s.ChipAddr == chip && s.Channel == channel);
            if (model != null)
            {
                model.Status = status;
            }
        }

        private void SetAllSiteStatus(SiteStatus status)
        {
            foreach (var site in Sites)
            {
                site.Status = status;
            }
        }

        private void RecvRegsHanlder(object sender, ToFReply msg)
        {
            foreach (var site in Sites)
            {
                if (site.TcaAddr == msg.TcaAddr && site.Channel == msg.TcaChannel && site.ChipAddr == msg.ChipAddr)
                {
                    if (msg.Num == 0)
                    {
                        site.Status = SiteStatus.Lost;
                        NLogHelper.DebugLog($"{site.TcaAddr} {site.Channel} {site.ChipAddr} Don't Recv Reg Success");
                    }    
                    else
                    {
                        foreach (var reg in site.Register)
                        {
                            foreach (var regRecv in msg.GetRegValue())
                            {
                                if (regRecv.Key == reg.Addr)
                                {
                                    reg.Val = regRecv.Value;//收到值之后需要对值进行校验

                                    if (CheckRegister(reg.Val, reg.Checks))
                                        site.Status = SiteStatus.Run;
                                    else
                                    {
                                        site.Status = SiteStatus.Error;
                                        NLogHelper.DebugLog($"{site.TcaAddr} {site.Channel} {site.ChipAddr} Recv Reg {reg.Val}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckRegister(UInt32 val, List<RegisterCheck> checks)
        {
            foreach (var check in checks)
            {
                UInt32 mask = (UInt32)(Math.Pow(2, check.End - check.Start + 1) - 1) << check.Start;

                if ((val & mask) != check.CheckValue)
                    return false;
            }
            return true;
        }


        private bool isMonitorRegister = false;
        public void StopMonitorRegister()
        {
            isMonitorRegister = false;
            SetAllSiteStatus(SiteStatus.Stop);
        }

        /// <summary>
        /// 监控寄存器的值
        /// </summary>
        /// <param name="timeout">查询的时间间隔 单位毫秒</param>
        public void MonitorRegister(int timeout)
        {
            SetAllSiteStatus(SiteStatus.Run);
            isMonitorRegister = true;
            while (isMonitorRegister)
            {
                foreach (var site in Sites)
                {
                    foreach (var reg in site.Register)
                    {
                        if (comm.ReadReg(reg.Addr, 1, site.TcaAddr, site.Channel, site.ChipAddr) > 0)
                            NLogHelper.DebugLog($"ReadReg addr:{reg.Addr} TcaAddr:{site.TcaAddr} Channel:{site.Channel} ChipAddr:{site.ChipAddr}");
                    }
                }
                Thread.Sleep(timeout);
            }
        }
    }
}