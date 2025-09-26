using Microsoft.Extensions.Configuration;

namespace ToDoBackend.Tests.Load;

[SetUpFixture]
public class GlobalSetup
{
    public static IConfiguration Configuration { get; private set; }

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(TestContext.CurrentContext.TestDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }
}