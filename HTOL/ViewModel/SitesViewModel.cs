using HTOL.Common;
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

            foreach (TcaAddr tcaAddr in Enum.GetValues(typeof(TcaAddr)))
            {
                foreach (ChipAddr chipAddr in Enum.GetValues(typeof(ChipAddr)))
                {
                    foreach (TcaChannel tcaChannel in Enum.GetValues(typeof(TcaChannel)))
                    {
                        Sites.Add
                            (
                               new SiteModel
                               {
                                   TcaAddr = tcaAddr,
                                   Channel = tcaChannel,
                                   ChipAddr = chipAddr,
                                   Status = SiteStatus.Stop,
                                   Register = new List<RegisterModel>
                                    {
                                        new RegisterModel{ Addr=0x040d,Val=0x00,Checks=new List<RegisterCheck>{ new RegisterCheck { Start=0,End=0,CheckValue=1} } }
                                    }
                               }
                            );
                    }
                }
            }
            #endregion Init

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

        private void RecvRegsHanlder(object sender, Tuple<TcaAddr, TcaChannel, ChipAddr, Dictionary<ushort, uint>> e)
        {
            foreach (var item in Sites)
            {
                if (item.TcaAddr == e.Item1 && item.Channel == e.Item2 && item.ChipAddr == e.Item3)
                {
                    foreach (var reg in item.Register)
                    {
                        foreach (var regRecv in e.Item4)
                        {
                            if (regRecv.Key == reg.Addr)
                            {
                                reg.Val = regRecv.Value;//收到值之后需要对值进行校验

                                if(CheckRegister(reg.Val,reg.Checks))
                                    item.Status = SiteStatus.Run;
                                else
                                {
                                    item.Status = SiteStatus.Error;
                                    NLogHelper.DebugLog($"{item.TcaAddr} {item.Channel} {item.ChipAddr} Recv Reg {reg.Val}");
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