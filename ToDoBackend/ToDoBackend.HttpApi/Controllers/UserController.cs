using Common.Common.Base;
using Common.Common.Services;
using Microsoft.AspNetCore.Mvc;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.User.Request;

namespace ToDoBackend.HttpApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(
    IUserHandler handler,
    IHttpContextAccessor httpContextAccessor,
    IWarningService warningService,
    ILogger<HttpControllerBase> logger
) : HttpControllerBase(httpContextAccessor, warningService, logger)
{
    [HttpGet]
    [ProducesResponseType(typeof(GetUsersRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.GetUsers(request, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateUsersRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUsers([FromQuery] CreateUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.CreateUsers(request, cancellationToken));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteUsersRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUsers([FromQuery] DeleteUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.DeleteUsers(request, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateUsersRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUsers([FromQuery] UpdateUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        return ResponseWith(await handler.UpdateUsers(request, cancellationToken));
    }
}