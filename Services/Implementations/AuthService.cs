using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
using SportReserva.Data;

namespace SportReserva.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly Conexion _context;

        public AuthService(IUsuarioRepository usuarioRepo, Conexion context)
        {
            _usuarioRepo = usuarioRepo;
            _context = context;
        }

        public async Task<ClaimsPrincipal?> Authenticate(LoginDTO loginDTO)
        {
            var usuario = _usuarioRepo.ValidarLogin(loginDTO.NombreUsuario, loginDTO.Clave);

            if (usuario == null) return null;

            // Buscamos el perfil de Cliente real asociado a este Usuario
            var cliente = _context.Clientes.FirstOrDefault(c => c.IdUsuario == usuario.IdUsuario);
            string idClienteStr = cliente != null ? cliente.IdCliente.ToString() : "0";

            // La lógica de creación de identidad se queda aquí, fuera del controlador
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim("IdCliente", idClienteStr), // Guardamos el IdCliente real para las reservas
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "Cliente")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}