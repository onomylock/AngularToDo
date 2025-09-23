namespace Common.Common.Dto.Base;

public interface IEntityBaseResultDto
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}