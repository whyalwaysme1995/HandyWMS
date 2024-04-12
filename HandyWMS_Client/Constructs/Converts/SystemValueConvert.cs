using HandyWMS_Client.Constructs.SystemEnums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HandyWMS_Client.Constructs.Converts
{
    internal class SystemValueConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter)
            {
                case ContentTypeEnum.BOOL:
                    return (bool)value?  '是' : '否';
                case ContentTypeEnum.DATE:
                    return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter)
            {
                case ContentTypeEnum.BOOL:
                    return value.Equals('是') ? true : false;
                case ContentTypeEnum.DATE:
                    return DateTime.Parse((string)value);
                default:
                    return value;
            }
        }
    }
}
