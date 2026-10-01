using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalepYonetimi.Application.DTOs.Kullanicilar;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Application.Services;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Web.Filters;
using TalepYonetimi.Web.Helpers;

namespace TalepYonetimi.Web.Controllers;


public sealed class KullanicilarController(IKullaniciService kullaniciService) : Controller
{
    [HttpGet]
    [Auth]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var kullanicilar = await kullaniciService.ListeleAsync(cancellationToken);
        return View(kullanicilar);
    }

    [HttpGet]
    
    public IActionResult Create() => View(new KullaniciOlusturDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Auth]
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
    [Auth]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var kullanici = await kullaniciService.GuncellemeIcinGetirAsync(id, cancellationToken);
        return kullanici is null ? NotFound() : View(kullanici);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Auth]
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
    [Auth]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var sonuc = await kullaniciService.SilAsync(id, cancellationToken);

        TempData[sonuc.Basarili ? "BasariMesaji" : "HataMesaji"] = sonuc.Basarili
            ? "Kullanıcı başarıyla silindi."
            : sonuc.HataMesaji;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Login() => View(new GirisDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
       GirisDto dto,
       CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var kullanici = await kullaniciService.GirisAsync(
            dto, cancellationToken);

        if (kullanici is null)
        {
            ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            return View(dto);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString()),
            new Claim(ClaimTypes.Name, kullanici.AdSoyad)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        HttpContext.Session.Set("OturumAcmisKullanici", kullanici);

        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult LogOut()
    {
       HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}