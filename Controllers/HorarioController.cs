using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Controllers
{   
    [Authorize(Roles = "Admin")]
    public class HorarioController : Controller
    {
        private readonly IHorarioRepository _horarioRepository;

        public HorarioController(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public IActionResult Index()
        {
            var horarios = _horarioRepository.ObtenerTodos();
            return View(horarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HorarioDTO horarioDTO)
        {
            if (ModelState.IsValid)
            {
                _horarioRepository.Agregar(horarioDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(horarioDTO);
        }
    }
}
