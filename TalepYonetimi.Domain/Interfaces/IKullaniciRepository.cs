
using TalepYonetimi.Domain.Entities;

namespace TalepYonetimi.Domain.Interfaces
{
    public interface IKullaniciRepository : IRepository<Kullanici>
    {
        Task<Kullanici?> MaileGoreGetirAsync(string eposta, CancellationToken cancellationToken = default);

        Task<bool> MailVarMiAsync(string eposta, int? excludedKullaniciId = null, CancellationToken cancellationToken = default);

        Task<bool> TalepVarmiAsync(int kullaniciId,CancellationToken cancellationToken = default);

    }
}
