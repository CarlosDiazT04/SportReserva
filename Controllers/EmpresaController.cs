using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Models.Catalogs;
using SportReserva.Services;
using System.Security.Claims;

namespace SportReserva.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        public async Task<IActionResult> MiPerfil()
        {
            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idUsuarioClaim, out var idUsuario))
            {
                return Forbid();
            }

            var empresa = await _empresaService.ObtenerPorUsuarioIdAsync(idUsuario);
            if (empresa == null)
            {
                return NotFound("No se encontró el perfil de empresa para el usuario autenticado.");
            }

            return View(empresa);
        }

        public IActionResult MisCanchas()
        {
            var idEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == "IdEmpresa")?.Value;
            var idEmpresa = int.TryParse(idEmpresaClaim, out var id) ? id : 0;

            var canchas = _empresaService.ObtenerCanchasDeEmpresa(idEmpresa);
            return View(canchas);
        }

        public IActionResult CrearCancha()
        {
            ViewBag.TiposDeporte = TipoDeporteCatalog.Tipos;
            return View(new CanchaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearCancha(CanchaDTO canchaDTO)
        {
            if (!TipoDeporteCatalog.EsValido(canchaDTO.TipoDeporte))
            {
                ModelState.AddModelError(nameof(canchaDTO.TipoDeporte), "Selecciona un tipo de deporte válido.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.TiposDeporte = TipoDeporteCatalog.Tipos;
                return View(canchaDTO);
            }

            var idEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == "IdEmpresa")?.Value;
            if (int.TryParse(idEmpresaClaim, out var idEmpresa))
            {
                canchaDTO.EmpresaId = idEmpresa;
            }

            _empresaService.AgregarCancha(canchaDTO);
            return RedirectToAction(nameof(MisCanchas));
        }
    }
}