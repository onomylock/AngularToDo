using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Common.Converters;

public sealed class DateOnlyToLongConverter : ValueConverter<DateOnly, long>
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public DateOnlyToLongConverter()
        : base(
            date => (long)(date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) - UnixEpoch).TotalDays,
            days => DateOnly.FromDateTime(UnixEpoch.AddDays(days)))
    {
    }
}