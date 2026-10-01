using TalepYonetimi.Application.Common;
using TalepYonetimi.Application.DTOs;
using TalepYonetimi.Application.DTOs.Kullanicilar;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Application.Security;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Domain.Interfaces;

namespace TalepYonetimi.Application.Services;

public sealed class KullaniciService(
    IUnitOfWork unitOfWork,
    ISifreHasher sifreHasher) : IKullaniciService
{
    public async Task<IReadOnlyList<KullaniciListeDto>> ListeleAsync(
        CancellationToken cancellationToken = default)
    {
        var kullanicilar = await unitOfWork.Kullanicilar.ListeleAsync(cancellationToken);

        return kullanicilar
            .OrderBy(kullanici => kullanici.Ad)
            .ThenBy(kullanici => kullanici.Soyad)
            .Select(kullanici => new KullaniciListeDto(
                kullanici.Id,
                kullanici.Ad,
                kullanici.Soyad,
                kullanici.Eposta,
                kullanici.AdminMi,
                kullanici.OlusturmaTarihi))
            .ToList();
    }

    public async Task<KullaniciGuncelleDto?> GuncellemeIcinGetirAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var kullanici = await unitOfWork.Kullanicilar.IdYeGoreGetirAsync(id, cancellationToken);
        if (kullanici is null)
        {
            return null;
        }

        return new KullaniciGuncelleDto
        {
            Id = kullanici.Id,
            Ad = kullanici.Ad,
            Soyad = kullanici.Soyad,
            Eposta = kullanici.Eposta
        };
    }

    public async Task<IslemSonucu> OlusturAsync(
        KullaniciOlusturDto dto,
        CancellationToken cancellationToken = default)
    {
        var eposta = NormalizeEmail(dto.Eposta);
        if (await unitOfWork.Kullanicilar.MailVarMiAsync(eposta, cancellationToken: cancellationToken))
        {
            return IslemSonucu.Hata("Bu eposta adresiyle kayıtlı bir kullanıcı zaten var.");
        }

        var kullanici = new Kullanici
        {
            Ad = dto.Ad.Trim(),
            Soyad = dto.Soyad.Trim(),
            Eposta = eposta,
            SifreHash = sifreHasher.Hash(dto.Sifre)
        };

        await unitOfWork.Kullanicilar.EkleAsync(kullanici, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return IslemSonucu.Basari();
    }

    public async Task<IslemSonucu> GuncelleAsync(
        KullaniciGuncelleDto dto,
        CancellationToken cancellationToken = default)
    {
        var kullanici = await unitOfWork.Kullanicilar.IdYeGoreGetirAsync(dto.Id, cancellationToken);
        if (kullanici is null)
        {
            return IslemSonucu.Hata("Kullanıcı bulunamadı.");
        }

        var eposta = NormalizeEmail(dto.Eposta);
        if (await unitOfWork.Kullanicilar.MailVarMiAsync(
                eposta,
                dto.Id,
                cancellationToken))
        {
            return IslemSonucu.Hata("Bu eposta adresi başka bir kullanıcı tarafından kullanılıyor.");
        }

        kullanici.Ad = dto.Ad.Trim();
        kullanici.Soyad = dto.Soyad.Trim();
        kullanici.Eposta = eposta;

        if (!string.IsNullOrWhiteSpace(dto.Sifre))
        {
            kullanici.SifreHash = sifreHasher.Hash(dto.Sifre);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return IslemSonucu.Basari();
    }

    public async Task<IslemSonucu> SilAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var kullanici = await unitOfWork.Kullanicilar.IdYeGoreGetirAsync(id, cancellationToken);
        if (kullanici is null)
        {
            return IslemSonucu.Hata("Kullanıcı bulunamadı.");
        }

        if (await unitOfWork.Kullanicilar.TalepVarmiAsync(id, cancellationToken))
        {
            return IslemSonucu.Hata(
                "Bu kullanıcıya ait talepler bulunduğu için kullanıcı silinemez.");
        }

        unitOfWork.Kullanicilar.Sil(kullanici);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return IslemSonucu.Basari();
    }
    public async Task<KullaniciListeDto?> GirisAsync(
     GirisDto dto,
     CancellationToken cancellationToken = default)
    {
        var kullanici = await unitOfWork.Kullanicilar.MaileGoreGetirAsync(
            NormalizeEmail(dto.Eposta), cancellationToken);

        if (kullanici is null ||
            !sifreHasher.Verify(dto.Sifre, kullanici.SifreHash))
        {
            return null;
        }

        return new KullaniciListeDto(
            kullanici.Id,
            kullanici.Ad,
            kullanici.Soyad,
            kullanici.Eposta,
            kullanici.AdminMi,
            kullanici.OlusturmaTarihi);
    }

    private static string NormalizeEmail(string eposta) =>
        eposta.Trim().ToLowerInvariant();
}