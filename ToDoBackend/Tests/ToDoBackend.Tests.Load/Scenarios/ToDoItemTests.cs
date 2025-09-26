using NBomber.CSharp;
using NBomber.Http.CSharp;
using ToDoBackend.Tests.Load.Helpers;

namespace ToDoBackend.Tests.Load.Scenarios;

[TestFixture]
public class ToDoItemTests
{
    private Uri _apiBaseUrl;
    private string _fullApi;

    [SetUp]
    public void Setup()
    {
        _apiBaseUrl = new Uri(GlobalSetup.Configuration["TestSettings:ApiBaseUrl"] ?? string.Empty);
    }

    [Test]
    public void ToDoItem_MultiStage_ClosedModel_Test()
    {
        _fullApi = new Uri(_apiBaseUrl, "ToDoItem/GetToDoItems?Ids=1&Ids=2&Ids=3").AbsoluteUri;
        var httpClient = new HttpClient();

        var scenario = Scenario.Create("ToDoItem_MultiStage_Test", async _ =>
            {
                // Добавляем небольшую задержку для имитации "думания" пользователя
                await Task.Delay(TimeSpan.FromMilliseconds(100));

                var request = Http.CreateRequest("GET", _fullApi);

                var response = await Http.Send(httpClient, request);

                return response;
            })
            .WithLoadSimulations(
                // Многостадийный тест с Closed Model
                Simulation.RampingConstant( // Стадия 1: Постепенный рост
                    50,
                    TimeSpan.FromMinutes(1)
                ),
                Simulation.KeepConstant( // Стадия 2: Постоянная нагрузка
                    50,
                    TimeSpan.FromMinutes(3)
                ),
                Simulation.RampingConstant( // Стадия 3: Постепенный спад нагрузки
                    0,
                    TimeSpan.FromMinutes(1)
                )
            );

        var result = NBomberRunner.RegisterScenarios(scenario).WithDefaultParameters().Run();

        httpClient.Dispose();

        // Проверки
        var scenarioStats = result.ScenarioStats.First(x => x.ScenarioName == "ToDoItem_MultiStage_Test");
        var okStats = scenarioStats.Ok.Request;

        Assert.Multiple(() =>
        {
            Assert.That(scenarioStats.Fail.Request.Count, Is.EqualTo(0),
                $"Не должно быть ошибок. Найдено: {scenarioStats.Fail.Request.Count}");

            Assert.That(okStats.Percent, Is.LessThan(2000),
                "99% запросов должны быть быстрее 2000 мс");

            Assert.That(okStats.RPS, Is.GreaterThan(5),
                $"RPS должен быть больше 5. Текущий: {okStats.RPS:F2}");
        });
    }

    [Test]
    [TestCase(10)]
    [TestCase(25)]
    [TestCase(50)]
    [TestCase(100)]
    public void ToDoItem_Different_User_Loads_Test(int userCount)
    {
        // Тестируем с разным количеством пользователей
        var httpClient = new HttpClient();
        _fullApi = new Uri(_apiBaseUrl, "ToDoItem/GetToDoItems?Ids=1&Ids=2&Ids=3").AbsoluteUri;
        var scenario = Scenario.Create($"ToDoItem_{userCount}Users_Test", async _ =>
            {
                var request = Http.CreateRequest("GET", _fullApi);

                var response = await Http.Send(httpClient, request);

                return response;
            })
            .WithLoadSimulations(
                Simulation.KeepConstant(
                    userCount,
                    TimeSpan.FromMinutes(2)
                )
            );

        var result = NBomberRunner.RegisterScenarios(scenario).Run();
        var stats = result.ScenarioStats.First(x => x.ScenarioName == $"ToDoItem_{userCount}Users_Test");
        var redFlags = RedFlagDetector.DetectRedFlags(stats);

        httpClient.Dispose();

        Console.WriteLine($"\n=== Результаты для {userCount} пользователей ===");
        Console.WriteLine($"RPS: {stats.Ok.Request.RPS:F2}");
        Console.WriteLine($"Среднее время: {stats.Ok.Latency.MeanMs:F2} мс");
        Console.WriteLine($"Ошибок: {stats.Fail.Request.Count}");
        redFlags.ForEach(x => Console.WriteLine($"<UNK>: {x}"));

        // Проверка для каждого уровня нагрузки

        Assert.That(stats.Fail.Request.Count, Is.EqualTo(0),
            $"Для {userCount} пользователей не должно быть ошибок");
    }
}