using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.User.Common;

namespace ToDoBackend.Application.Models.Dto.User.Response;

public class GetUsersResponse : PageModelResult<UserDto>;