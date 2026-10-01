using TalepYonetimi.Application.Common;
using TalepYonetimi.Application.DTOs.Kullanicilar;

namespace TalepYonetimi.Application.Interfaces;

public interface IKullaniciService
{
    Task<IReadOnlyList<KullaniciListeDto>> ListeleAsync(
        CancellationToken cancellationToken = default);

    Task<KullaniciGuncelleDto?> GuncellemeIcinGetirAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IslemSonucu> OlusturAsync(
        KullaniciOlusturDto dto,
        CancellationToken cancellationToken = default);

    Task<IslemSonucu> GuncelleAsync(
        KullaniciGuncelleDto dto,
        CancellationToken cancellationToken = default);

    Task<IslemSonucu> SilAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<KullaniciListeDto?> GirisAsync(
      GirisDto dto,
      CancellationToken cancellationToken = default);
}