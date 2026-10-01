using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalepYonetimi.Application.DTOs.Kullanicilar;
using TalepYonetimi.Application.DTOs.Talepler;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Application.Services;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Domain.Interfaces;
using TalepYonetimi.Web.Filters;
using TalepYonetimi.Web.Helpers;
namespace TalepYonetimi.Web.Controllers;

[Auth]
public sealed class TaleplerController(ITalepService talepService) : Controller
{

    private int KullaniciId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    private bool adminMi = false;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var kullanici = HttpContext.Session.KullaniciGetir();

        if (kullanici.AdminMi)
        {

            return View(await talepService.TumunuListeleAsync());
        }

        return View(await talepService.KullaniciyaGoreListeleAsync(
    KullaniciId, cancellationToken));
    }
    [HttpGet]
    public IActionResult Create() => View(new TalepOlusturDto());
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TalepOlusturDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var sonuc = await talepService.OlusturAsync(
    dto, KullaniciId, cancellationToken);

        if (!sonuc.Basarili)
        {
            ModelState.AddModelError(nameof(dto.Baslik), sonuc.HataMesaji!);
            return View(dto);
        }
        TempData["BasariMesaji"] = "Talep başarıyla oluşturuldu.";
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var talep = await talepService.GetirAsync(id, cancellationToken);
        return talep is null ? NotFound() : View(talep);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        TalepGuncelleDto dto,
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

        var sonuc = await talepService.GuncelleAsync(dto, cancellationToken);
        if (!sonuc.Basarili)
        {
            ModelState.AddModelError(string.Empty, sonuc.HataMesaji!);
            return View(dto);
        }

        TempData["BasariMesaji"] = "Talep bilgileri güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var sonuc = await talepService.SilAsync(id, cancellationToken);

        TempData[sonuc.Basarili ? "BasariMesaji" : "HataMesaji"] = sonuc.Basarili
            ? "Talep başarıyla silindi."
            : sonuc.HataMesaji;

        return RedirectToAction(nameof(Index));
    }
}
