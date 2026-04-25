using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Controllers
{   [Authorize]
    public class HorarioController : Controller
    {
        private readonly IHorarioRepository _horarioRepository;

        public HorarioController(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public IActionResult Index()
        {
            // TODO: var horarios = _horarioRepository.ObtenerTodos();
            return View(new List<HorarioDTO>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HorarioDTO horarioDTO)
        {
            // TODO: Guardar nuevo horario si el ModelState es válido
            return RedirectToAction(nameof(Index));
        }
    }
}
