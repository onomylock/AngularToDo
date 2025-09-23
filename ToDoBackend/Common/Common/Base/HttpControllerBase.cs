using System.Diagnostics;
using Common.Common.Dto.Generic;
using Common.Common.Models;
using Common.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Common.Common.Base;

/// <summary>
///     Base Controller every Controller must implement
/// </summary>
[ProducesResponseType(typeof(ErrorModelResult), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ErrorModelResult), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorModelResult), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ErrorModelResult), StatusCodes.Status500InternalServerError)]
public class HttpControllerBase(
    IHttpContextAccessor httpContextAccessor,
    IWarningService warningService,
    ILogger<HttpControllerBase> logger
)
    : ControllerBase
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

    /// <summary>
    ///     Used to response with the result from Handler
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected IActionResult ResponseWith(IResultDtoBase response)
    {
        response.TraceId = Activity.Current?.Id ?? _httpContext.TraceIdentifier;

        var warnings = warningService.GetAll();

        foreach (var warning in warnings)
            logger.LogWarning(warning.Message);

        response.Warnings = warnings.Count != 0 ? warnings : null;

        switch (response)
        {
            case FileReadResultDto fileResult:
            {
                var file = File(fileResult.Stream, fileResult.ContentType, fileResult.FileName);
                file.EnableRangeProcessing = true;
                return file;
            }
            case RedirectResultDto redirect:
                return new RedirectResult(redirect.Url, redirect.IsPermanent, redirect.PreserveMethod);
            default:
                return new OkObjectResult(response);
        }
    }
}