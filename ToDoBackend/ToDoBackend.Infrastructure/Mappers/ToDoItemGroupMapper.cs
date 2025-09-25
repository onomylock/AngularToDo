using Common.Common.Models;
using ToDoBackend.Application.Exceptions;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Common;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Mappers;

public static class ToDoItemGroupMapper
{
    public static async Task<IEnumerable<ToDoItemGroup>> MapCrateToDoItemGroupsRequest(
        CreateToDoItemGroupsRequest request,
        IToDoItemEntityService toDoItemEntityService, CancellationToken token = default)
    {
        var entities = new List<ToDoItemGroup>();

        var toDoItemsIds = request.Items.Select((item, index) => (index, item.ToDoItemIds));

        var toDoItems = await toDoItemEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(toDoItemsIds.SelectMany(x => x.ToDoItemIds).Distinct(), toDoItem => toDoItem.Id),
            true, token);

        foreach (var dto in request.Items)
        {
            var currentToDoItems = toDoItems.entities.Where(x => dto.ToDoItemIds.Contains(x.Id)).ToList();
            entities.Add(MapCreateToDoItemGroupDto(dto, currentToDoItems));
        }

        return entities;
    }

    public static async Task<IEnumerable<ToDoItemGroup>> MapUpdateToDoItemGroupsRequest(
        UpdateToDoItemGroupsRequest request, IReadOnlyCollection<ToDoItemGroup> toDoItemGroups,
        IToDoItemEntityService toDoItemEntityService,
        CancellationToken token = default)
    {
        var entities = new List<ToDoItemGroup>();

        var toDoItemsIds = request.Items.Select((item, index) => (index, item.ToDoItemIds));

        var toDoItems = await toDoItemEntityService.GetCollection(PageModel.All,
            query => query.IntersectBy(toDoItemsIds.SelectMany(x => x.ToDoItemIds).Distinct(), toDoItem => toDoItem.Id),
            true, token);

        foreach (var dto in request.Items)
        {
            var currentToDoItems = toDoItems.entities.Where(x => dto.ToDoItemIds.Contains(x.Id)).ToList();
            var currentToDoItemGroup = toDoItemGroups.FirstOrDefault(x => dto.Id == x.Id) ??
                                       throw new EntityNotFoundException(
                                           $"entity {typeof(ToDoItemGroup)} with id {dto.Id} not found");

            entities.Add(MapUpdateToDoItemGroupDto(currentToDoItemGroup, dto, currentToDoItems));
        }

        return entities;
    }

    public static IEnumerable<ToDoItemGroupDto> MapGetToDoItemGroups(IEnumerable<ToDoItemGroup> entities,
        Dictionary<int, IEnumerable<int>> userDict)
    {
        return entities.Select(x => MapToToDoItemGroupDto(x, userDict[x.Id] ?? []));
    }

    private static ToDoItemGroupDto MapToToDoItemGroupDto(ToDoItemGroup entity, IEnumerable<int> userIds)
    {
        return new ToDoItemGroupDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Title = entity.Title,
            UserIds = userIds,
            ToDoItemIds = entity.ToDoItems?.Select(x => x.Id).ToList()
        };
    }

    private static ToDoItemGroup MapUpdateToDoItemGroupDto(ToDoItemGroup entity, ToDoItemGroupDto dto,
        ICollection<ToDoItem> toDoItems)
    {
        entity.Title = dto.Title;
        entity.ToDoItems = toDoItems;
        return entity;
    }

    private static ToDoItemGroup MapCreateToDoItemGroupDto(ToDoItemGroupDto dto,
        ICollection<ToDoItem> toDoItems)
    {
        return new ToDoItemGroup
        {
            Id = dto.Id ?? 0,
            Title = dto.Title,
            ToDoItems = toDoItems
        };
    }
}