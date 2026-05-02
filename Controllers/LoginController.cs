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
                if (User.IsInRole("Admin")) return RedirectToAction("Index", "Cancha");
                if (User.IsInRole("Empresa")) return RedirectToAction("Index", "Cancha");
                if (User.IsInRole("Cliente")) return RedirectToAction("MisReservas", "Reserva");
                return RedirectToAction("Index", "Home");
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

                var rol = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (rol == "Admin") return RedirectToAction("Index", "Cancha");
                if (rol == "Empresa") return RedirectToAction("Index", "Cancha");
                if (rol == "Cliente") return RedirectToAction("MisReservas", "Reserva");
                return RedirectToAction("Index", "Home");
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
            if (User.Identity != null && User.Identity.IsAuthenticated) return RedirectToAction("Index", "Cancha");
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


        [HttpGet]
        public IActionResult Forbidden(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}