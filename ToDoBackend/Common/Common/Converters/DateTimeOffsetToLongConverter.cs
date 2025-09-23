using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Common.Converters;

public sealed class DateTimeOffsetToLongConverter : ValueConverter<DateTimeOffset, long>
{
    public DateTimeOffsetToLongConverter() : base(
        d => d.ToUniversalTime().ToUnixTimeSeconds(),
        d => DateTimeOffset.FromUnixTimeSeconds(d))
    {
    }
}