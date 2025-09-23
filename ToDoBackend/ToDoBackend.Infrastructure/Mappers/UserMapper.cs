using Common.Common.Models;
using ToDoBackend.Application.Exceptions;
using ToDoBackend.Application.Models.Dto.User.Common;
using ToDoBackend.Application.Models.Dto.User.Request;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Mappers;

internal static class UserMapper
{
    public static async Task<IEnumerable<User>> MapCrateUsersRequest(CreateUsersRequest request,
        IToDoItemGroupEntityService toDoItemGroupEntityService, CancellationToken token = default)
    {
        var entities = new List<User>();

        var toDoItemsGroupIds = request.Items.Select((item, index) => (index, item.ToDoItemGroupIds));

        var toDoItemGroups = await toDoItemGroupEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(toDoItemsGroupIds.SelectMany(x => x.ToDoItemGroupIds).Distinct(),
                user => user.Id), true, token);

        foreach (var dto in request.Items)
        {
            var currentToDoItemGroups =
                toDoItemGroups.entities.Where(x => dto.ToDoItemGroupIds.Contains(x.Id)).ToList();
            entities.Add(MapCreateUserDto(dto, currentToDoItemGroups));
        }

        return entities;
    }

    public static async Task<IEnumerable<User>> MapUpdateUsersRequest(UpdateUsersRequest request,
        IReadOnlyCollection<User> users,
        IToDoItemGroupEntityService toDoItemGroupEntityService, CancellationToken token = default)
    {
        var entities = new List<User>();

        var toDoItemsGroupIds = request.Items.Select((item, index) => (index, item.ToDoItemGroupIds));

        var toDoItemGroups = await toDoItemGroupEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(toDoItemsGroupIds.SelectMany(x => x.ToDoItemGroupIds).Distinct(),
                user => user.Id), true, token);

        foreach (var dto in request.Items)
        {
            var currentToDoItemGroups =
                toDoItemGroups.entities.Where(x => dto.ToDoItemGroupIds.Contains(x.Id)).ToList();
            var currentUser = users.FirstOrDefault(x => dto.Id == x.Id) ??
                              throw new EntityNotFoundException($"entity {typeof(User)} with id {dto.Id} not found");

            entities.Add(MapUpdateUserDto(currentUser, dto, currentToDoItemGroups));
        }

        return entities;
    }

    public static IEnumerable<UserDto> MapGetUsers(IEnumerable<User> entities)
    {
        return entities.Select(MapUserDto);
    }

    private static UserDto MapUserDto(User entity)
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
            ToDoItemGroupIds = entity.ToDoItemGroups.Select(x => x.Id).ToList()
        };
    }

    private static User MapUpdateUserDto(User entity, UserDto dto, ICollection<ToDoItemGroup> currentToDoItemGroups)
    {
        entity.ToDoItemGroups = currentToDoItemGroups;
        entity.Email = dto.Email;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Username = dto.Username;
        entity.Password = dto.Password;

        return entity;
    }

    private static User MapCreateUserDto(UserDto dto, ICollection<ToDoItemGroup> groups)
    {
        return new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            ToDoItemGroups = groups
        };
    }
}