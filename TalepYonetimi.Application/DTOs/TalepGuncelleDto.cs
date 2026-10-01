using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalepYonetimi.Application.DTOs.Talepler;

public sealed class TalepGuncelleDto
{
    public int Id { get; set; }

    [Display(Name = "Başlık")]
    [Required(ErrorMessage = "Başlık alanı zorunludur.")]
    [StringLength(20, ErrorMessage = "Başlık en fazla 20 karakter olabilir.")]
    public string Baslik { get; set; } = string.Empty;
    [Display(Name = "Açıklama")]
    [Required(ErrorMessage = "Açıklama zorunludur.")]
    [StringLength(2000, ErrorMessage = "Açıklama en fazla {1} karakter olabilir")]
    public string Aciklama { get; set; } = string.Empty;

    public bool AktifMi { get; set; } = default;
}
