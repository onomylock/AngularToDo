using ToDoBackend.Application.Handlers;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Handlers;

public class ToDoItemHandler : IToDoItemHandler
{
    public Task<List<ToDoItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}