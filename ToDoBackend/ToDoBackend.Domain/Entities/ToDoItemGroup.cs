using Common.Domain.Models.Base;

namespace ToDoBackend.Domain.Entities;

public record ToDoItemGroup : EntityBase
{
    public string Title { get; set; }
    public ICollection<ToDoItem> ToDoItems { get; set; }
}