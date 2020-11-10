using Ivi.Visa;
using NationalInstruments.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HTOL.Common
{
    public abstract class Instrument
    {
        protected string ResourceName;
        protected MessageBasedSession session = null;
        /// <summary>
        /// 开启设备
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            bool success = false;
            ResourceManager resource = new ResourceManager();
            try
            {
                session = (MessageBasedSession)resource.Open(ResourceName);
            }
            catch (NativeVisaException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (session != null)
                success = true;
            return success;
        }

        public bool Close()
        {
            if (session == null)
                return true;

            session.Dispose();
            return session.IsDisposed;
        }

        protected bool WriteCmd(string cmd)
        {
            bool success = false;
            if (session == null)
                return success;
            try
            {
                this.session.RawIO.Write(cmd);
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return success;
        }

        protected string ReadCmd()
        {
            string result = string.Empty;
            if (session == null)
                return result;

            try
            {
                result = this.session.RawIO.ReadString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
    }
}
