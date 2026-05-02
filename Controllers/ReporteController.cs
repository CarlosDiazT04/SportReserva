﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReporteController : Controller
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteController(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public IActionResult Index()
        {
            var reporte = _reporteRepository.GenerarReporteGeneral();
            return View(reporte);
        }
    }
}
