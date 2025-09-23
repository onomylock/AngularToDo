using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Common.Converters;

public sealed class NullableDateTimeOffsetToLongConverter : ValueConverter<DateTimeOffset?, long?>
{
    public NullableDateTimeOffsetToLongConverter() : base(
        d => d.HasValue ? d.Value.ToUniversalTime().ToUnixTimeSeconds() : null,
        d => d.HasValue ? DateTimeOffset.FromUnixTimeSeconds(d.Value) : null)
    {
    }
}