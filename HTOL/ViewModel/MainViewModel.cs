using HTOL.Command;
using HTOL.Common;
using HTOL.Model;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HTOL.ViewModel
{
    public class MainViewModel : BaseModel
    {
        public GraphViewModel GraphVM { set; get; }
        public LoggerViewModel LoggerVM { set; get; }
        public SitesViewModel SitesVM { get; set; }

        public RelayCommand ConnectCmd { get; set; }
        public RelayCommand DisconnectCmd { get; set; }

        Communication comm = Communication.Instacne;
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
            if (!comm.CreatClient())
            {
                MessageBox.Show("Init USB Fail");
                return;
            }

            DateTime time = DateTime.Now;
            Task.Run(() => SitesVM.MonitorRegister(30 * 1000));
            Task.Run(() => GraphVM.MonitorInstrument(30 * 1000, time));
            //Task.Run(() => GraphVM.MonitorTemp(30 + 1000, time));
        }

        private void Disconnect()
        {
            SitesVM.StopMonitorRegister();
            GraphVM.StopMonitorInstrument();
            GraphVM.StopMonitorTemp();

            comm.DestoryClient();
        }
    }
}