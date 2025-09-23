using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Common.Converters;

public sealed class DateTimeToLongConverter : ValueConverter<DateTime, long>
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public DateTimeToLongConverter()
        : base(
            d => (long)(d.ToUniversalTime() - UnixEpoch).TotalSeconds,
            d => UnixEpoch.AddSeconds(d))
    {
    }
}