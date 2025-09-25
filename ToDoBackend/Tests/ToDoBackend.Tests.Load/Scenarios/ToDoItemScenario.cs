using NBomber.CSharp;
using NBomber.Http;
using NBomber.Http.CSharp;
using NBomber.Plugins.Network.Ping;
using NBomber.Sinks.Timescale;
using ToDoBackend.Application.Models.Dto.ToDoItems.Request;

namespace ToDoBackend.Tests.Load.Scenarios;

public class ToDoItemScenario
{
    public void Run()
    {
        var httpClient = Http.CreateDefaultClient();

        var config = new TimescaleDbSinkConfig(connectionString: "");
        var timescaleDb = new TimescaleDbSink(config);

        
        var scenario = Scenario.Create("http_scenario", async context =>
            {
                var requestBody = new GetToDoItemsRequest() { Ids = [1, 2, 3] };
                var request =
                    Http.CreateRequest("GET", "http://localhost:5034/ToDoItem/GetToDoItems?Ids=1&Ids=2&Ids=3")
                        .WithHeader("Content-Type", "application/json");
                 //.WithBody(new StringContent("{ some JSON }", Encoding.UTF8, "application/json"));
                var response = await Http.Send(httpClient, request);

                return response;
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(3))
            .WithLoadSimulations(
                Simulation.RampingInject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.RampingInject(rate: 0, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1))
            );

        NBomberRunner
            .RegisterScenarios(scenario)
            // .WithWorkerPlugins(
            //     new PingPlugin(PingPluginConfig.CreateDefault("nbomber.com")),
            //     new HttpMetricsPlugin([HttpVersion.Version1])
            // )
            .WithReportingSinks(timescaleDb)
            .Run();
    }
}