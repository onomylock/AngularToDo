using Common.Common.Enums;
using Common.Domain.Models.Base;

namespace ToDoBackend.Domain.Entities;

public class ToDoItem : EntityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public ToDoItemGroup Group { get; set; }
    public int FromUserId  {get; set;}
    public User From { get; set; }
    public int ToUserId {get; set;}
    public User To { get; set; }
    public DateOnly Deadline { get; set; }
    public ToDoItemStateEnum  State { get; set; }
}