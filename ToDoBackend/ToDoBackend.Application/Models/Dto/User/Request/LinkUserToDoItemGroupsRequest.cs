using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.User.Request;

public class LinkUserToDoItemGroupsRequest : IRequestDtoBase
{
    public int TargetUserId { get; set; }
    public int[] ToDoItemGroupIds { get; set; }
}