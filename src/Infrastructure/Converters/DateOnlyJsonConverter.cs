using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string Format;
        public DateOnlyJsonConverter(string format)
        {
            Format = format;
        }
        public override void Write(Utf8JsonWriter writer, DateOnly date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(Format));
        }
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateTime = DateTime.Parse(reader.GetString());
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
