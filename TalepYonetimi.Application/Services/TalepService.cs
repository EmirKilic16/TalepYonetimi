
using System.Reflection.Metadata.Ecma335;
using TalepYonetimi.Application.Common;
using TalepYonetimi.Application.DTOs.Kullanicilar;
using TalepYonetimi.Application.DTOs.Talepler;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Domain.Interfaces;

namespace TalepYonetimi.Application.Services
{
    public sealed class TalepService(IUnitOfWork unitOfWork) : ITalepService
    {


        public async Task<IReadOnlyList<TalepListeDto>> TumunuListeleAsync(CancellationToken cancellationToken = default)
        {
            var talepler = await unitOfWork.Talepler.TumunuListeleAsync(cancellationToken);

            return talepler
                .OrderBy(talep => talep.OlusturmaTarihi)
                .ThenBy(talep => talep.Kullanici)
                .Select(talep => new TalepListeDto(
                talep.Id,
                talep.Baslik,
                talep.Aciklama,
                talep.KullaniciId,
                talep.Kullanici.Eposta,
                talep.AktifMi,
                talep.OlusturmaTarihi                
                )).ToList();

        }
        public async Task<IReadOnlyList<TalepListeDto>> KullaniciyaGoreListeleAsync(
                int kullaniciId,
                CancellationToken cancellationToken = default)
        {
            var talepler = await unitOfWork.Talepler.KullaniciyaGoreListeleAsync(
                kullaniciId, cancellationToken);

            return talepler.Select(t => new TalepListeDto(
                t.Id,
                t.Baslik,
                t.Aciklama,
                t.KullaniciId,
                t.Kullanici.Eposta,
                t.AktifMi,
                t.OlusturmaTarihi
            )).ToList();
        }

        public async Task<IslemSonucu> OlusturAsync(
              TalepOlusturDto dto,
              int kullaniciId,
              CancellationToken cancellationToken = default)
        {
            var talep = new Talep
            {
                Baslik = dto.Baslik,
                Aciklama = dto.Aciklama,
                KullaniciId = kullaniciId
            };

            await unitOfWork.Talepler.EkleAsync(talep, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return IslemSonucu.Basari();
        }

        public async Task<IslemSonucu> GuncelleAsync(TalepGuncelleDto dto, CancellationToken cancellationToken = default)
        {
            var talep = await unitOfWork.Talepler.IdYeGoreGetirAsync(dto.Id, cancellationToken);
            if (talep == null)
            {
                return IslemSonucu.Hata("Talep bulunamadı");
            }

            talep.Baslik = dto.Baslik;
            talep.Aciklama = dto.Aciklama;
            talep.AktifMi = dto.AktifMi;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return IslemSonucu.Basari();

        }
        public async Task<IslemSonucu> SilAsync(int id, CancellationToken cancellationToken = default)
        {
            var talep = await unitOfWork.Talepler.IdYeGoreGetirAsync(id, cancellationToken);
            if (talep == null)
            {
                return IslemSonucu.Hata("Talep bulunamadı");
            }

            unitOfWork.Talepler.Sil(talep);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return IslemSonucu.Basari();

        }

        public async Task<TalepGuncelleDto> GetirAsync(int id, CancellationToken cancellationToken)
        {
            var talep = await unitOfWork.Talepler.IdYeGoreGetirAsync(id, cancellationToken);

            if (talep is null)
            {
                return null;
            }
            return new TalepGuncelleDto
            {
                Id = talep.Id,
                Baslik = talep.Baslik,
                Aciklama = talep.Aciklama,
                AktifMi = talep.AktifMi,
            };
        }
    }
}
