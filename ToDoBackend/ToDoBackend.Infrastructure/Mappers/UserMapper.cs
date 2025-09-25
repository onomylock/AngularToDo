using Common.Common.Models;
using ToDoBackend.Application.Exceptions;
using ToDoBackend.Application.Models.Dto.User.Common;
using ToDoBackend.Application.Models.Dto.User.Request;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Mappers;

internal static class UserMapper
{
    public static IEnumerable<User> MapCrateUsersRequest(CreateUsersRequest request)
    {
        var entities = new List<User>();

        foreach (var dto in request.Items) entities.Add(MapCreateUserDto(dto));

        return entities;
    }

    public static IEnumerable<User> MapUpdateUsersRequest(UpdateUsersRequest request,
        IReadOnlyCollection<User> users,
        IToDoItemGroupEntityService toDoItemGroupEntityService, CancellationToken token = default)
    {
        var entities = new List<User>();


        foreach (var dto in request.Items)
        {
            var currentUser = users.FirstOrDefault(x => dto.Id == x.Id) ??
                              throw new EntityNotFoundException($"entity {typeof(User)} with id {dto.Id} not found");

            entities.Add(MapUpdateUserDto(currentUser, dto));
        }

        return entities;
    }

    public static IEnumerable<UserDto> MapGetUsers(IEnumerable<User> entities,
        Dictionary<int, IEnumerable<int>> toDoItemsDict)
    {
        return entities.Select(x => MapUserDto(x, toDoItemsDict[x.Id] ?? []));
    }

    public static IEnumerable<UserToToDoItemGroupMapping> MapUserToToDoItemGroupMapping(User user,
        IEnumerable<ToDoItemGroup> toDoItemGroups)
    {
        return toDoItemGroups.Select(x => new UserToToDoItemGroupMapping
        {
            EntityLeft = user, EntityLeftId = user.Id, EntityRight = x, EntityRightId = x.Id
        });
    }

    private static UserDto MapUserDto(User entity, IEnumerable<int> toDoItemGroupIds)
    {
        return new UserDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Username = entity.Username,
            Password = entity.Password,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            ToDoItemGroupIds = toDoItemGroupIds
        };
    }

    private static User MapUpdateUserDto(User entity, UserDto dto)
    {
        entity.Email = dto.Email;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Username = dto.Username;
        entity.Password = dto.Password;

        return entity;
    }

    private static User MapCreateUserDto(UserDto dto)
    {
        return new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }
}