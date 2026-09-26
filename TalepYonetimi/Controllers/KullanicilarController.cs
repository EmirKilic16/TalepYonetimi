using Microsoft.AspNetCore.Mvc;
using TalepYonetimi.Application.DTOs.Kullanicilar;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Application.Services;

namespace TalepYonetimi.Web.Controllers;

public sealed class KullanicilarController(IKullaniciService kullaniciService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var kullanicilar = await kullaniciService.ListeleAsync(cancellationToken);
        return View(kullanicilar);
    }

    [HttpGet]
    public IActionResult Create() => View(new KullaniciOlusturDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        KullaniciOlusturDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var sonuc = await kullaniciService.OlusturAsync(dto, cancellationToken);
        if (!sonuc.Basarili)
        {
            ModelState.AddModelError(nameof(dto.Eposta), sonuc.HataMesaji!);
            return View(dto);
        }

        TempData["BasariMesaji"] = "Kullanıcı başarıyla oluşturuldu.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var kullanici = await kullaniciService.GuncellemeIcinGetirAsync(id, cancellationToken);
        return kullanici is null ? NotFound() : View(kullanici);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        KullaniciGuncelleDto dto,
        CancellationToken cancellationToken)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var sonuc = await kullaniciService.GuncelleAsync(dto, cancellationToken);
        if (!sonuc.Basarili)
        {
            ModelState.AddModelError(string.Empty, sonuc.HataMesaji!);
            return View(dto);
        }

        TempData["BasariMesaji"] = "Kullanıcı bilgileri güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var sonuc = await kullaniciService.SilAsync(id, cancellationToken);

        TempData[sonuc.Basarili ? "BasariMesaji" : "HataMesaji"] = sonuc.Basarili
            ? "Kullanıcı başarıyla silindi."
            : sonuc.HataMesaji;

        return RedirectToAction(nameof(Index));
    }
}