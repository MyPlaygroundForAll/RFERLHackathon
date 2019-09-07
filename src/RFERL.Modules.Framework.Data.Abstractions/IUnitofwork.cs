using System.Threading;
using System.Threading.Tasks;

namespace RFERL.Modules.Framework.Data.Abstractions
{
    /// <summary>Interface for supporting unit of work operations.</summary>
    public interface IUnitOfWork
    {
        /// <summary>Save changes on data context.</summary>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
        /// <returns>Number of effected records on save operation.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>Save entities on data context. (Used for dispatching domain events.)</summary>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
        /// <returns>Return true if operation is successful, else false.</returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}