using System.Reflection;

namespace RFERL.Modules.Framework.Common.Helpers.Json
{
    /// <summary>Represents extension methods for <see cref="MemberInfo"/>.</summary>
    public static class MemberInfoExtensions
    {
        /// <summary>Checks property has set method or not.</summary>
        /// <param name="member">MemberInfo instance.</param>
        /// <returns>A value whether property has setter or not.</returns>
        public static bool IsPropertyWithSetter(this MemberInfo member)
        {
            var property = member as PropertyInfo;

            return property?.SetMethod != null;
        }
    }
}
