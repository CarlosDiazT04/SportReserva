using Microsoft.AspNetCore.Mvc;

namespace SportReserva.Controllers
{
    public class ReservaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
