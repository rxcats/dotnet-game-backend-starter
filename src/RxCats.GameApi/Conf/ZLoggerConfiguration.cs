using System.Buffers;
using Cysharp.Text;
using ZLogger;

namespace RxCats.GameApi.Conf;

public static class ZLoggerConfiguration
{
    private static readonly Utf8PreparedFormat<string, LogLevel, string> LogPrefix
        = ZString.PrepareUtf8<string, LogLevel, string>("{0} {1} --- {2}    : ");

    private const int LogRollSizeKb = 1024 * 1024 * 10;

    private static void LogFormatter(IBufferWriter<byte> writer, LogInfo info) =>
        LogPrefix.FormatTo(ref writer,
            info.Timestamp.DateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), info.LogLevel,
            info.CategoryName);

    public static void ApplyFullOptions(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Trace);
        builder.AddZLoggerConsole(options => { options.PrefixFormatter = LogFormatter; });
        builder.AddZLoggerFile("logs/app.log", options => { options.PrefixFormatter = LogFormatter; });
        builder.AddZLoggerRollingFile((dt, number) => $"logs/app.{dt.ToLocalTime():yyyyMMdd}.{number:000}.log",
            x => x.ToLocalTime().Date,
            LogRollSizeKb,
            options => { options.PrefixFormatter = LogFormatter; });
    }

    public static void ApplyConsoleOptions(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Trace);
        builder.AddZLoggerConsole(options => { options.PrefixFormatter = LogFormatter; });
    }
}