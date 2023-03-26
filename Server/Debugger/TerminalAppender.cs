using Cmd.Terminal;
using Cmd.Terminal.Debugger.Logger;
using NLog;
using NLog.Targets;

namespace Server.Debugger
{
    [Target("TerminalAppender")]
    public class TerminalAppender : TargetWithLayout
    {
        private TerminalLogger terminalLogger = new();
        public TerminalAppender()
        {
            terminalLogger.Format = "%message";
            Terminal.AddCommand(new TerminalLoggerCommand(terminalLogger));
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var message = base.RenderLogEvent(base.Layout, logEvent);

            if (LogLevel.Debug == logEvent.Level) { terminalLogger.Debug(message); }
            else if (LogLevel.Info == logEvent.Level) { terminalLogger.Info(message); }
            else if (LogLevel.Warn == logEvent.Level) { terminalLogger.Warn(message); }
            else if (LogLevel.Error == logEvent.Level) { terminalLogger.Error(message); }
            else if (LogLevel.Fatal == logEvent.Level) { terminalLogger.Fatal(message); }
            else { terminalLogger.Info(message); }
        }
    }
}