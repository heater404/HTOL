using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static HTOL.Enums;

namespace HTOL.Converters
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SiteStatus status))
                return Binding.DoNothing;

            switch (status)
            {
                case SiteStatus.Stop:
                default:
                    return new SolidColorBrush(new Color() { A = 0xff, R = 0x1E, G = 0x1E, B = 0x1E });

                case SiteStatus.Run:
                    return Brushes.Green;

                case SiteStatus.Error:
                    return Brushes.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}