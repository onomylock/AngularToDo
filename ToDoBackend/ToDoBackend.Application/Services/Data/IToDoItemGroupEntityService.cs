using Common.Domain.Services;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Data;

public interface IToDoItemGroupEntityService : IEntityServiceBase<ToDoItemGroup>, IEntityServiceCollectionBase<ToDoItemGroup>;