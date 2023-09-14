using System.Text.Json.Serialization;
using System.Text.Json;

namespace MISA.WebFresher042023.Demo.Core.ConvertTime
{
    public class LocalTimeZone : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString() ?? DateTime.Now.ToString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value.Kind == DateTimeKind.Unspecified)
            {
                writer.WriteStringValue(DateTime.SpecifyKind(value, DateTimeKind.Local));
            }
            else
            {
                writer.WriteStringValue(value.ToLocalTime());
            }
        }
    }
}
