using HTOL.Common;
using HTOL.Model;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Threading;

namespace HTOL.ViewModel
{
    public class GraphViewModel : BaseModel
    {
        public Func<double, string> DateTimeFormatter { get; set; } = value => new DateTime((long)value).ToString("HH:mm:ss");
        public Func<double, string> ValueFormatter { get; set; } = value => value.ToString("0.000");
        public double AxisXStep { get; set; } = TimeSpan.FromMinutes(5).Ticks;

        private readonly Communication comm = Communication.Instacne;
        private IT6332A t6332A = null;
        private IT6942A t6942A = null;
        private double axisXMax;

        public double AxisXMax
        {
            get { return axisXMax; }
            set { axisXMax = value; RaisePropertyChanged(); }
        }

        private double axisXMin;

        public double AxisXMin
        {
            get { return axisXMin; }
            set { axisXMin = value; RaisePropertyChanged(); }
        }

        public ChartValues<GraphModel> Voltages { get; set; }
        public ChartValues<GraphModel> Currents { get; set; }
        public ChartValues<GraphModel> Temps { get; set; }

        public GraphViewModel()
        {
            Communication.Instacne.RecvTempHandler += RecvTempHandler;
            t6332A = new IT6332A();
            t6942A = new IT6942A();

            var mapper = Mappers.Xy<GraphModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<GraphModel>(mapper);
            Voltages = new ChartValues<GraphModel>();
            Currents = new ChartValues<GraphModel>();
            Temps = new ChartValues<GraphModel>();
            SetAxisLimits(DateTime.Now);
        }

        private void RecvTempHandler(object sender, float e)
        {
            var now = DateTime.Now;
            Temps.Add(new GraphModel
            {
                DateTime = now,
                Value = e
            });

            csvHelper.WirteLine(new string[] { now.ToString("MMdd:HH:mm:ss"), e.ToString("0.000") });

            SetAxisLimits(now);
            //lets only use the last 15 values
            if (Temps.Count > 120) Temps.RemoveAt(0);
        }

        private bool isMonitorInstrument = false;

        public void StopMonitorInstrument()
        {
            isMonitorInstrument = false;
            t6332A.Close();
            t6942A.Close();
        }

        /// <summary>
        /// 监控仪表电压和电流
        /// </summary>
        /// <param name="timeout">查询时间间隔，单位ms</param>
        public void MonitorInstrument(int timeout, DateTime time)
        {
            Voltages.Clear();
            Currents.Clear();

            if (!t6332A.Open())
                return;

            if (!t6942A.Open())
                return;

            CsvHelper csvHelper = new CsvHelper($@"D:\HTOL\{time:yyyyMMddHHmmss}\InstrumentData.csv");
            csvHelper.WirteLine(new string[] { "Time(MMdd:HH:mm:ss)", "CH1_Voltage(V)", "CH2_Voltage(V)", "CH1_Current(A)", "CH2_Current(A)", "Voltage(V)", "Current(A)" });

            isMonitorInstrument = true;
            while (isMonitorInstrument)
            {
                var now = DateTime.Now;

                double[] voltages = t6332A.QueryVoltage();
                double voltage = t6942A.QueryVoltage();
                Voltages.Add(new GraphModel
                {
                    DateTime = now,
                    Value = voltage
                });

                double[] currents = t6332A.QueryCurrent();
                double current = t6942A.QueryCurrent();
                Currents.Add(new GraphModel
                {
                    DateTime = now,
                    Value = current
                });

                csvHelper.WirteLine(new string[]
                { now.ToString("MMdd:HH:mm:ss"),
                    voltages[0].ToString("0.000"),
                    voltages[1].ToString("0.000"),
                    currents[0].ToString("0.000"),
                    currents[1].ToString("0.000"),
                    voltage.ToString("0.000"),
                    current.ToString("0.000"),
                });

                SetAxisLimits(now);
                //lets only use the last 15 values
                if (Voltages.Count > 120) Voltages.RemoveAt(0);
                if (Currents.Count > 120) Currents.RemoveAt(0);
                Thread.Sleep(timeout);
            }
        }

        private bool isMonitorTemp = false;

        public void StopMonitorTemp()
        {
            isMonitorTemp = false;
        }

        private CsvHelper csvHelper = null;

        /// <summary>
        /// 监控温度方法
        /// </summary>
        /// <param name="timeout">查询时间间隔 单位毫秒</param>
        public void MonitorTemp(int timeout, DateTime time)
        {
            Temps.Clear();
            isMonitorTemp = true;

            csvHelper = new CsvHelper($@"D:\HTOL\{time:yyyyMMddHHmmss}\M92Data_{DateTime.Now}.csv");
            csvHelper.WirteLine(new string[] { "Time(MMdd:HH:mm:ss)", "Temp(℃)" });

            while (isMonitorTemp)
            {
                comm.CheckTemp();
                Thread.Sleep(timeout);
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisXMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisXMin = now.Ticks - TimeSpan.FromHours(1).Ticks; // and 8 seconds behind
        }
    }
}