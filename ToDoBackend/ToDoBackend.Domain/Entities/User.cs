using Common.Domain.Models.Base;

namespace ToDoBackend.Domain.Entities;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<ToDoItemGroup> ToDoItemGroups { get; set; }
}