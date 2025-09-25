using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using Common.Common.BinderProviders;
using Common.Common.Converters;
using Common.Common.Enums;
using Common.Common.Filters;
using Common.Common.InputFormatters;
using Common.Common.Models;
using Common.Common.Services;
using Common.Domain.Data;
using Common.Domain.Repository.Base;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Options;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Infrastructure.Data;
using ToDoBackend.Infrastructure.Handlers;
using ToDoBackend.Infrastructure.Services.Data;

namespace ToDoBackend.HttpApi.Extensions;

internal static class ConfigureServicesExtensions
{
    public static void InitCustom(this WebApplicationBuilder builder)
    {
        builder.Services
            .ConfigureDiOptions(builder.Configuration)
            .ConfigureDiToDoDbContext(builder.Configuration, builder.Environment)
            .AddHttpContextAccessor()
            .AddHttpClient()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Description = "An API of ASP.NET Core"
                });

                c.EnableAnnotations();
            })
            .ConfigureDiRepositories()
            .ConfigureDiServices()
            .ConfigureDiHandlers()
            .AddCors(options =>
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true)
                        .WithOrigins(
                            "http://localhost:4200/",
                            "http://localhost:8080/"
                        )
                        .WithExposedHeaders("Content-Disposition");
                })
            )
            .ConfigureHttp();
    }

    private static IServiceCollection ConfigureDiOptions(this IServiceCollection serviceCollection,
        IConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile("appsettings.json", false, false)
            .AddDotNetEnv("to_do.env", LoadOptions.TraversePath())
            .AddUserSecrets(Assembly.GetExecutingAssembly());

        serviceCollection.Configure<ToDoHttpApiOptions>(configurationManager.GetSection("ToDoHttpApiOptions"));

        return serviceCollection;
    }

    private static IServiceCollection ConfigureDiRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

        return serviceCollection;
    }

    private static IServiceCollection ConfigureDiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWarningService, WarningService>();


        serviceCollection.AddScoped<IToDoItemGroupEntityService, ToDoItemGroupEntityService>();
        serviceCollection.AddScoped<IToDoItemEntityService, ToDoItemEntityService>();
        serviceCollection.AddScoped<IUserEntityService, UserEntityService>();
        serviceCollection
            .AddScoped<IUserToToDoItemGroupMappingEntityService, UserToToDoItemGroupMappingEntityService>();

        return serviceCollection;
    }

    private static IServiceCollection ConfigureDiHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IToDoItemGroupHandler, ToDoItemGroupHandler>();
        serviceCollection.AddScoped<IToDoItemHandler, ToDoItemHandler>();
        serviceCollection.AddScoped<IUserHandler, UserHandler>();

        return serviceCollection;
    }

    private static IServiceCollection ConfigureDiToDoDbContext(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
        IHostEnvironment env
    )
    {
        void NodeDbContextOptionsBuilder(DbContextOptionsBuilder options)
        {
            //https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/database-errors#locking-retries-and-timeouts
            //https://github.com/dotnet/efcore/issues/28135
            //https://www.sqlite.org/wal.html

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            options.UseSqlite(connectionString, sqliteOptionsAction =>
            {
                sqliteOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                sqliteOptionsAction.CommandTimeout(300);
            });

            if (env.IsDevelopment()) options.EnableSensitiveDataLogging().EnableDetailedErrors();
        }

        //Context lifetime MUST BE ServiceLifetime.Scoped, for usage in singletons, use IDbContextFactory<AppDbContext> instead
        //Options must be ServiceLifetime.Singleton in order to be consumed in a DbContextFactory which is a singleton
        serviceCollection.AddDbContext<ToDoDbContext>(NodeDbContextOptionsBuilder, ServiceLifetime.Scoped,
            ServiceLifetime.Singleton);

        /*
         * Use IDbContextFactory<AppDbContext> in singletons
         * Then pass DbContext returned by .CreateDbContext() into ctor of AppDbContextAction and RepositoryBase<>
         * This way you are able to use one DbContext over multiple Repositories in Singletons (giving ability to do one transaction)
         */
        serviceCollection.AddDbContextFactory<ToDoDbContext>(NodeDbContextOptionsBuilder);

        serviceCollection.AddScoped(typeof(DbContextAction<>));
        serviceCollection.AddScoped(typeof(IDbContextEntityAction<>), typeof(DbContextAction<>));
        serviceCollection.AddScoped(typeof(IDbContextTransactionAction<>), typeof(DbContextAction<>));

        return serviceCollection;
    }

    private static IServiceCollection ConfigureHttp(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                // options.SuppressModelStateInvalidFilter = true;
                apiBehaviorOptions.InvalidModelStateResponseFactory = context =>
                {
                    var errorModelResult = new ErrorModelResult
                    {
                        TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
                    };

                    foreach (var modelError in context.ModelState.Values.SelectMany(modelStateValue =>
                                 modelStateValue.Errors))
                        errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.ModelState,
                            modelError.ErrorMessage));

                    return new BadRequestObjectResult(errorModelResult);
                };
            });

        serviceCollection
            .AddMvc()
            .ConfigureApiBehaviorOptions(options =>
            {
                // options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorModelResult = new ErrorModelResult
                    {
                        TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
                    };

                    foreach (var modelError in context.ModelState.Values.SelectMany(modelStateValue =>
                                 modelStateValue.Errors))
                        errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.ModelState,
                            modelError.ErrorMessage));

                    return new BadRequestObjectResult(errorModelResult);
                };
            });

        serviceCollection
            .AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
                options.InputFormatters.Add(new TextPlainInputFormatter());
                options.ModelBinderProviders.Insert(0, new FormDataJsonBinderProvider());
            })
            .AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.IncludeFields = true;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.JsonSerializerOptions.Converters.Add(new StringTrimmingConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return serviceCollection;
    }
}