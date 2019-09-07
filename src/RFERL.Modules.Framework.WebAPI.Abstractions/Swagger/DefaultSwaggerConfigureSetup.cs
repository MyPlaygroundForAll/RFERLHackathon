using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Swagger
{
    /// <summary>Represents default swagger configuration setup.</summary>
    public class DefaultSwaggerConfigureSetup : IPostConfigureOptions<SwaggerConfigurationOptions>
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes default swagger configure setup instance.</summary>
        /// <param name="configuration">Configuration accessor instance.</param>
        public DefaultSwaggerConfigureSetup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>Configures swagger details by using configuration accessor.</summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public void PostConfigure(string name, SwaggerConfigurationOptions options)
        {
            if (name == Options.DefaultName)
            {
                var configurationSection = _configuration.GetSection("SwaggerDetails");
                options.ApplicationName = string.IsNullOrWhiteSpace(options.ApplicationName) ? configurationSection.GetValue<string>(nameof(options.ApplicationName)) : options.ApplicationName;
                options.Description = string.IsNullOrWhiteSpace(options.Description) ? configurationSection.GetValue<string>(nameof(options.Description)) : options.Description;
                options.ContactEmail = string.IsNullOrWhiteSpace(options.ContactEmail) ? configurationSection.GetValue<string>(nameof(options.ContactEmail)) : options.ContactEmail;
                options.ContactName = string.IsNullOrWhiteSpace(options.ContactName) ? configurationSection.GetValue<string>(nameof(options.ContactName)) : options.ContactName;
                options.Enabled = configurationSection.GetValue<bool>(nameof(options.Enabled));
                options.EndpointUrl = string.IsNullOrWhiteSpace(options.EndpointUrl) ? configurationSection.GetValue<string>(nameof(options.EndpointUrl)) : options.EndpointUrl;
            }
        }
    }
}
