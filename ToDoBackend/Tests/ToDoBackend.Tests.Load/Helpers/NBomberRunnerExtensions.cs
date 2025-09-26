using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace ToDoBackend.Tests.Load.Helpers;

internal static class NBomberRunnerExtensions
{
    public static NBomberContext WithDefaultParameters(this NBomberContext context)
    {
        return context.LoadConfig("config.json")
            .WithReportFormats(ReportFormat.Txt, ReportFormat.Html)
            .WithReportingInterval(TimeSpan.FromSeconds(10))
            .DisplayConsoleMetrics(true);
    }
}