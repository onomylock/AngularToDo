using System.ComponentModel.DataAnnotations;

namespace Common.Domain.Models.Base;

public abstract record EntityBase
{
    [Key] public int Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}