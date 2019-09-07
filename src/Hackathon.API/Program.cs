using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;

namespace Hackathon.API
{
    /// <summary>Represent entry class of API.</summary>
    public static class Program
    {
        /// <summary>Entry point of API.</summary>
        /// <param name="args">Command arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Run();
        }

        /// <summary>Creates a web host builder.</summary>
        /// <param name="args">Command arguments.</param>
        /// <returns>Web host instance. <see cref="IWebHost"/>.</returns>
        private static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .UseSerilog((provider, hostContext, logConfiguration) =>
                {
                    logConfiguration
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        .Enrich.WithProcessName()
                        .Enrich.WithAspnetcoreHttpcontext(provider)
                        .Enrich.WithProperty("Application", "Hackathon")
                        .WriteTo.File(
                            Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Hackathon.log"),
                            rollingInterval: RollingInterval.Day,
                            outputTemplate: "{Timestamp:HH:mm:ss,zzz} [{Level:U}] [{SourceContext}] {Message:lj}{Properties}{NewLine}{Exception}")
                        .ReadFrom.Configuration(hostContext.Configuration);
                })
                .Build();
    }
}
