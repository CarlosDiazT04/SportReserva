﻿﻿﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
using SportReserva.Services;

namespace SportReserva.Controllers
{   [Authorize]
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly ICanchaService _canchaService;
        private readonly IHorarioRepository _horarioRepo;

        public ReservaController(IReservaService reservaService, ICanchaService canchaService, IHorarioRepository horarioRepo)
        {
            _reservaService = reservaService;
            _canchaService = canchaService;
            _horarioRepo = horarioRepo;
        }

        public IActionResult Index()
        {
            var reservas = _reservaService.ObtenerTodas();
            return View(reservas);
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            _reservaService.CambiarEstado(id, "Confirmada");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            _reservaService.CambiarEstado(id, "Cancelada");
            return RedirectToAction(nameof(Index));
        }

        // GET: Para que el cliente busque disponibilidad por fecha/deporte
        public IActionResult Disponibilidad(DateTime? fecha, string deporte)
        {
            DateTime targetDate = fecha ?? DateTime.Now.Date;
            ViewBag.Horarios = _horarioRepo.ObtenerTodos();
            ViewBag.Canchas = string.IsNullOrEmpty(deporte) ? 
                              _canchaService.ObtenerTodas().ToList() : 
                              _canchaService.ObtenerTodas().Where(c => c.TipoDeporte == deporte).ToList();
            
            // Cruce de reservas: Filtramos las que coincidan con la fecha y no estén canceladas
            var reservasActivas = _reservaService.ObtenerTodas()
                .Where(r => r.FechaReserva.Date == targetDate && r.EstadoReserva != "Cancelada")
                .ToList();
            
            ViewBag.ReservasOcupadas = reservasActivas;

            return View();
        }

        // GET: Para mostrar el formulario de reserva de una cancha específica
        public IActionResult Create(int idCancha, DateTime fecha, int idHorario)
        {
            var cancha = _canchaService.ObtenerTodas().FirstOrDefault(c => c.IdCancha == idCancha);
            var horario = _horarioRepo.ObtenerTodos().FirstOrDefault(h => h.IdHorario == idHorario);

            if (cancha == null || horario == null) return NotFound("Datos no encontrados");

            var reserva = new ReservaDTO
            {
                IdCancha = idCancha,
                FechaReserva = fecha,
                IdHorario = idHorario,
                PrecioTotal = cancha.PrecioHora // Asumimos que 1 turno = PrecioHora completo
            };

            ViewBag.CanchaNombre = cancha.Nombre;
            ViewBag.HorarioTexto = $"{horario.HoraInicio:hh\\:mm} - {horario.HoraFin:hh\\:mm}";

            return View(reserva);
        }

        // POST: Para procesar la reserva que hace el cliente
        [HttpPost]
        public IActionResult Create(ReservaDTO reservaDTO)
        {
            // Extraemos el ID del Cliente desde los Claims del usuario logueado
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            reservaDTO.IdCliente = int.TryParse(clienteIdClaim, out int id) ? id : 0;

            if (reservaDTO.IdCliente == 0)
            {
                ModelState.AddModelError(string.Empty, "La sesión ha expirado o no se pudo identificar al cliente. Vuelve a iniciar sesión.");
            }

            // Ignoramos la validación de estos campos porque se llenan internamente en el Servicio
            ModelState.Remove("EstadoReserva");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("IdReserva"); // Es autogenerado, lo ignoramos
            ModelState.Remove("IdCliente"); // Ya lo asignamos manualmente, lo ignoramos

            if (ModelState.IsValid)
            {
                // Cruce de reservas (Backend): Validamos que no exista otra reserva activa en ese turno
                bool existeCruce = _reservaService.ObtenerTodas()
                    .Any(r => r.IdCancha == reservaDTO.IdCancha 
                           && r.FechaReserva.Date == reservaDTO.FechaReserva.Date 
                           && r.IdHorario == reservaDTO.IdHorario 
                           && r.EstadoReserva != "Cancelada");

                if (existeCruce)
                {
                    ModelState.AddModelError(string.Empty, "La cancha ya se encuentra reservada en esa fecha y horario. Por favor, elige otro turno.");
                }
                else
                {
                    bool registroExitoso = _reservaService.RegistrarReserva(reservaDTO);
                    if (registroExitoso)
                    {
                        return RedirectToAction(nameof(MisReservas));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al procesar el registro de la reserva.");
                    }
                }
            }

            // Si falla la validación, recargamos los datos visuales para no romper la pantalla
            var cancha = _canchaService.ObtenerTodas().FirstOrDefault(c => c.IdCancha == reservaDTO.IdCancha);
            var horario = _horarioRepo.ObtenerTodos().FirstOrDefault(h => h.IdHorario == reservaDTO.IdHorario);
            ViewBag.CanchaNombre = cancha?.Nombre;
            ViewBag.HorarioTexto = horario != null ? $"{horario.HoraInicio:hh\\:mm} - {horario.HoraFin:hh\\:mm}" : "";

            return View(reservaDTO);
        }

        public IActionResult MisReservas()
        {
            // Asumiendo que guardaste el Id del cliente en los Claims durante el Login
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            int clienteId = int.TryParse(clienteIdClaim, out int id) ? id : 0;
            var misReservas = _reservaService.ObtenerPorClienteId(clienteId);
            return View(misReservas);
        }
    }
}
