namespace TalepYonetimi.Application.Common;

public sealed record IslemSonucu(bool Basarili, string? HataMesaji = null)
{
    public static IslemSonucu Basari() => new(true);
    public static IslemSonucu Hata(string mesaj) => new(false, mesaj);
}