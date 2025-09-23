using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.User.Request;

public class DeleteUsersRequest : IRequestDtoBase
{
    public IEnumerable<int> Ids { get; set; }
}