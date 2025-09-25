using Common.Domain.Services;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Data;

public interface IUserToToDoItemGroupMappingEntityService :
    IEntityToEntityMappingServiceBase<UserToToDoItemGroupMapping>,
    IEntityServiceCollectionBase<UserToToDoItemGroupMapping>;