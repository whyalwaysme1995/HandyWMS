using HandyWMS_Client.Constructs.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HandyWMS_Client.Objects.Base
{
    class WMSTableRow
    {
        public int Index { get; set; }
        public int Width { get; set; }
        public ContentTypeEnum Type { get; set; }
        public HorizontalAlignment Align { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public TextTrimming TextTrimming { get; set; }
        public bool Enable { get; set; }
        public bool Display { get; set; }
    }
}
