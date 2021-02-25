using System;
using Serilog;
using Serilog.Core;
using System.IO;

namespace TiTorrent.Shared
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
