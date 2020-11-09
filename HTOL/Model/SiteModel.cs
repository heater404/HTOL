using System;
using System.Collections.Generic;
using static HTOL.Enums;

namespace HTOL.Model
{
    public class RegisterModel : BaseModel
    {
        public UInt16 Addr { get; set; }

        private UInt32 val;

        public UInt32 Val
        {
            get { return val; }
            set
            {
                val = value;
                RaisePropertyChanged();
            }
        }
    }

    public class SiteModel : BaseModel
    {
        public TcaAddr TcaAddr { get; set; }
        public TcaChannel Channel { get; set; }
        public ChipAddr ChipAddr { get; set; }

        public List<RegisterModel> Register { get; set; }

        private SiteStatus status;

        public SiteStatus Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(); }
        }
    }
}