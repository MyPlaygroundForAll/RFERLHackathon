using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Swagger
{
    /// <summary>Represents document filter for polymorphic objects.</summary>
    /// <typeparam name="T">Type of abstract class which has inheritances.</typeparam>
    public class PolymorphismDocumentFilter<T> : IDocumentFilter
    {
        /// <summary>Initializes new Polymorphism document filter instance.</summary>
        /// <param name="discriminatorName">Discriminator property's name.</param>
        public PolymorphismDocumentFilter(string discriminatorName)
        {
            DiscriminatorName = discriminatorName;
        }

        /// <summary>Gets discriminator property name.</summary>
        public string DiscriminatorName { get; }

        /// <summary>Applies found derivations from abstract class to swagger documentation.</summary>
        /// <param name="swaggerDoc">Swagger document instance.</param>
        /// <param name="context">Swagger document filter context instance.</param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var parentSchema = context.SchemaRegistry.Definitions.FirstOrDefault(def => def.Key.Equals(typeof(T).Name)).Value;

            // set up a discriminator property (it must be required)
            parentSchema.Discriminator = DiscriminatorName.ToLowerInvariant();
            parentSchema.Required = new List<string> { DiscriminatorName.ToLowerInvariant() };

            if (!parentSchema.Properties.ContainsKey(DiscriminatorName.ToLowerInvariant()))
            {
                parentSchema.Properties.Add(DiscriminatorName.ToLowerInvariant(), new Schema { Type = "string" });
            }

            // register all subclasses
            var derivedTypes = typeof(T).Assembly
                                           .GetTypes()
                                           .Where(x => typeof(T) != x && typeof(T).IsAssignableFrom(x));

            foreach (var item in derivedTypes)
            {
                context.SchemaRegistry.GetOrRegister(item);
            }
        }
    }
}
