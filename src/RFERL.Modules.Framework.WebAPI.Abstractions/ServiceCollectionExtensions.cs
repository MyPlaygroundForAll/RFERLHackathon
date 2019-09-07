using System;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RFERL.Modules.Framework.WebAPI.Abstractions.Builder;
using RFERL.Modules.Framework.WebAPI.Abstractions.Conventions;
using RFERL.Modules.Framework.WebAPI.Abstractions.Model;
using RFERL.Modules.Framework.WebAPI.Abstractions.ModelBinders;
using RFERL.Modules.Framework.WebAPI.Abstractions.ObjectResults;

namespace RFERL.Modules.Framework.WebAPI.Abstractions
{
    /// <summary>Contains core web api component registrar extensions.</summary>
    public static class ServiceCollectionExtensions
    {
        private const string RferlModulesPrefix = "RFERL.Modules";

        /// <summary>Registers core components of web api infrastructure.</summary>
        /// <param name="services">Service collection for storing dependencies.</param>
        /// <returns>Service collection instance.</returns>
        public static IWebApiBuilder AddWebApi(this IServiceCollection services)
        {
            var builder = services.AddWebApi(setup => { });
            return builder;
        }

        /// <summary>Registers core components of web api infrastructure.</summary>
        /// <param name="services">Service collection for storing dependencies.</param>
        /// <param name="setup">Web api configuration options delegate.</param>
        /// <returns>Returns Web api builder instance.</returns>
        public static IWebApiBuilder AddWebApi(this IServiceCollection services, Action<WebApiConfigurationOptions> setup)
        {
            services.AddOptions();
            var configurationOptions = new WebApiConfigurationOptions();
            setup?.Invoke(configurationOptions);

            services.AddMvcCore(opt =>
            {
                opt.Filters.Add(configurationOptions.ExceptionHandlerType);
                opt.AllowEmptyInputInBodyModelBinding = true;
                var readerFactory = services.BuildServiceProvider().GetRequiredService<IHttpRequestStreamReaderFactory>();
                opt.ModelBinderProviders.Insert(0, new IdentityModelBinderProvider(opt.InputFormatters, readerFactory));
                opt.Conventions.Insert(0, new RouteConvention(new RouteAttribute("api/v{version:apiVersion}")));

                configurationOptions.SetCustomMvcOptions(opt);
            })
                .AddJsonFormatters()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter()
                    { NamingStrategy = new CamelCaseNamingStrategy() });

                    configurationOptions.SetCustomMvcJsonOptions(opt);
                })
                .AddFluentValidation(o =>
                {
                    o.LocalizationEnabled = false;

                    // Gets assemblies in domain which starts with modules prefix and has fluent validator classes
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(w => w.GetName()
                                .Name.StartsWith(RferlModulesPrefix) && w.GetTypes().Any(t => typeof(IValidator).IsAssignableFrom(t)));

                    o.RegisterValidatorsFromAssemblies(assemblies);
                    o.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.SubstituteApiVersionInUrl = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.Configure<ApiBehaviorOptions>(behaviourOptions =>
            {
                behaviourOptions.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(w => w.Value.Errors.Any())
                                    .Select(s => new ValidationResult.ValidationError(s.Key, string.Join(",", s.Value.Errors.Select(e => e.ErrorMessage)))).ToList();
                    return new ValidationFailedResult(errors);
                };
            });

            var builder = new WebApiBuilder(services);
            return builder;
        }
    }
}
