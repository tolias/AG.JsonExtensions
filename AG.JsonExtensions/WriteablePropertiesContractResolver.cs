using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace AG.JsonExtensions
{
    public class WriteablePropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = _ => ShouldSerialize(member);
            return property;
        }

        internal static bool ShouldSerialize(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                return memberInfo is FieldInfo;
            }

            return propertyInfo.CanWrite && propertyInfo.CanRead;
        }
    }
}
