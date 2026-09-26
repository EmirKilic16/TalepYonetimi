using TalepYonetimi.Domain.Interfaces;
using TalepYonetimi.Infrastructure.Data;

namespace TalepYonetimi.Infrastructure;

public sealed class UnitOfWork(
    ApplicationDbContext dbContext,
    IKullaniciRepository kullaniciRepository,
    ITalepRepository talepRepository) : IUnitOfWork
{
    public IKullaniciRepository Kullanicilar { get; } = kullaniciRepository;
    public ITalepRepository Talepler { get; } = talepRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync() => dbContext.DisposeAsync();
}