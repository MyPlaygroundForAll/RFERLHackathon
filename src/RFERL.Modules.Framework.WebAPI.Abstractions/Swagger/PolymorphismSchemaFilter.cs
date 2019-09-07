using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Swagger
{
    /// <summary>Represents polymorphism schema filter for overriding schema for inherited objects.</summary>
    /// <typeparam name="T">Type of abstract class which has inheritances.</typeparam>
    public class PolymorphismSchemaFilter<T> : ISchemaFilter
    {
        private readonly Lazy<HashSet<Type>> _derivedTypes = new Lazy<HashSet<Type>>(Init);

        /// <summary>Applies derived object from abstract class to swagger schema.</summary>
        /// <param name="schema">Schema instance.</param>
        /// <param name="context">Schema filter context instance.</param>
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (!_derivedTypes.Value.Contains(context.SystemType))
            {
                return;
            }

            var clonedSchema = new Schema
            {
                Properties = schema.Properties,
                Type = schema.Type,
                Required = schema.Required
            };

            var parentSchema = new Schema { Ref = "#/definitions/" + typeof(T).Name };

            schema.AllOf = new List<Schema> { parentSchema, clonedSchema };

            schema.Properties = new Dictionary<string, Schema>();
        }

        private static HashSet<Type> Init()
        {
            var abstractType = typeof(T);
            var dTypes = abstractType.Assembly
                                     .GetTypes()
                                     .Where(x => abstractType != x && abstractType.IsAssignableFrom(x));

            var result = new HashSet<Type>();

            foreach (var item in dTypes)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
