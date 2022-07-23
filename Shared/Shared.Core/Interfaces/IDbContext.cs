using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Core.Interfaces
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();
    }
}