using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RFERL.Modules.Framework.Common.Helpers.Json
{
    /// <summary>Represents contract resolver for properties which is privately set.</summary>
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        /// <summary>Creates json property for privately set ones.</summary>
        /// <param name="member">MemberInfo instace.</param>
        /// <param name="memberSerialization">Member serialization enumeration.</param>
        /// <returns>Created json property.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
            {
                return jProperty;
            }

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }
}
