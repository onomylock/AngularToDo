using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.User.Common;

namespace ToDoBackend.Application.Models.Dto.User.Request;

public class UpdateUsersRequest : IRequestDtoBase
{
    public IEnumerable<UserDto> Items { get; set; }
}