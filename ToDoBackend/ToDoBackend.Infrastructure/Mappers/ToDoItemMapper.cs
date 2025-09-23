using Common.Common.Enums;
using Common.Common.Models;
using ToDoBackend.Application.Exceptions;
using ToDoBackend.Application.Models.Dto.ToDoItems.Common;
using ToDoBackend.Application.Models.Dto.ToDoItems.Request;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Mappers;

internal static class ToDoItemMapper
{
    public static async Task<IEnumerable<ToDoItem>> MapCrateToDoItemsRequest(
        CreateToDoItemsRequest request,
        IUserEntityService userEntityService,
        IToDoItemGroupEntityService groupEntityService,
        CancellationToken token = default)
    {
        var entities = new List<ToDoItem>();

        var fromUserIds = request.Items.Select((item, index) => (index, item.FromUserId));
        var toUserIds = request.Items.Select((item, index) => (index, item.ToUserId));
        var userIds = fromUserIds.Concat(toUserIds).Select(x => x.Item2).Distinct();

        var users = await userEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(userIds, user => user.Id), true, token);

        var groups = request.Items.Select((item, index) => (index, item.GroupId));
        var groupIds = groups.Select(x => x.GroupId).Distinct();


        var entityGroups = await groupEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(groupIds, group => group.Id), true, token);

        foreach (var dto in request.Items)
        {
            var currentUserFrom = users.entities.FirstOrDefault(x => x.Id == dto.FromUserId) ??
                                  throw new EntityNotFoundException(
                                      $"entity {typeof(User)} with id {dto.FromUserId} not found");
            var currentUserTo = users.entities.FirstOrDefault(x => x.Id == dto.ToUserId) ??
                                throw new EntityNotFoundException(
                                    $"entity {typeof(User)} with id {dto.ToUserId} not found");
            var currentGroup = entityGroups.entities.FirstOrDefault(x => x.Id == dto.GroupId) ??
                               throw new EntityNotFoundException(
                                   $"entity {typeof(ToDoItemGroupMapper)} with id {dto.GroupId} not found");
            entities.Add(MapToToDoItemDto(dto, currentUserFrom.Id, currentUserTo.Id, currentGroup.Id));
        }

        return entities;
    }

    public static async Task<IEnumerable<ToDoItem>> MapUpdateToDoItemsRequest(UpdateToDoItemsRequest request,
        IReadOnlyCollection<ToDoItem> toDoItems,
        IUserEntityService userEntityService,
        IToDoItemGroupEntityService groupEntityService,
        CancellationToken token = default)
    {
        var entities = new List<ToDoItem>();

        var fromUserIds = request.Items.Select(x => (x.Id, x.FromUserId));
        var toUserIds = request.Items.Select(x => (x.Id, x.ToUserId));
        var userIds = fromUserIds.Concat(toUserIds).Select(x => x.Item2).Distinct();

        var users = await userEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(userIds, user => user.Id), true, token);

        var groups = request.Items.Select(x => (x.Id, x.GroupId));
        var groupIds = groups.Select(x => x.GroupId).Distinct();


        var entityGroups = await groupEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(groupIds, group => group.Id), true, token);

        foreach (var dto in request.Items)
        {
            var currentUserFrom = users.entities.FirstOrDefault(x => x.Id == dto.FromUserId) ??
                                  throw new EntityNotFoundException(
                                      $"entity {typeof(User)} with id {dto.FromUserId} not found");
            var currentUserTo = users.entities.FirstOrDefault(x => x.Id == dto.ToUserId) ??
                                throw new EntityNotFoundException(
                                    $"entity {typeof(User)} with id {dto.ToUserId} not found");
            var currentGroup = entityGroups.entities.FirstOrDefault(x => x.Id == dto.GroupId) ??
                               throw new EntityNotFoundException(
                                   $"entity {typeof(ToDoItemGroupMapper)} with id {dto.GroupId} not found");
            var currentToDoItem = toDoItems.FirstOrDefault(x => x.Id == dto.Id) ??
                                  throw new EntityNotFoundException(
                                      $"entity {typeof(ToDoItem)} with id {dto.Id} not found");

            entities.Add(MapUpdateToDoItem(currentToDoItem, dto));
        }

        return entities;
    }

    public static IEnumerable<ToDoItemDto> MapToDoItem(IReadOnlyCollection<ToDoItem> toDoItems)
    {
        return toDoItems.Select(MapToToDoItemDto);
    }

    private static ToDoItemDto MapToToDoItemDto(ToDoItem entity)
    {
        return new ToDoItemDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Title = entity.Title,
            Description = entity.Description,
            GroupId = entity.GroupId,
            FromUserId = entity.FromUserId,
            ToUserId = entity.ToUserId,
            Deadline = entity.Deadline,
            State = entity.State
        };
    }

    private static ToDoItem MapUpdateToDoItem(ToDoItem entity, ToDoItemDto dto)
    {
        entity.GroupId = dto.GroupId;
        entity.ToUserId = dto.ToUserId;
        entity.FromUserId = dto.FromUserId;
        entity.Deadline = dto.Deadline;
        entity.Description = dto.Description;
        entity.State = dto.State ?? ToDoItemStateEnum.New;
        entity.Title = dto.Title;
        return entity;
    }

    private static ToDoItem MapToToDoItemDto(ToDoItemDto dto, int userFromId, int userToId, int groupId)
    {
        return new ToDoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            GroupId = groupId,
            FromUserId = userFromId,
            ToUserId = userToId,
            Deadline = dto.Deadline,
            State = ToDoItemStateEnum.New
        };
    }
}