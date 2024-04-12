using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HandyWMS_Client.Constructs.SystemEnums
{
    public enum ContentTypeEnum
    {
        [Description("字符串")]
        STRING = 0,
        [Description("数字")]
        DOUBLE = 1,
        [Description("是否")]
        BOOL = 2,
        [Description("单项选择")]
        SELECT_ONE = 3,
        [Description("多项选择")]
        SELECT_MANY = 4,
        [Description("日期")]
        DATE = 5,
        [Description("文件")]
        FILE = 6
    }

    public static class ContentTypeEnumUtil
    {
        
    }
}
