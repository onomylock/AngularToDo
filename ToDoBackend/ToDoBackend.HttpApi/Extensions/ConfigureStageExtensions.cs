using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Settings.Configuration;
using ToDoBackend.Infrastructure.Data;

namespace ToDoBackend.HttpApi.Extensions;

internal static class ConfigureStageExtensions
{
    internal static void ConfigureMiddlewarePipeline(IApplicationBuilder applicationBuilder,
        IHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsDevelopment())
        {
            Log.Information("Add Swagger & SwaggerUI");
            applicationBuilder.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }
        else
        {
            applicationBuilder.UseHsts();
        }

        applicationBuilder.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate =
                    "[{httpContextTraceIdentifier}] {httpContextRequestProtocol} {httpContextRequestMethod} {httpContextRequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("httpContextTraceIdentifier",
                        Activity.Current?.Id ?? httpContext.TraceIdentifier);
                    diagnosticContext.Set("httpContextConnectionId", httpContext.Connection.Id);
                    diagnosticContext.Set("httpContextConnectionRemoteIpAddress",
                        httpContext.Connection.RemoteIpAddress);
                    diagnosticContext.Set("httpContextConnectionRemotePort", httpContext.Connection.RemotePort);
                    diagnosticContext.Set("httpContextRequestHost", httpContext.Request.Host);
                    diagnosticContext.Set("httpContextRequestPath", httpContext.Request.Path);
                    diagnosticContext.Set("httpContextRequestProtocol", httpContext.Request.Protocol);
                    diagnosticContext.Set("httpContextRequestIsHttps", httpContext.Request.IsHttps);
                    diagnosticContext.Set("httpContextRequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("httpContextRequestMethod", httpContext.Request.Method);
                    diagnosticContext.Set("httpContextRequestContentType", httpContext.Request.ContentType);
                    diagnosticContext.Set("httpContextRequestContentLength", httpContext.Request.ContentLength);
                    diagnosticContext.Set("httpContextRequestQueryString", httpContext.Request.QueryString);
                    diagnosticContext.Set("httpContextRequestQuery", httpContext.Request.Query);
                    diagnosticContext.Set("httpContextRequestHeaders", httpContext.Request.Headers);
                    diagnosticContext.Set("httpContextRequestCookies", httpContext.Request.Cookies);
                };
            })
            //All unhandled exception are processed by this Controller
            .UseExceptionHandler("/Error")
            .UseRouting()
            .UseResponseCaching()
            .UseCors()
            .UseWebSockets()
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    public static async Task EnsureDbContextsCreated(IServiceProvider serviceProvider)
    {
        var appDbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ToDoDbContext>>();
        await using var appDbContext = await appDbContextFactory.CreateDbContextAsync();
        await appDbContext.Database.EnsureCreatedAsync();
        await appDbContext.Database.MigrateAsync();
    }

    public static void InitBootstrapLogger()
    {
        var ms = new MemoryStream();

        const string json = """
                            {
                              "Serilog": {
                                "Using": [
                                  "Serilog.Sinks.Console",
                                  "Serilog.Sinks.File",
                                  "Serilog.Enrichers.AssemblyName",
                                  "Serilog.Enrichers.Environment",
                                  "Serilog.Enrichers.GlobalLogContext",
                                  "Serilog.Enrichers.Memory",
                                  "Serilog.Enrichers.Process",
                                  "Serilog.Enrichers.Thread"
                                ],
                                "Enrich": [
                                  "WithSpan",
                                  "FromLogContext",
                                  "WithAssemblyName",
                                  "WithEnvironmentName",
                                  "WithMachineName",
                                  "WithMemoryUsage",
                                  "WithProcessId",
                                  "WithProcessName",
                                  "WithThreadId",
                                  "WithThreadName"
                                ],
                                "WriteTo": [
                                  {
                                    "Name": "Console",
                                    "Args": {
                                      "outputTemplate": "{Timestamp:o} [{Level:u3}] ({AssemblyName}/{MachineName}/{ThreadId}) {Message}{NewLine}{Exception}",
                                      "restrictedToMinimumLevel": "Information"
                                    }
                                  },
                                  {
                                    "Name": "File",
                                    "Args": {
                                      "path": "logs/Log_.log",
                                      "outputTemplate": "{Timestamp:o} [{Level:u3}] ({AssemblyName}/{MachineName}/{ThreadId}) {Message:j}{NewLine}{Exception}",
                                      "restrictedToMinimumLevel": "Information",
                                      "rollingInterval": "Day",
                                      "Shared": true,
                                      "retainedFileCountLimit": 7
                                    }
                                  }
                                ],
                                "MinimumLevel": {
                                  "Default": "Information",
                                  "Override": {
                                    "Microsoft": "Warning",
                                    "System.Net.Http.HttpClient": "Warning"
                                  }
                                }
                              }
                            }
                            """;
        ms.Write(Encoding.UTF8.GetBytes(json));
        ms.Flush();
        ms.Seek(0, SeekOrigin.Begin);

        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(ms)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationRoot, new ConfigurationReaderOptions { SectionName = "Serilog" })
            .CreateBootstrapLogger();
    }
}