using TalepYonetimi.Application.Common;
using TalepYonetimi.Application.DTOs.Kullanicilar;
using TalepYonetimi.Application.DTOs.Talepler;
using TalepYonetimi.Domain.Entities;

namespace TalepYonetimi.Application.Interfaces;

public interface ITalepService
{
    Task<IReadOnlyList<TalepListeDto>> TumunuListeleAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TalepListeDto>> KullaniciyaGoreListeleAsync(
    int kullaniciId,
    CancellationToken cancellationToken = default);

    Task<IslemSonucu> OlusturAsync(TalepOlusturDto dto, int kullaniciId, CancellationToken cancellationToken = default);
    Task<TalepGuncelleDto> GetirAsync(int id, CancellationToken cancellationToken = default);
    Task<IslemSonucu> GuncelleAsync(
    TalepGuncelleDto dto,
    CancellationToken cancellationToken = default);

    Task<IslemSonucu> SilAsync(
        int id,
        CancellationToken cancellationToken = default);
}
