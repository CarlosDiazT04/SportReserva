﻿﻿﻿﻿﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportReserva.Services;

namespace SportReserva.Controllers
{
    [Authorize]
    public class CanchaController : Controller
    {
        private readonly ICanchaService _canchaService;

        public CanchaController(ICanchaService canchaService)
        {
            _canchaService = canchaService;
        }

        public IActionResult Index()
        {
            var canchas = _canchaService.ObtenerTodas();
            return View(canchas);
        }
    }
}