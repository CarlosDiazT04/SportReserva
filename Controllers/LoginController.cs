﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Services;
using System.Security.Claims;

namespace SportReserva.Controllers
{
    // Permitimos que cualquiera vea el Login, de lo contrario tendríamos un bucle infinito
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        // Inyectamos solo el servicio de autenticación
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Si el usuario ya está logueado y trata de entrar al Login, lo mandamos a las canchas
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Cancha");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Buena práctica de seguridad contra ataques CSRF
        public async Task<IActionResult> Index(LoginDTO loginDTO)
        {
            // 1. Validación básica de los campos (según las DataAnnotations del DTO)
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            // 2. Delegamos la lógica pesada al servicio
            // El servicio nos devuelve el "Principal" (la identidad lista para la cookie)
            var principal = await _authService.Authenticate(loginDTO);

            if (principal != null)
            {
                // 3. Si la identidad es válida, creamos la sesión en el navegador
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // La sesión se mantiene al cerrar el navegador
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) // Tiempo de vida de la sesión
                    });

                // 4. Redirección al centro de mando (Canchas)
                return RedirectToAction("Index", "Cancha");
            }

            // 5. Si el servicio devolvió null, es tarjeta roja: credenciales incorrectas
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

                return RedirectToAction("Index"); // Se envía al login para que inicie sesión
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
            // Limpiamos la cookie de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}