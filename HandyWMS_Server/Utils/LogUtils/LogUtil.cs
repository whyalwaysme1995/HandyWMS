using HandyWMS_ClassLibrary.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_Server.Utils.LogUtils
{
    public class LogUtil
    {

        public static void Trace(object msg, Exception ex = null)
        {
            Info(msg, ex);
        }

        public static void Debug(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                Log.Debug(msg.ParseToString());
            }
            else
            {
                Log.Debug(msg + GetExceptionMessage(ex));
            }
        }

        public static void Info(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                Log.Information(msg.ParseToString());
            }
            else
            {
                Log.Information(msg + GetExceptionMessage(ex));
            }
        }

        public static void Warn(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                Log.Warning(msg.ParseToString());
            }
            else
            {
                Log.Warning(msg + GetExceptionMessage(ex));
            }
        }

        public static void Error(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                Log.Error(msg.ParseToString());
            }
            else
            {
                Log.Error(msg + GetExceptionMessage(ex));
            }
        }

        public static void Error(Exception ex)
        {
            if (ex != null)
            {
                Log.Error(GetExceptionMessage(ex));
            }
        }

        public static void Fatal(object msg, Exception ex = null)
        {
            if (ex == null)
            {
                Log.Fatal(msg.ParseToString());
            }
            else
            {
                Log.Fatal(msg + GetExceptionMessage(ex));
            }
        }

        public static void Fatal(Exception ex)
        {
            if (ex != null)
            {
                Log.Fatal(GetExceptionMessage(ex));
            }
        }

        private static string GetExceptionMessage(Exception ex)
        {
            string message = string.Empty;
            if (ex != null)
            {
                message += ex.Message;
                message += Environment.NewLine;
                Exception originalException = ex.GetOriginalException();
                if (originalException != null)
                {
                    if (originalException.Message != ex.Message)
                    {
                        message += originalException.Message;
                        message += Environment.NewLine;
                    }
                }
                message += ex.StackTrace;
                message += Environment.NewLine;
            }
            return message;
        }
    }
}
