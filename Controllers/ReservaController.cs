using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Controllers
{   [Authorize]
    public class ReservaController : Controller
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public IActionResult Index()
        {
            // TODO: var reservas = _reservaRepository.ObtenerTodas();
            return View(new List<ReservaDTO>());
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            // TODO: Cambiar el estado de la reserva a "Confirmada"
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            // TODO: Cambiar el estado de la reserva a "Cancelada"
            return RedirectToAction(nameof(Index));
        }

        // GET: Para que el cliente busque disponibilidad por fecha/deporte
        public IActionResult Disponibilidad(DateTime? fecha, string deporte)
        {
            // TODO: Lógica para buscar canchas libres en esa fecha
            return View();
        }

        // GET: Para mostrar el formulario de reserva de una cancha específica
        public IActionResult Create(int idCancha, DateTime fecha, int idHorario)
        {
            // TODO: Preparar los datos de la cancha y horario seleccionado
            return View();
        }

        // POST: Para procesar la reserva que hace el cliente
        [HttpPost]
        public IActionResult Create(ReservaDTO reservaDTO)
        {
            if (ModelState.IsValid)
            {
                // TODO: Guardar la reserva en la base de datos
                return RedirectToAction(nameof(MisReservas));
            }
            return View(reservaDTO);
        }

        public IActionResult MisReservas()
        {
            // TODO: Obtener el ID del cliente logueado (ej. desde User.Identity o Session)
            // var misReservas = _reservaRepository.ObtenerPorClienteId(clienteId);
            return View(new List<ReservaDTO>());
        }
    }
}
