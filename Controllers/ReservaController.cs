﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportReserva.Controllers
{   [Authorize]
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly ICanchaService _canchaService;
        private readonly IHorarioService _horarioService;
        private readonly IEmpresaService _empresaService;

        public ReservaController(IReservaService reservaService, ICanchaService canchaService, IHorarioService horarioService, IEmpresaService empresaService)
        {
            _reservaService = reservaService;
            _canchaService = canchaService;
            _horarioService = horarioService;
            _empresaService = empresaService;
        }

        public IActionResult Index()
        {
            var reservas = _reservaService.ObtenerTodas();
            return View(reservas);
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            _reservaService.ActualizarEstado(id, "Confirmada");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            _reservaService.ActualizarEstado(id, "Cancelada");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Disponibilidad(DateTime? fecha, string deporte)
        {
            DateTime targetDate = fecha ?? DateTime.Now.Date;
            ViewBag.Horarios = _horarioService.ObtenerTodos();
            ViewBag.Canchas = string.IsNullOrEmpty(deporte) ? 
                              _canchaService.ObtenerTodas().ToList() : 
                              _canchaService.ObtenerTodas().Where(c => c.TipoDeporte == deporte).ToList();
            
            var reservasActivas = _reservaService.ObtenerTodas()
                .Where(r => r.FechaReserva.Date == targetDate && r.EstadoReserva != "Cancelada")
                .ToList();
            
            ViewBag.ReservasOcupadas = reservasActivas;

            return View();
        }

        public async Task<IActionResult> Create(int idCancha, DateTime fecha, int idHorario)
        {
            var cancha = _canchaService.ObtenerTodas().FirstOrDefault(c => c.IdCancha == idCancha);
            var horario = _horarioService.ObtenerTodos().FirstOrDefault(h => h.IdHorario == idHorario);

            if (cancha == null || horario == null) return NotFound("Datos no encontrados");
            
            var empresas = await _empresaService.ObtenerTodasAsync();
            var empresa = empresas.FirstOrDefault(e => e.EmpresaId == cancha.EmpresaId);

            var reserva = new ReservaDTO
            {
                IdCancha = idCancha,
                FechaReserva = fecha,
                IdHorario = idHorario,
                PrecioTotal = cancha.PrecioHora 
            };

            ViewBag.CanchaNombre = cancha.Nombre;
            ViewBag.HorarioTexto = $"{horario.HoraInicio:hh\\:mm} - {horario.HoraFin:hh\\:mm}";
            
            ViewBag.TelefonoEmpresa = empresa?.Telefono ?? "No registrado";
            ViewBag.QrEmpresa = string.IsNullOrEmpty(empresa?.UrlQR) ? "https://upload.wikimedia.org/wikipedia/commons/d/d0/QR_code_for_mobile_English_Wikipedia.svg" : empresa?.UrlQR;
            ViewBag.DireccionEmpresa = empresa?.Direccion ?? "No registrada";
            ViewBag.UrlMapa = empresa?.UrlMapa;

            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservaDTO reservaDTO)
        {
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            reservaDTO.IdCliente = int.TryParse(clienteIdClaim, out int id) ? id : 0;

            if (reservaDTO.IdCliente == 0)
            {
                ModelState.AddModelError(string.Empty, "La sesión ha expirado o no se pudo identificar al cliente. Vuelve a iniciar sesión.");
            }

            ModelState.Remove("EstadoReserva");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("IdReserva"); 
            ModelState.Remove("IdCliente"); 

            if (ModelState.IsValid)
            {
                bool existeCruce = _reservaService.ExisteCruce(reservaDTO.IdCancha, reservaDTO.FechaReserva, reservaDTO.IdHorario);

                if (existeCruce)
                {
                    ModelState.AddModelError(string.Empty, "La cancha ya se encuentra reservada en esa fecha y horario. Por favor, elige otro turno.");
                }
                else
                {
                    try 
                    {
                        reservaDTO.EstadoReserva = "Pendiente";
                        reservaDTO.FechaRegistro = DateTime.Now;
                        
                        _reservaService.Agregar(reservaDTO);
                        return RedirectToAction(nameof(MisReservas));
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al procesar el registro de la reserva.");
                    }
                }
            }

            var cancha = _canchaService.ObtenerTodas().FirstOrDefault(c => c.IdCancha == reservaDTO.IdCancha);
            var horario = _horarioService.ObtenerTodos().FirstOrDefault(h => h.IdHorario == reservaDTO.IdHorario);
            ViewBag.CanchaNombre = cancha?.Nombre;
            ViewBag.HorarioTexto = horario != null ? $"{horario.HoraInicio:hh\\:mm} - {horario.HoraFin:hh\\:mm}" : "";
            
            var empresas = await _empresaService.ObtenerTodasAsync();
            var empresa = cancha != null ? empresas.FirstOrDefault(e => e.EmpresaId == cancha.EmpresaId) : null;
            
            ViewBag.TelefonoEmpresa = empresa?.Telefono ?? "No registrado";
            ViewBag.QrEmpresa = string.IsNullOrEmpty(empresa?.UrlQR) ? "https://upload.wikimedia.org/wikipedia/commons/d/d0/QR_code_for_mobile_English_Wikipedia.svg" : empresa?.UrlQR;
            ViewBag.DireccionEmpresa = empresa?.Direccion ?? "No registrada";
            ViewBag.UrlMapa = empresa?.UrlMapa;

            return View(reservaDTO);
        }

        public IActionResult MisReservas()
        {
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            int clienteId = int.TryParse(clienteIdClaim, out int id) ? id : 0;
            var misReservas = _reservaService.ObtenerPorClienteId(clienteId);
            return View(misReservas);
        }
    }
}