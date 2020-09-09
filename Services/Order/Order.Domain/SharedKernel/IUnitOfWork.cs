using System;
using System.Threading.Tasks;

namespace Ordering.Domain.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        Task<bool> SaveEntitiesAsync();
    }
}