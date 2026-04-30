// Controllers/EmpresaController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReserva.Models.DTOs;
using SportReserva.Services;

[Authorize(Roles = "Empresa")]
public class EmpresaController : Controller
{
    private readonly SportReserva.Services.IEmpresaService _empresaService;

    public EmpresaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    public IActionResult MisCanchas()
    {
        var idEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == "IdEmpresa")?.Value;
        int idEmpresa = int.TryParse(idEmpresaClaim, out int id) ? id : 0;

        var canchas = _empresaService.ObtenerCanchasDeEmpresa(idEmpresa);
        return View(canchas);
    }

    public IActionResult CrearCancha()
    {
        return View(new CanchaDTO());
    }

    [HttpPost]
    public IActionResult CrearCancha(CanchaDTO canchaDTO)
    {
        if (ModelState.IsValid)
        {
                var idEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == "IdEmpresa")?.Value;
                if (int.TryParse(idEmpresaClaim, out int idEmpresa))
                {
                    canchaDTO.EmpresaId = idEmpresa;
                }

            _empresaService.AgregarCancha(canchaDTO);
            return RedirectToAction("MisCanchas");
        }
        return View(canchaDTO);
    }
}