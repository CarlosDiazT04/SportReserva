using Microsoft.AspNetCore.Mvc;

namespace SportReserva.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
