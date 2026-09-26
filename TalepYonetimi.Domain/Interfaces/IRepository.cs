using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> IdYeGoreGetirAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ListeleAsync(CancellationToken cancellationToken = default);

        Task EkleAsync(T entity, CancellationToken cancellationToken = default);

        void Güncelle(T entity);
        void Sil(T entity);

    }
}
