using HTOL.Model;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HTOL.ViewModel
{
    public class GraphViewModel : BaseModel
    {
        public Func<double, string> DateTimeFormatter { get; set; } = value => new DateTime((long)value).ToString("mm:ss");

        public double AxisXStep { get; set; } = TimeSpan.FromSeconds(5).Ticks;

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
            var mapper = Mappers.Xy<GraphModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<GraphModel>(mapper);
            Voltages = new ChartValues<GraphModel>();
            Currents = new ChartValues<GraphModel>();
            Temps = new ChartValues<GraphModel>();
            SetAxisLimits(DateTime.Now);
            Task.Run(Read);
        }

        private void Read()
        {
            var r = new Random();
            double _trend;
            while (true)
            {
                var now = DateTime.Now;

                _trend = r.NextDouble() * 20;
                Voltages.Add(new GraphModel
                {
                    DateTime = now,
                    Value = _trend
                });

                _trend = r.NextDouble() * 20;
                Currents.Add(new GraphModel
                {
                    DateTime = now,
                    Value = _trend
                });

                _trend = r.NextDouble() * 50;
                Temps.Add(new GraphModel
                {
                    DateTime = now,
                    Value = _trend
                });

                SetAxisLimits(now);
                //lets only use the last 15 values
                if (Voltages.Count > 15) Voltages.RemoveAt(0);
                if (Currents.Count > 15) Currents.RemoveAt(0);
                if (Temps.Count > 15) Temps.RemoveAt(0);
                Thread.Sleep(5000);
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisXMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisXMin = now.Ticks - TimeSpan.FromSeconds(60).Ticks; // and 8 seconds behind
        }
    }
}