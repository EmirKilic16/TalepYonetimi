
using System.ComponentModel.DataAnnotations;


namespace TalepYonetimi.Application.DTOs.Kullanicilar;

public sealed class GirisDto
{
    [Required(ErrorMessage = "E-posta gerekli.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta gir.")]
    public string Eposta { get; set; } = "";

    [Required(ErrorMessage = "Şifre gerekli.")]
    [DataType(DataType.Password)]
    public string Sifre { get; set; } = "";
}

