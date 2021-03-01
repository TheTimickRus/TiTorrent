using System;
using System.IO;
using Serilog;
using Serilog.Core;

namespace TiTorrent.UWP.Helpers
{
    public class Log
    {
        private static Logger _instance;
        public static Logger Instance => _instance ??= GetLogger();

        private static Logger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(Path.Combine(AppState.LogDirectory, $"Log ({DateTime.Now.ToString("G").Replace(':', '-')}).lg"))
                .CreateLogger();
        }
    }
}
