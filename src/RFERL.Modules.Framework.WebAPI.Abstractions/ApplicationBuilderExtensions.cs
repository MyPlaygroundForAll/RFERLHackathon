using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RFERL.Modules.Framework.WebAPI.Abstractions.Swagger;

namespace RFERL.Modules.Framework.WebAPI.Abstractions
{
    /// <summary>Represents extensions for configuring web api pipeline.</summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>Provides common method for configuring web api pipeline.</summary>
        /// <param name="app">Application builder instance.</param>
        /// <returns>Application Builder instance.</returns>
        public static IApplicationBuilder UseWebApi(this IApplicationBuilder app)
        {
            var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            var swaggerDocumentationOptions = app.ApplicationServices.GetService<IOptions<SwaggerConfigurationOptions>>();

            if (swaggerDocumentationOptions.Value.Enabled)
            {
                var rewriteOptions = new RewriteOptions();
                rewriteOptions.AddRedirect("^$", "swagger");
                app.UseRewriter(rewriteOptions);
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint(string.Format(swaggerDocumentationOptions.Value.EndpointUrl, description.GroupName), description.GroupName);
                    }
                });
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            return app;
        }
    }
}
