﻿﻿﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportReserva.Services;

namespace SportReserva.Controllers
{
    public class CanchaController : Controller
    {
        private readonly ICanchaService _canchaService;

        // Inyectamos el servicio en lugar del repositorio
        public CanchaController(ICanchaService canchaService)
        {
            _canchaService = canchaService;
        }

        public IActionResult Index()
        {
            // Pedimos la lista de canchas al servicio
            var canchas = _canchaService.ObtenerTodas();
            return View(canchas);
        }
    }
}