using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Application.DTOs.Talepler;

public sealed record TalepListeDto(
    int Id,
    string Baslik,
    string Aciklama,
    int KullaniciId,
    string OlusturanKullanici,
    bool Durum,
    DateTime OlusturmaTarihi)
{ }
