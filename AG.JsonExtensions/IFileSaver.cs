using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AG.JsonExtensions
{
    public interface IFileSaver<TJsonTypeClass>
    {
        TJsonTypeClass Load(string fileName);
        void Save(string fileName, TJsonTypeClass obj);
        void Save(string fileName, TJsonTypeClass obj, JsonSerializerSettings jsonSerializerSettings);
    }
}
