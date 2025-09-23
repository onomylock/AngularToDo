using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.User.Request;

public class GetUsersRequest : IRequestDtoBase
{
    public IEnumerable<int> Ids { get; set; }
}