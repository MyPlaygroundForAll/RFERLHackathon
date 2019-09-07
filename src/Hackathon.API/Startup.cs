using System;
using System.Reflection;
using Hackathon.API.Filters;
using Hackathon.Application.Interfaces;
using Hackathon.Application.Services;
using Hackathon.Domain.Model;
using Hackathon.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RFERL.Modules.Framework.WebAPI.Abstractions;
using RFERL.Modules.Framework.WebAPI.Abstractions.Builder;

namespace Hackathon.API
{
    /// <summary>Represents startup operations such as configuration settings, dependency registration and configuring web api pipeline.</summary>
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">Configuration accessor.</param>
        /// <param name="logger">Logger instance.</param>
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>Gets configuration accessor.</summary>
        public IConfiguration Configuration { get; }

        /// <summary>Configures api dependencies.</summary>
        /// <param name="services">Instance of Microsoft DI IoC container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("Injecting dependencies for starting application.");

            try
            {
                services.AddWebApi(c => c.UseExceptionHandler<HackathonExceptionHandler>())
                    .AddSwaggerInfrastructure();

                services.AddSingleton<ICsvDataImporter, CsvDataImporter>();
                services.AddDbContext<HackathonDbContext>(options => options.UseInMemoryDatabase(databaseName: "HackathonDB"), ServiceLifetime.Singleton);
                services.AddSingleton<IDataRepository, DataRepository>();
                services.AddSingleton<IDataService, DataService>();
                services.AddMediatR(Assembly.GetExecutingAssembly());

                services.AddCors();

                _logger.LogInformation("Dependencies are injected for starting application");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexpected exception occurred while injecting dependencies on startup", ex);
                throw;
            }
        }

        /// <summary>Configures web api http request pipeline.</summary>
        /// <param name="app">Application builder instance.</param>
        /// <param name="env">Hosting environment instance.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("Application configuration is started.");

            try
            {
                var importer = app.ApplicationServices.GetRequiredService<ICsvDataImporter>();
                importer.Import().Wait();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                    options.AllowCredentials();
                });

                app.UseWebApi();

                _logger.LogInformation("Application configuration is completed.");
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogCritical($"Exception occurred due to misconfiguration of {ex.OptionsType}. Details: {string.Join(",", ex.Failures)}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Unexcepted error occurred while configuring application", ex);
                throw;
            }
        }
    }
}
