using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTOL.Common
{
    public class IT6942A : Instrument
    {
        public IT6942A()
        {
            this.ResourceName="USB0::0x2EC7::0x6900::800064020757210042::INSTR";
        }

        /// <summary>
        /// 查询电压值
        /// </summary>
        /// <returns>电压值 单位V</returns>
        public double QueryVoltage()
        {
            double voltage = 0;

            if (WriteCmd("MEAS:VOLT?"))//读取当前通道的电压
            {
                string vol = ReadCmd();//读返回值
                if (!string.IsNullOrEmpty(vol))
                {
                    voltage = double.Parse(vol);
                }
            }
            return voltage;
        }

        /// <summary>
        /// 查询电流值 单位A
        /// </summary>
        /// <returns>电流值 单位A</returns>
        public double QueryCurrent()
        {
            double current = 0;

            if (WriteCmd("MEAS:CURR?"))//读取当前通道的电流
            {
                string vol = ReadCmd();//读返回值
                if (!string.IsNullOrEmpty(vol))
                {
                    current = double.Parse(vol);
                }
            }
            return current;
        }
    }
}
