using HTOL.Common;
using HTOL.Model;
using System;
using System.Collections.Generic;
using static HTOL.Enums;

namespace HTOL.ViewModel
{
    public class SitesViewModel
    {
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
                                        new RegisterModel { Addr = 0x0431, Val = 0xFF } ,
                                        new RegisterModel{ Addr=0x043d,Val=0x00}
                                    }
                               }
                            );
                    }
                }
            }

            #endregion Init

            Communication.Instacne.RecvRegsHanlder += RecvRegsHanlder;
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
                                reg.Val = regRecv.Value;
                        }
                    }
                }
            }
        }
    }
}