﻿namespace Common.Common.Models;

public interface IEntityResultDtoBase : IResultDtoBase
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}