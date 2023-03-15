using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Models.Response.Appointments.Appointment
{
    public class TimeSlotsResponse
    {
        [JsonConverter(typeof(TimeSlotsConverter))]
        public IDictionary<TimeOnly, HashSet<Guid>> TimeSlots { get; set; }
    }

    public class TimeSlotsConverter : JsonConverter<IDictionary<TimeOnly, HashSet<Guid>>>
    {
        private const string TimeFormat = "HH:mm";

        public override IDictionary<TimeOnly, HashSet<Guid>> Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<TimeOnly, HashSet<Guid>> value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (KeyValuePair<TimeOnly, HashSet<Guid>> pair in value)
            {
                writer.WriteString(pair.Key.ToString(TimeFormat),
                    JsonSerializer.Serialize(pair.Value));
            }

            writer.WriteEndObject();
        }
    }
}
