﻿using HandyWMS_ClassLibrary.Enum.Browser;
using HandyWMS_ClassLibrary.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_ClassLibrary.Utils.NetUtils
{
    public class BrowserHelper
    {
        public static string GetBrwoserInfo(string userAgent)
        {
            var ie = new InternetExplorer(userAgent);
            if (ie.Type == BrowserType.IE)
                return string.Format("{0} {1}", ie.Type.ToString(), ie.Version);
            var firefox = new Firefox(userAgent);
            if (firefox.Type == BrowserType.Firefox)
                return string.Format("{0} {1}", firefox.Type.ToString(), firefox.Version);
            var edge = new Edge(userAgent);
            if (edge.Type == BrowserType.Edge)
                return string.Format("{0} {1}", edge.Type.ToString(), edge.Version);
            var opera = new Opera(userAgent);
            if (opera.Type == BrowserType.Opera)
                return string.Format("{0} {1}", opera.Type.ToString(), opera.Version);
            var chrome = new Chrome(userAgent);
            if (chrome.Type == BrowserType.Chrome)
                return string.Format("{0} {1}", chrome.Type.ToString(), chrome.Version);
            var safari = new Safari(userAgent);
            if (safari.Type == BrowserType.Safari)
                return string.Format("{0} {1}", safari.Type.ToString(), safari.Version);
            return string.Empty;
        }
    }
}
