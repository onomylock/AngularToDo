using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Common.Converters;

public class GuidJsonConverter : JsonConverter<Guid?>
{
    public override Guid? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return Guid.TryParse(reader.GetString() ?? string.Empty, out var newGuid) ? newGuid : null;
    }

    public override void Write(
        Utf8JsonWriter writer,
        Guid? value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}