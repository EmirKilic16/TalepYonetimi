using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalepYonetimi.Domain.Entities;

namespace TalepYonetimi.Domain.Interfaces
{
    public interface ITalepRepository : IRepository<Talep>
    {
        Task<IReadOnlyList<Talep>> TumunuListeleAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Talep>> KullaniciyaGoreListeleAsync(int kullaniciId,CancellationToken cancellationToken = default);
    }
}
