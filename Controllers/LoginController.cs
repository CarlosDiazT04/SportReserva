﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Controllers
{   [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return View(loginDTO);

            // TODO: Agregar lógica de autenticación usando _usuarioRepository
            // Si el login es exitoso, redirigimos al panel principal (ej. Reservas)
            return RedirectToAction("Index", "Reserva");
        }
    }
}
