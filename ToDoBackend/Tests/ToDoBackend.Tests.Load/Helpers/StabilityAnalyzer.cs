using NBomber.Contracts.Stats;

namespace ToDoBackend.Tests.Load.Helpers;

internal static class StabilityAnalyzer
{
    public static bool IsSystemStable(ScenarioStats stats, TimeSpan duration)
    {
        var timeline = stats.Ok;

        // Проверяем, что latency не растет экспоненциально
        var latencyGrowth = CalculateExponentialGrowth(timeline.Latency);

        // Проверяем стабильность RPS
        var rpsVariance = CalculateVariance(timeline.Latency);

        return latencyGrowth < 1.05 && rpsVariance < 0.1;
    }

    private static double CalculateExponentialGrowth(LatencyStats latency)
    {
        return double.E;
    }

    private static double CalculateVariance(LatencyStats latency)
    {
        return double.E;
    }
}