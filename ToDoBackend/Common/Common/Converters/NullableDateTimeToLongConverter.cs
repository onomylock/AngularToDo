using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Common.Converters;

public sealed class NullableDateTimeToLongConverter : ValueConverter<DateTime?, long?>
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public NullableDateTimeToLongConverter()
        : base(
            d => d.HasValue ? (long)(d.Value.ToUniversalTime() - UnixEpoch).TotalSeconds : null,
            d => d.HasValue ? UnixEpoch.AddSeconds(d.Value) : null)
    {
    }
}