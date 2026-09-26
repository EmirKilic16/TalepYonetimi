using System.ComponentModel.DataAnnotations;

namespace TalepYonetimi.Application.DTOs.Kullanicilar;

public sealed class KullaniciGuncelleDto
{
    public int Id { get; set; }

    [Display(Name = "Ad")]
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
    public string Ad { get; set; } = string.Empty;

    [Display(Name = "Soyad")]
    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
    public string Soyad { get; set; } = string.Empty;

    [Display(Name = "E-posta")]
    [Required(ErrorMessage = "E-posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [StringLength(150, ErrorMessage = "E-posta en fazla 150 karakter olabilir.")]
    public string Eposta { get; set; } = string.Empty;

    [Display(Name = "Yeni Şifre")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifre 8-100 karakter arasında olmalıdır.")]
    [DataType(DataType.Password)]
    public string? Sifre { get; set; }

    [Display(Name = "Yeni Şifre Tekrar")]
    [Compare(nameof(Sifre), ErrorMessage = "Şifreler eşleşmiyor.")]
    [DataType(DataType.Password)]
    public string? SifreTekrar { get; set; }
}