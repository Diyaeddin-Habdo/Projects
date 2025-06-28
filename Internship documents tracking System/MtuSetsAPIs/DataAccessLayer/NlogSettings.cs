using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    using NLog;

    public class NlogSettings
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // Error seviyesinde log kaydetme
        public static void LogError(Exception exception, string message)
        {
            Logger.Error(exception, message);
        }

        // Warning seviyesinde log kaydetme
        public static void LogWarning(string message)
        {
            Logger.Warn(message);
        }

        // Info seviyesinde log kaydetme
        public static void LogInfo(string message)
        {
            Logger.Info(message);
        }

        // Debug seviyesinde log kaydetme
        public static void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        // Trace seviyesinde log kaydetme
        public static void LogTrace(string message)
        {
            Logger.Trace(message);
        }

        // Fatal seviyesinde log kaydetme
        public static void LogFatal(Exception exception, string message)
        {
            Logger.Fatal(exception, message);
        }
    }

}
