using Ivi.Visa;
using NationalInstruments.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static HTOL.Enums;

namespace HTOL.Common
{
    public class IT6332A
    {
        private const string ResourceName = "USB0::0xFFFF::0x6300::802071092757510163::INSTR";
        private MessageBasedSession session = null;
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
            session.Dispose();
            session = null;
            return session.IsDisposed;
        }

        public double QueryVoltage(InstrumentChannel channel)
        {
            double voltage = 0;
            if (WriteCmd($"INST {channel}"))//切换通道
            {
                if (WriteCmd("MEAS:VOLT?"))//读取当前通道的电压
                {
                    string vol = ReadCmd();//读返回值
                    if (!string.IsNullOrEmpty(vol))
                    {
                        voltage = double.Parse(vol);
                    }
                }
            }
            return voltage;
        }

        public double QueryCurrent(InstrumentChannel channel)
        {
            double current = 0;
            if (WriteCmd($"INST {channel}"))//切换通道
            {
                if (WriteCmd("MEAS:CURR?"))//读取当前通道的电流
                {
                    string vol = ReadCmd();//读返回值
                    if (!string.IsNullOrEmpty(vol))
                    {
                        current = double.Parse(vol);
                    }
                }
            }
            return current;
        }

        public double[] QueryVoltage()
        {
            double[] voltages = new double[3];
            if (WriteCmd("MEAS:VOLT:ALL?"))
            {
                string vol = ReadCmd();//读返回值
                if (!string.IsNullOrEmpty(vol))
                {
                    string[] vols = vol.Split(',');
                    for (int i = 0; i < vols.Length; i++)
                    {
                        voltages[i] = double.Parse(vols[i]);
                    }
                }
            }

            return voltages;
        }

        public double[] QueryCurrent()
        {
            double[] currents = new double[3];
            if (WriteCmd("MEAS:CURR:ALL?"))
            {
                string curr = ReadCmd();//读返回值
                if (!string.IsNullOrEmpty(curr))
                {
                    string[] currs = curr.Split(',');
                    for (int i = 0; i < currs.Length; i++)
                    {
                        currents[i] = double.Parse(currs[i]);
                    }
                }
            }

            return currents;
        }


        private bool WriteCmd(string cmd)
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

        private string ReadCmd()
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
