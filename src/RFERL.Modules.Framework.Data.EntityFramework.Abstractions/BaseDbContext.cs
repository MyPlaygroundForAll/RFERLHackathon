using Microsoft.EntityFrameworkCore;

namespace RFERL.Modules.Framework.Data.EntityFramework.Abstractions
{
    /// <summary>Base implementation of data context.</summary>
    public abstract class BaseDbContext : DbContext
    {
        /// <summary>Constructor for <see cref="BaseDbContext"/>.</summary>
        /// <param name="options">Database context options <see cref="DbContextOptions"/>.</param>
        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
