using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); // Página pública principal
    }

    public IActionResult About()
    {
        return View(); // Información de Acerca de Nosotros
    }

    public IActionResult Contact()
    {
        return View(); // Información de Contacto
    }
}