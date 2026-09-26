using Microsoft.EntityFrameworkCore;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Domain.Interfaces;
using TalepYonetimi.Infrastructure.Data;

namespace TalepYonetimi.Infrastructure.Repositories;

public sealed class KullaniciRepository(ApplicationDbContext dbContext)
    : Repository<Kullanici>(dbContext), IKullaniciRepository
{

    public Task<Kullanici?> MaileGoreGetirAsync(string eposta, CancellationToken cancellationToken = default)
    =>
        DbSet.AsNoTracking().SingleOrDefaultAsync(
            kullanici => kullanici.Eposta == eposta,
            cancellationToken);

    public Task<bool> MailVarMiAsync(string eposta, int? excludedKullaniciId = null, CancellationToken cancellationToken = default)
   =>
        DbSet.AnyAsync(
            kullanici => kullanici.Eposta == eposta &&
                         (!excludedKullaniciId.HasValue || kullanici.Id != excludedKullaniciId.Value),
            cancellationToken);
    public Task<bool> TalepVarmiAsync(int kullaniciId, CancellationToken cancellationToken = default)
    =>
        DbContext.Talepler.AnyAsync(
            talep => talep.KullaniciId == kullaniciId,
            cancellationToken);
}