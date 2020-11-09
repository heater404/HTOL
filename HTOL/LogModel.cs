using HTOL.Model;
using System;
using static HTOL.Enums;

namespace HTOL
{
    public class LogModel : BaseModel
    {
        public LogModel(string msg, LogType lev)
        {
            this.time = DateTime.Now;
            this.message = msg;
            this.lev = lev;
        }

        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
        }

        private LogType lev;

        public LogType Lev
        {
            get { return lev; }
            set { lev = value; RaisePropertyChanged(); }
        }

        public override string ToString()
        {
            return $"{this.time:HH:mm:ss} > {this.message}";
        }
    }
}