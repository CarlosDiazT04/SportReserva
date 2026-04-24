using Microsoft.AspNetCore.Mvc;

namespace SportReserva.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
