using System.Diagnostics;
using Common.Common.Enums;
using Common.Common.Exceptions;
using Common.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Common.Filters;

public sealed class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var errorModelResult = new ErrorModelResult
        {
            TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
        };

        var environment = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        var logger = context.HttpContext.RequestServices.GetService<ILogger<HttpResponseExceptionFilter>>();

        switch (context.Exception)
        {
            case LocalizedException localizedException:
                logger.LogError(context.Exception, "Handled error!");

                errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.Generic, localizedException.AsUi,
                    ErrorEntryType.Message));

                if (!environment.IsProduction())
                {
                    errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.Generic,
                        context.Exception.StackTrace, ErrorEntryType.StackTrace));
                    errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.Generic, context.Exception.Source,
                        ErrorEntryType.Source));
                    errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.Generic,
                        context.HttpContext.Request.Path, ErrorEntryType.Path));
                }

                context.Result = new ObjectResult(errorModelResult)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                context.ExceptionHandled = true;
                break;
            case not null:
                //This is handled by /Error controller

                context.ExceptionHandled = false;
                break;
        }
    }

    public int Order => int.MaxValue - 10;
}