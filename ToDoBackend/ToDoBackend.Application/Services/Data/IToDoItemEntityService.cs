using Common.Domain.Services;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Data;

public interface IToDoItemEntityService : IEntityServiceBase<ToDoItem>, IEntityServiceCollectionBase<ToDoItem>;