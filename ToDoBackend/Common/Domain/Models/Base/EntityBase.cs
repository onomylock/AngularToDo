using System.ComponentModel.DataAnnotations;

namespace Common.Domain.Models.Base;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}