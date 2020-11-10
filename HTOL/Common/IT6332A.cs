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
    public class IT6332A : Instrument
    {
        public IT6332A()
        {
            this.ResourceName = "USB0::0xFFFF::0x6300::802071092757510163::INSTR";
        }
        /// <summary>
        /// 根据通道查询电压值
        /// </summary>
        /// <param name="channel">通道，是一个预定义好的枚举类型</param>
        /// <returns>电压值 单位V</returns>
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

        /// <summary>
        /// 根据通道查询电流值 单位A
        /// </summary>
        /// <param name="channel">通道，是一个预定义好的枚举类型</param>
        /// <returns>电流值 单位A</returns>
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

        /// <summary>
        /// 查询所有通道的电压值
        /// </summary>
        /// <returns>所有通道的电压值 单位V</returns>
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

        /// <summary>
        /// 查询所有通道的电流值
        /// </summary>
        /// <returns>电流值数组 单位A</returns>
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
    }
}
