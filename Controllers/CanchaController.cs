﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Services;
using System.Security.Claims;

namespace SportReserva.Controllers
{
    [Authorize]
    public class CanchaController : Controller
    {
        private readonly ICanchaService _canchaService;
        private readonly IEmpresaService _empresaService;

        public CanchaController(ICanchaService canchaService, IEmpresaService empresaService)
        {
            _canchaService = canchaService;
            _empresaService = empresaService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var todasLasCanchas = _canchaService.ObtenerTodas();
                return View(todasLasCanchas);
            }

            if (User.IsInRole("Empresa"))
            {
                var idEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == "IdEmpresa")?.Value;
                if (int.TryParse(idEmpresaClaim, out var idEmpresa))
                {
                    var canchasDeLaEmpresa = _empresaService.ObtenerCanchasDeEmpresa(idEmpresa);
                    return View(canchasDeLaEmpresa);
                }

                var idUsuarioString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

                if (int.TryParse(idUsuarioString, out var idUsuario))
                {
                    var empresa = await _empresaService.ObtenerPorUsuarioIdAsync(idUsuario);
                    if (empresa == null)
                    {
                        return RedirectToAction("MiPerfil", "Empresa");
                    }

                    var canchasDeLaEmpresa = _empresaService.ObtenerCanchasDeEmpresa(empresa.EmpresaId);
                    return View(canchasDeLaEmpresa);
                }
            }

            return Forbid();
        }
    }
}