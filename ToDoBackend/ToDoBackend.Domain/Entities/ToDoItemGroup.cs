using Common.Domain.Models.Base;

namespace ToDoBackend.Domain.Entities;

public class ToDoItemGroup : EntityBase
{
    public string Title { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<ToDoItem> Items { get; set; }
}