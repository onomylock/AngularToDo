using NBomber.Contracts.Stats;

namespace ToDoBackend.Tests.Load.Helpers;

internal static class RedFlagDetector
{
    private const int ExpectedRps = 3000;

    public static List<string> DetectRedFlags(ScenarioStats stats)
    {
        var redFlags = new List<string>();

        if (stats.Fail.Request.Count > stats.AllRequestCount * 0.01)
            redFlags.Add("Error rate > 1%");

        if (stats.Ok.Latency.Percent95 > 1000)
            redFlags.Add("95th percentile latency > 1s");

        if (stats.Ok.Request.RPS < ExpectedRps * 0.8)
            redFlags.Add("RPS ниже ожидаемого на 20%+");

        // if (IsMemoryLeakDetected(stats))
        //     redFlags.Add("Обнаружена утечка памяти");

        return redFlags;
    }
}