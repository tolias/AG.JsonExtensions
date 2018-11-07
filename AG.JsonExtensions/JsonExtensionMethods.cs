using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using AG.Utilities;

namespace AG.JsonExtensions
{
    public static class JsonExtensionMethods
    {
        public static JsonFile<TJsonTypeClass> JsonFile<TJsonTypeClass>(this TJsonTypeClass obj)
        where TJsonTypeClass : new()
        {
            return new JsonFile<TJsonTypeClass>();
        }

        public static TJsonTypeClass FromJsonFile<TJsonTypeClass>(this TJsonTypeClass obj, string fileName)
        where TJsonTypeClass : new()
        {
            obj = obj.JsonFile().Load(fileName);

            return obj;
        }

        public static void SaveToJsonFile<TJsonTypeClass>(this TJsonTypeClass obj, string fileName)
        where TJsonTypeClass : new()
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            obj.JsonFile().Save(fileName, obj, settings);
        }

        public static void SaveToJsonFile<TJsonTypeClass>(this TJsonTypeClass obj, string fileName, JsonSerializerSettings settings)
        where TJsonTypeClass : new()
        {
            obj.JsonFile().Save(fileName, obj, settings);
        }

        public static DateTime FromJsonUnixTime(this JToken unixTimeValue)
        {
            return UnixTime.UnixTimeToDateTime(unixTimeValue.ToObject<int>());
        }
    }
}
