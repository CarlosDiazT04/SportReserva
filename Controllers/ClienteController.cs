﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Services;

namespace SportReserva.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IEmpresaService _empresaService;

        public ClienteController(IClienteService clienteService, IEmpresaService empresaService)
        {
            _clienteService = clienteService;
            _empresaService = empresaService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            var empresas = await _empresaService.ObtenerTodasAsync();
            ViewBag.TotalEmpresas = empresas.Count();
            return View(clientes);
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> MiPerfil()
        {
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            if (int.TryParse(clienteIdClaim, out int idCliente))
            {
                var cliente = await _clienteService.ObtenerPorIdAsync(idCliente);
                if (cliente != null) return View(cliente);
            }
            
            return NotFound("No se encontró el perfil del cliente.");
        }
    }
}