using Common.Common.Base;
using Common.Common.Services;
using Microsoft.AspNetCore.Mvc;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;
using ToDoBackend.Application.Models.Dto.ToDoItems.Request;
using ToDoBackend.Application.Models.Dto.User.Request;

namespace ToDoBackend.HttpApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class ToDoItemController(
    IToDoItemHandler handler,
    IHttpContextAccessor httpContextAccessor,
    IWarningService warningService,
    ILogger<HttpControllerBase> logger
) : HttpControllerBase(httpContextAccessor, warningService, logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(GetToDoItemsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetToDoItems([FromQuery] GetToDoItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.GetToDoItems(request, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateToDoItemsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateToDoItems([FromQuery] CreateToDoItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.CreateToDoItems(request, cancellationToken));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteToDoItemsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteToDoItems([FromQuery] DeleteToDoItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.DeleteToDoItems(request, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateToDoItemsRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateToDoItems([FromQuery] UpdateToDoItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.UpdateToDoItems(request, cancellationToken));
    }
}