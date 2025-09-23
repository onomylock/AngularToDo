using Common.Common.Base;
using Common.Common.Services;
using Microsoft.AspNetCore.Mvc;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;

namespace ToDoBackend.HttpApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ToDoItemGroupController(
    IToDoItemGroupHandler handler,
    IHttpContextAccessor httpContextAccessor,
    IWarningService warningService,
    ILogger<HttpControllerBase> logger
) : HttpControllerBase(httpContextAccessor, warningService, logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(GetToDoItemGroupsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetToDoItemGroups([FromQuery] GetToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.GetToDoItemGroups(request, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateToDoItemGroupsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateToDoItemGroups([FromQuery] CreateToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.CreateToDoItemGroups(request, cancellationToken));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteToDoItemGroupsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteToDoItemGroups([FromQuery] DeleteToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.DeleteToDoItemGroups(request, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateToDoItemGroupsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateToDoItemGroups([FromQuery] UpdateToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.UpdateToDoItemGroups(request, cancellationToken));
    }
}