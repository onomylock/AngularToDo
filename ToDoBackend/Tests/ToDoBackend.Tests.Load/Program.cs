using ToDoBackend.Tests.Load.Scenarios;

namespace ToDoBackend.Tests.Load;

public static class Program
{
    public static void Main()
    {
        var scenario = new ToDoItemScenario();
        scenario.Run();
    }
}