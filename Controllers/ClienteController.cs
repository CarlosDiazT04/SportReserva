using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportReserva.Data;
using SportReserva.Models.Entities;

namespace SportReserva.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly Conexion _context;

        public ClienteController(Conexion context)
        {
            _context = context;
        }

        // Acceso exclusivo para que el administrador vea todos los clientes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }

        // Acceso para que el cliente logueado vea su propia información
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> MiPerfil()
        {
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            if (int.TryParse(clienteIdClaim, out int idCliente))
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == idCliente);
                if (cliente != null) return View(cliente);
            }
            
            return NotFound("No se encontró el perfil del cliente.");
        }
    }
}