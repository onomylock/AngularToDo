using Common.Common.Enums;

namespace ToDoBackend.Application.Models.Dto.ToDoItems.Common;

public class ToDoItemDto
{
    public int? Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public DateOnly Deadline { get; set; }
    public ToDoItemStateEnum? State { get; set; }
}