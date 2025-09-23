namespace ToDoBackend.Application.Models.Dto.User.Common;

public class UserDto
{
    public int? Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<int> ToDoItemGroupIds { get; set; }
}