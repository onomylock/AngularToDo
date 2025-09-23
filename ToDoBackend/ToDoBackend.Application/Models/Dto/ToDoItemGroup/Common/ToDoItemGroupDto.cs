namespace ToDoBackend.Application.Models.Dto.ToDoItemGroup.Common;

public class ToDoItemGroupDto
{
    public int? Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string Title { get; set; }
    public IEnumerable<int> UserIds { get; set; }
    public IEnumerable<int> ToDoItemIds { get; set; }
}