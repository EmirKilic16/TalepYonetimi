namespace TalepYonetimi.Application.DTOs.Kullanicilar;

public sealed record KullaniciListeDto(
    int Id,
    string Ad,
    string Soyad,
    string Eposta,
    DateTime OlusturmaTarihi)
{
    public string AdSoyad => $"{Ad} {Soyad}";
}