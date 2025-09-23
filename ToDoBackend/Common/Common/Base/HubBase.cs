﻿using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Common.Common.Enums;
using Common.Common.Exceptions;
using Common.Common.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Common.Base;

public interface IHubBase
{
    /// <summary>
    ///     Validates a model
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public ErrorModelResult ValidateModel(object data);

    /// <summary>
    ///     Use as a wrapper when throwing an Exception inside Hub
    /// </summary>
    /// <param name="e"></param>
    /// <param name="errorType"></param>
    public void ThrowException(Exception e, ErrorType errorType = ErrorType.Generic);

    /// <summary>
    ///     Use to throw error model
    /// </summary>
    /// <param name="errorModel"></param>
    public void ThrowError(ErrorModelResult errorModel);

    /// <summary>
    ///     Call in Hub Endpoint dropping outdated clients is needed
    /// </summary>
    public void InvokeDisconnectFilter();

    /// <summary>
    ///     Adds connection filter that needs to return true in order to get disconnected
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="filter"></param>
    public void AddDisconnectFilter(string connectionId, Func<(bool result, string reason)> filter);

    /// <summary>
    ///     Removes connection filters by given predicate
    /// </summary>
    /// <param name="predicate"></param>
    public void DeleteDisconnectFilter(
        Func<KeyValuePair<(string connectionId, Func<(bool result, string reason)> filter), HubCallerContext>, bool>
            predicate
    );
}

public interface IHubBaseAction
{
    public Task ReceiveError(ErrorModelResult errorModel);
    public Task ReceiveErrorModelResult(ErrorModelResult errorModel);
}

/// <summary>
///     Base Hub every Hub must implement. MUST BE COPIED separately for each API project
/// </summary>
public class HubBase<T>(ILogger logger, IHostEnvironment hostEnvironment) : Hub<T>, IHubBase
    where T : class, IHubBaseAction
{
    private static readonly
        ConcurrentDictionary<(string connectionId, Func<(bool result, string reason)> disconnectOn), HubCallerContext>
        HubDictDisconnectOn = new();

    public ErrorModelResult ValidateModel(object data)
    {
        var context = new ValidationContext(data);
        var validationResults = new List<ValidationResult>();

        if (Validator.TryValidateObject(data, context, validationResults, true))
            return null;

        var errorModelResult = new ErrorModelResult
        {
            Errors = new List<ErrorModelResultEntry>()
        };

        foreach (var validationResult in validationResults)
            errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.ModelState, validationResult.ErrorMessage));

        return errorModelResult;
    }

    public void ThrowException(Exception e, ErrorType errorType = ErrorType.Generic)
    {
        logger.LogError(e, Localize.Log.Empty);

        var exceptionHandlerFeature = Context.Features.Get<IExceptionHandlerFeature>()!;
        var httpContextFeature = Context.Features.Get<IHttpContextFeature>()!;

        var errorModelResult = new ErrorModelResult
        {
            TraceId = Activity.Current?.Id ?? httpContextFeature.HttpContext!.TraceIdentifier
        };

        if (!hostEnvironment.IsDevelopment())
        {
            errorModelResult.Errors.Add(new ErrorModelResultEntry(errorType,
                Localize.Keys.Error.HandledExceptionContactSystemAdministrator, ErrorEntryType.Message));

            Clients.Caller.ReceiveError(errorModelResult);
            //Clients.Caller.SendAsync(Consts.SignalRClientEventNameReceiveError, errorModelResult);
            Context.Abort();
        }

        errorModelResult.Errors.Add(new ErrorModelResultEntry(errorType, exceptionHandlerFeature.Error.Message,
            ErrorEntryType.Message));
        errorModelResult.Errors.Add(new ErrorModelResultEntry(errorType, exceptionHandlerFeature.Error.StackTrace,
            ErrorEntryType.StackTrace));
        errorModelResult.Errors.Add(new ErrorModelResultEntry(errorType, exceptionHandlerFeature.Error.Source,
            ErrorEntryType.Source));
        errorModelResult.Errors.Add(new ErrorModelResultEntry(errorType, exceptionHandlerFeature.Path,
            ErrorEntryType.Path));

        Clients.Caller.ReceiveError(errorModelResult);
        //Clients.Caller.SendAsync(Consts.SignalRClientEventNameReceiveError, errorModelResult);
        Context.Abort();
    }

    public void ThrowError(ErrorModelResult errorModel)
    {
        var httpContextFeature = Context.Features.Get<IHttpContextFeature>()!;

        errorModel.TraceId = Activity.Current?.Id ?? httpContextFeature.HttpContext!.TraceIdentifier;

        Clients.Caller.ReceiveError(errorModel);
        //Clients.Caller.SendAsync(Consts.SignalRClientEventNameReceiveError, errorModelResult);
    }

    public void InvokeDisconnectFilter()
    {
        var httpContextFeature = Context.Features.Get<IHttpContextFeature>()!;

        foreach (var _ in HubDictDisconnectOn.Select(_ => (_, _.Key.disconnectOn())).Where(_ => _.Item2.result))
        {
            Clients.Client(_._.Key.connectionId).ReceiveErrorModelResult(new ErrorModelResult
            {
                Errors =
                [
                    new ErrorModelResultEntry(ErrorType.Generic,
                        _.Item2.reason ?? new ConnectionExpiredException().AsUi, ErrorEntryType.Message)
                ],
                TraceId = Activity.Current?.Id ?? httpContextFeature.HttpContext!.TraceIdentifier
            });
            _._.Value.Abort();
        }
    }

    public void AddDisconnectFilter(string connectionId, Func<(bool result, string reason)> filter)
    {
        HubDictDisconnectOn.TryAdd((Context.ConnectionId, filter), Context);
    }

    public void DeleteDisconnectFilter(
        Func<KeyValuePair<(string connectionId, Func<(bool result, string reason)> filter), HubCallerContext>, bool>
            predicate
    )
    {
        HubDictDisconnectOn.TryRemove(HubDictDisconnectOn.SingleOrDefault(predicate));
    }
}