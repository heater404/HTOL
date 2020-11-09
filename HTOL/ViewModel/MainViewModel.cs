using HTOL.Command;
using HTOL.Model;
using static HTOL.Enums;

namespace HTOL.ViewModel
{
    public class MainViewModel : BaseModel
    {
        public GraphViewModel GraphVM { set; get; }
        public LoggerViewModel LoggerVM { set; get; }
        public SitesViewModel SitesVM { get; set; }

        public RelayCommand ConnectCmd { get; set; }

        public MainViewModel()
        {
            SitesVM = new SitesViewModel();
            GraphVM = new GraphViewModel();

            InitCmds();
        }

        private void InitCmds()
        {
            ConnectCmd = new RelayCommand(Connect);
        }

        private void Connect()
        {
            SitesVM.Sites[0].Status = SiteStatus.Run;
            SitesVM.Sites[0].Register[0].Val = 0x999;
        }
    }
}