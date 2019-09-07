﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RFERL.Modules.Framework.WebAPI.Abstractions.Swagger;
using Swashbuckle.AspNetCore.Swagger;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Builder
{
    /// <summary>Represents extensions for <see cref="WebApiBuilder"/>.</summary>
    public static class WebApiBuilderExtensions
    {
        private const string RferlModulesPrefix = "RFERL.Modules";

        /// <summary>Registers swagger infrastructure.</summary>
        /// <param name="builder">Web api builder instance.</param>
        /// <returns>Service Collection instance.</returns>
        public static IWebApiBuilder AddSwaggerInfrastructure(this IWebApiBuilder builder)
        {
            builder.AddSwaggerInfrastructure(opt => { });
            return builder;
        }

        /// <summary>Registers swagger infrastructure.</summary>
        /// <param name="builder">Web api builder instance.</param>
        /// <param name="options">Options delegate for configuring swagger.</param>
        /// <returns>Service Collection instance.</returns>
        public static IWebApiBuilder AddSwaggerInfrastructure(this IWebApiBuilder builder, Action<SwaggerConfigurationOptions> options)
        {
            builder.Services.ConfigureOptions<DefaultSwaggerConfigureSetup>();
            builder.Services.AddOptions<SwaggerConfigurationOptions>()
                .Configure(options)
                .Validate(x => !string.IsNullOrEmpty(x.ApplicationName), "Application name is required");

            var serviceProvider = builder.Services.BuildServiceProvider();
            var swaggerConfigurationOptions = serviceProvider.GetRequiredService<IOptions<SwaggerConfigurationOptions>>();
            builder.Services.AddSwaggerGen(c =>
            {
                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, swaggerConfigurationOptions.Value));
                }

                // Locate the XML file being generated by ASP.NET...
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(w => w.GetName().Name.StartsWith("RFERL"));
                foreach (var assembly in assemblies)
                {
                    var xmlFile = $"{assembly.GetName().Name}.XML";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                }

                c.SchemaFilter<SwaggerExcludeSchemaFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.AddFluentValidationRules();

                swaggerConfigurationOptions.Value.SetCustomSwaggerOptions?.Invoke(c);
            });

            return builder;
        }

        /// <summary>Creates version for API.</summary>
        /// <param name="description">Api version description.</param>
        /// <param name="options">Swagger configuration options.</param>
        /// <returns>Information object instance.</returns>
        private static Info CreateInfoForApiVersion(ApiVersionDescription description, SwaggerConfigurationOptions options)
        {
            var appVersion = Assembly.GetEntryAssembly().GetName().Version;
            var info = new Info()
            {
                Title = $"{options.ApplicationName} {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = $"{options.Description} \n Application Version : {appVersion}",
                Contact = new Contact() { Name = options.ContactName, Email = options.ContactEmail }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}