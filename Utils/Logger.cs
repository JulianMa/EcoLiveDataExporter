using Eco.Shared.Localization;
using Eco.Shared.Utils;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class Logger
    {
        public static void Debug(string message)
        {
            if (Config.Data.Debug)
                LogMessage("DEBUG", message);
        }

        public static void Info(string message)
        {
            LogMessage("INFO", message);
        }

        public static void Error(string message)
        {
            LogMessage("Error", message);
        }

        private static void LogMessage(string type, string message) => Log.Write(new LocString($"[LiveData] {type}: {message}\n"));
    }
}
