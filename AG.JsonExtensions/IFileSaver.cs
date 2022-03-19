using Newtonsoft.Json;

namespace AG.JsonExtensions
{
    public interface IFileSaver<TJsonTypeClass>
    {
        TJsonTypeClass Load(string fileName, bool dontCreateDefaultSettings = false);
        void Save(string fileName, TJsonTypeClass obj);
        void Save(string fileName, TJsonTypeClass obj, JsonSerializerSettings jsonSerializerSettings);
    }
}
