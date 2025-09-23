using Common.Domain.Models.Base;

namespace ToDoBackend.Domain.Entities;

public class User : EntityBase
{
    public ICollection<ToDoItemGroup> ToDoItemGroups { get; set; }
}