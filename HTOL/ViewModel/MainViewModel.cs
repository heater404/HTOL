using HTOL.Command;
using HTOL.Common;
using HTOL.Model;
using System;
using static HTOL.Enums;

namespace HTOL.ViewModel
{
    public class MainViewModel : BaseModel
    {
        public GraphViewModel GraphVM { set; get; }
        public LoggerViewModel LoggerVM { set; get; }
        public SitesViewModel SitesVM { get; set; }

        public RelayCommand ConnectCmd { get; set; }
        public RelayCommand DisconnectCmd { get; set; }

        public MainViewModel()
        {
            SitesVM = new SitesViewModel();
            GraphVM = new GraphViewModel();

            InitCmds();
        }

        private void InitCmds()
        {
            ConnectCmd = new RelayCommand(Connect);
            DisconnectCmd = new RelayCommand(Disconnect);
        }

        private void Connect()
        {
            SitesVM.SetAllSiteStatus(SiteStatus.Run);

            IT6332A t6332A = new IT6332A();
            t6332A.Open();

            Console.WriteLine(t6332A.QueryVoltage(InstrumentChannel.CH2));
            Console.WriteLine(t6332A.QueryVoltage(InstrumentChannel.CH1));
            Console.WriteLine(t6332A.QueryVoltage(InstrumentChannel.CH3));
            Console.WriteLine(t6332A.QueryVoltage()[0].ToString()+ t6332A.QueryVoltage()[1].ToString() + t6332A.QueryVoltage()[2]);

            Console.WriteLine(t6332A.QueryCurrent(InstrumentChannel.CH3));
            Console.WriteLine(t6332A.QueryCurrent(InstrumentChannel.CH2));
            Console.WriteLine(t6332A.QueryCurrent(InstrumentChannel.CH1));
            Console.WriteLine(t6332A.QueryCurrent());
        }


        private void Disconnect()
        {
            SitesVM.SetAllSiteStatus(SiteStatus.Stop);
        }
    }
}