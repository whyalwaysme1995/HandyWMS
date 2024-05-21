using HandyWMS_ClassLibrary.Params.SystemManage;
using System;
using System.Collections.Generic;

namespace HandyWMS_ClassLibrary.Params
{
    public class NewsParam : BaseAreaParam
    {
        public string NewsTitle { get; set; }
        public int? NewsType { get; set; }
        public string NewsTag { get; set; }
    }
}
