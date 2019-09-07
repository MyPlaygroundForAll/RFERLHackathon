using System;

namespace RFERL.Modules.Framework.Common.Attributes
{
    /// <summary>Swagger excluded attribute for marking excluded properties.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }
}
