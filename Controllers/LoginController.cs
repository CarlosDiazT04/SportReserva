using Microsoft.AspNetCore.Mvc;

namespace SportReserva.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
