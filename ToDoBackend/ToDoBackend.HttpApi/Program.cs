using Serilog;
using Serilog.Settings.Configuration;
using ToDoBackend.HttpApi.Extensions;

ConfigureStageExtensions.InitBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, _, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration, new ConfigurationReaderOptions { SectionName = "Serilog" });
});

builder.InitCustom();

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Environment: {EnvironmentName}", app.Environment.EnvironmentName);

await ConfigureStageExtensions.EnsureDbContextsCreated(app.Services);
ConfigureStageExtensions.ConfigureMiddlewarePipeline(app, app.Environment);

logger.LogInformation("App successfully started!");

try
{
    logger.LogInformation("Application running at: {Urls}", app.Configuration["ASPNETCORE_URLS"]);
    app.Run();
    logger.LogInformation("Application stopping");
}
catch (Exception e)
{
    logger.LogCritical(e, "An unhandled exception occured during bootstrapping!");
}
finally
{
    logger.LogInformation("Flushing logs");
    Log.CloseAndFlush();
}