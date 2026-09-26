using Microsoft.EntityFrameworkCore;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Domain.Interfaces;
using TalepYonetimi.Infrastructure.Data;

namespace TalepYonetimi.Infrastructure.Repositories;

public sealed class TalepRepository(ApplicationDbContext dbContext)
    : Repository<Talep>(dbContext), ITalepRepository
{

    public async Task<IReadOnlyList<Talep>> KullaniciyaGoreListeleAsync(int kullaniciId, CancellationToken cancellationToken = default)
    =>
        await DbSet
            .AsNoTracking()
            .Where(talep => talep.KullaniciId == kullaniciId)
            .OrderByDescending(talep => talep.OlusturmaTarihi)
            .ToListAsync(cancellationToken);


    public async Task<IReadOnlyList<Talep>> TümünüListeleAsync(CancellationToken cancellationToken = default)
    =>
        await DbSet
            .AsNoTracking()
            .Include(talep => talep.Kullanici)
            .OrderByDescending(talep => talep.OlusturmaTarihi)
            .ToListAsync(cancellationToken);
}