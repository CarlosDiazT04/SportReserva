﻿﻿﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Services;
using System.Security.Claims;
using SportReserva.Data;
using SportReserva.Models.Entities;

namespace SportReserva.Controllers
{
    // Permitimos que cualquiera vea el Login, de lo contrario tendríamos un bucle infinito
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly Conexion _context;

        // Inyectamos solo el servicio de autenticación, no el repositorio
        public LoginController(IAuthService authService, Conexion context)
        {
            _authService = authService;
            _context = context;
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
                if (_context.Usuarios.Any(u => u.NombreUsuario == dto.NombreUsuario))
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario ya está en uso.");
                    return View(dto);
                }

                var nuevoUsuario = new Usuario { NombreUsuario = dto.NombreUsuario, Clave = dto.Clave, Rol = "Cliente" };
                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                var nuevoCliente = new Cliente 
                { 
                    Nombres = dto.Nombres, 
                    Apellidos = dto.Apellidos, 
                    DNI = dto.DNI, 
                    Telefono = dto.Telefono, 
                    Correo = dto.Correo, 
                    IdUsuario = nuevoUsuario.IdUsuario 
                };
                _context.Clientes.Add(nuevoCliente);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Se envía al login para que inicie sesión
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Salir()
        {
            // Limpiamos la cookie de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}