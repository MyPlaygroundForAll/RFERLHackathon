using System;
using Microsoft.Extensions.DependencyInjection;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Builder
{
    /// <summary>Represents web api builder for instantiating api configuration.</summary>
    public class WebApiBuilder : IWebApiBuilder
    {
        /// <summary>Initializes web api builder instance.</summary>
        /// <param name="services">IoC container instance.</param>
        public WebApiBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>Gets IoC container instance.</summary>
        public IServiceCollection Services { get; }
    }
}
