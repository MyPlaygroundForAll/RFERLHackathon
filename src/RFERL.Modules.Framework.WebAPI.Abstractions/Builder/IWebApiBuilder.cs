using Microsoft.Extensions.DependencyInjection;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Builder
{
    /// <summary>Represents interface for web api builder.</summary>
    public interface IWebApiBuilder
    {
        /// <summary>Gets services from IoC container.</summary>
        IServiceCollection Services { get; }
    }
}
