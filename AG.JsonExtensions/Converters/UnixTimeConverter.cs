using System;
using AG.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AG.JsonExtensions.Converters
{
    public class UnixTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jsonValue = UnixTime.ToUnixTime((DateTime)value, DateTimeKind.Local).ToString();
            writer.WriteRawValue(jsonValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return UnixTime.UnixTimeToDateTime((long)reader.Value);
        }
    }
}
