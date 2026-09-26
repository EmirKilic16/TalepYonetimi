using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IKullaniciRepository Kullanicilar { get; }

        ITalepRepository Talepler { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
