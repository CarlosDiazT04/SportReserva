using Microsoft.AspNetCore.Mvc;

namespace SportReserva.Controllers
{
    public class CanchaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
