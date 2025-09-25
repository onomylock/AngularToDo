using Common.Domain.Models.Base;

namespace ToDoBackend.Domain.Entities;

public record UserToToDoItemGroupMapping : EntityToEntityMappingBase<User, ToDoItemGroup>;