using System;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Swagger
{
    /// <summary>Represents swagger configuration options.</summary>
    public class SwaggerConfigurationOptions
    {
        /// <summary>Initializes new swagger configuration options.</summary>
        public SwaggerConfigurationOptions()
        {
            Enabled = false;
            EndpointUrl = "/swagger/v1/swagger.json";
            SetCustomSwaggerOptions = opt => { };
        }

        /// <summary>Gets or sets application name.</summary>
        public string ApplicationName { get; set; }

        /// <summary>Gets or sets application description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets contact name.</summary>
        public string ContactName { get; set; }

        /// <summary>Gets or sets contact email.</summary>
        public string ContactEmail { get; set; }

        /// <summary>Gets or sets a value indicating whether swagger enabled or not.</summary>
        public bool Enabled { get; set; }

        /// <summary>Gets or sets swagger endpoint url.</summary>
        public string EndpointUrl { get; set; }

        /// <summary>Gets custom swagger options delegate.</summary>
        public Action<SwaggerGenOptions> SetCustomSwaggerOptions { get; private set; }

        /// <summary>Adds additional swagger options.</summary>
        /// <param name="options">Swagger generation options delegate.</param>
        public void AddAdditionalSwaggerOptions(Action<SwaggerGenOptions> options)
        {
            SetCustomSwaggerOptions = options;
        }
    }
}
