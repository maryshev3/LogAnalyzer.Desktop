using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

public class LogLevelConverter : JsonConverter<LogLevel>
{
    public override LogLevel ReadJson(JsonReader reader, Type objectType, LogLevel existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var name = obj["name"]?.ToString();
        return LogLevel.FromString(name);
    }

    public override void WriteJson(JsonWriter writer, LogLevel value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("name");
        writer.WriteValue(value.Name);
        writer.WritePropertyName("ordinal");
        writer.WriteValue(value.Ordinal);
        writer.WriteEndObject();
    }
}