﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Services;
using System.Security.Claims;

namespace SportReserva.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Cancha");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            var principal = await _authService.Authenticate(loginDTO);

            if (principal != null)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                    });

                return RedirectToAction("Index", "Cancha");
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View(loginDTO);
        }

        [HttpGet]
        public IActionResult Registro()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated) return RedirectToAction("Index", "Cancha");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroDTO dto)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _authService.RegistrarClienteAsync(dto);
                if (!resultado.Exito)
                {
                    ModelState.AddModelError(string.Empty, resultado.Mensaje);
                    return View(dto);
                }

                return RedirectToAction("Index");
            }
            return View(dto);
        }

        [HttpGet]
        public IActionResult RegistroEmpresa()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated) return RedirectToAction("MisCanchas", "Empresa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistroEmpresa(RegistroEmpresaDTO dto)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _authService.RegistrarEmpresaAsync(dto);
                if (!resultado.Exito)
                {
                    ModelState.AddModelError(string.Empty, resultado.Mensaje);
                    return View(dto);
                }

                return RedirectToAction("Index"); 
            }
            return View(dto);
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}