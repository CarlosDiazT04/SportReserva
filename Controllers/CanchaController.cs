﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Controllers
{
    public class CanchaController : Controller
    {
        private readonly ICanchaRepository _canchaRepo;

        // Inyectamos el repositorio
        public CanchaController(ICanchaRepository canchaRepo)
        {
            _canchaRepo = canchaRepo;
        }

        public IActionResult Index()
        {
            // Pedimos la lista de canchas y se la enviamos a la vista
            var canchas = _canchaRepo.ObtenerTodas();
            return View(canchas);
        }
    }
}