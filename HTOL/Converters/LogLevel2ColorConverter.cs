using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static HTOL.Enums;

#region File Information

/* ----------------------------------------------------------------------
* File Name: LogLevel2ColorConverter
* Create Author: ZDK
* Create DateTime: 4/9/2020 6:11:00 PM
* Description:
*----------------------------------------------------------------------*/

#endregion File Information

namespace HTOL.Converters
{
    public class LogLevel2ColorConverter : IValueConverter
    {
        private static SolidColorBrush Error = new SolidColorBrush(new Color { R = 0xff, G = 0x00, B = 0x00, A = 0xFF });
        private static SolidColorBrush Warning = new SolidColorBrush(new Color { R = 0xf0, G = 0xb4, B = 0x00, A = 0xFF });
        private static SolidColorBrush Info = new SolidColorBrush(new Color { R = 0x00, G = 0x00, B = 0x00, A = 0xFF });

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LogType level)
            {
                switch (level)
                {
                    case LogType.Error:
                        return Error;

                    case LogType.Warning:
                        return Warning;

                    case LogType.Info:
                        return Info;

                    default:
                        return Warning;
                }
            }
            return Warning;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}