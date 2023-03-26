using Cmd.Terminal;
using Cmd.Terminal.Debugger.Monitoring;
using NLog;
using NLog.Config;

namespace Server.Debugger
{
    public static class RecastDebug
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static Telemetry Telemetry { get; } = new Telemetry();

        public static void Init()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            TelemetryCommand command = new TelemetryCommand(Telemetry);
            command.FileName = $"Telemetry-VER-{1}";
            Terminal.AddCommand(command);
        }
        
        public static void Log(string info) => logger.Debug(info);
        public static void LogWarning(string info) => logger.Warn(info);
        public static void LogError(string info) => logger.Error(info);
    }
}
