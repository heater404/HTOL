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

        public void SetSiteStatus(TcaAddr tca, ChipAddr chip, TcaChannel channel, SiteStatus status)
        {
            SiteModel model = Sites.Find(s => s.TcaAddr == tca && s.ChipAddr == chip && s.Channel == channel);
            if (model != null)
            {
                model.Status = status;
            }
        }

        public void SetAllSiteStatus(SiteStatus status)
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
                                reg.Val = regRecv.Value;
                        }
                    }
                }
            }
        }
    }
}