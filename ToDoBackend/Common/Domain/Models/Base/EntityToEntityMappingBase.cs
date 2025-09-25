namespace Common.Domain.Models.Base;

/// <summary>
///     Base class every Database Many-to-Many Entity must inherit
/// </summary>
public abstract record EntityToEntityMappingBase<TEntityLeft, TEntityRight> : EntityBase
    where TEntityLeft : EntityBase
    where TEntityRight : EntityBase
{
    public int EntityLeftId { get; set; }
    public virtual TEntityLeft EntityLeft { get; set; }
    public int EntityRightId { get; set; }
    public virtual TEntityRight EntityRight { get; set; }
}