using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HandyWMS_ClassLibrary.Extensions;
using HandyWMS_ClassLibrary.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HandyWMS_ClassLibrary.Utils.NetUtils
{
    public class NetHelper
    {
        public static HttpContext HttpContext
        {
            get { return GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>().HttpContext; }
        }

        public static string Ip
        {
            get
            {
                string result = string.Empty;
                if (HttpContext != null)
                {
                    result = GetWebClientIp();
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = GetLanIp();
                }
                return result;
            }
        }

        private static string GetWebClientIp()
        {
            string ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }

        public static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }

        public static string GetWanIp()
        {
            string ip = string.Empty;
            string url = "http://www.net.cn/static/customercare/yourip.asp";
            string html = HttpHelper.HttpGet(url);
            if (!string.IsNullOrEmpty(html))
            {
                ip = HtmlHelper.Resove(html, "<h2>", "</h2>");
            }
            return ip;
        }

        private static string GetWebRemoteIp()
        {
            string ip = HttpContext?.Connection?.RemoteIpAddress.ParseToString();
            if (HttpContext != null && HttpContext.Request != null)
            {
                if (HttpContext.Request.Headers.ContainsKey("X-Real-IP"))
                {
                    ip = HttpContext.Request.Headers["X-Real-IP"].ToString();
                }

                if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    ip = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                }
            }
            return ip;
        }

        public static string Browser
        {
            get
            {
                var browser = HttpContext.Request.Headers["User-Agent"];
                var agent = UserAgent.ParseToString();
                return BrowserHelper.GetBrwoserInfo(agent);
            }
        }

        public static string UserAgent
        {
            get
            {
                string userAgent = string.Empty;
                userAgent = HttpContext?.Request?.Headers["User-Agent"];
                return userAgent;
            }
        }

        public static string GetOSVersion()
        {
            var osVersion = string.Empty;
            var userAgent = UserAgent;
            if (userAgent.Contains("NT 10"))
            {
                osVersion = "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Android"))
            {
                osVersion = "Android";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }
    }
}
