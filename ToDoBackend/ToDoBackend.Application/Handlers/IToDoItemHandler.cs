using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Handlers;

public interface IToDoItemHandler
{
    public Task<List<ToDoItem>> GetAllAsync();
}