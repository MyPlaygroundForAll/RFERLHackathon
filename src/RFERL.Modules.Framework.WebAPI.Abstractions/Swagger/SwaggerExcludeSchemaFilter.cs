using System;
using System.Linq;
using System.Reflection;
using RFERL.Modules.Framework.Common.Attributes;
using RFERL.Modules.Framework.Common.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Swagger
{
    /// <summary>Represents swagger schema filter for excluding properties.</summary>
    public class SwaggerExcludeSchemaFilter : ISchemaFilter
    {
        /// <summary>Applies exclude rules to swagger schema.</summary>
        /// <param name="schema">Schema instance.</param>
        /// <param name="context">Schema filter context instance.</param>
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var isRequestHasIdentity = context.SystemType.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Any(i => i.GetGenericTypeDefinition() == typeof(IRequestHasIdentity<>));

            if (isRequestHasIdentity)
            {
                var idProperty = schema.Properties.Keys.SingleOrDefault(key => key.ToLowerInvariant().Equals("id"));
                if (idProperty != null)
                {
                    schema.Properties.Remove(idProperty);
                }
            }

            var excludedProperties =
                context.SystemType.GetProperties().Where(
                    t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var propertyToRemove =
                    schema.Properties.Keys.SingleOrDefault(
                        x => string.Equals(x, excludedProperty.Name, StringComparison.CurrentCultureIgnoreCase));

                if (propertyToRemove != null)
                {
                    schema.Properties.Remove(propertyToRemove);
                }
            }
        }
    }
}
