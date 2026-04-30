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

            // La lógica de creación de identidad se queda aquí, fuera del controlador
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "Cliente")
            };

            // Inyectamos el ID específico dependiendo del tipo de perfil del usuario
            if (usuario.Rol == "Cliente")
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.IdUsuario == usuario.IdUsuario);
                if (cliente != null) claims.Add(new Claim("IdCliente", cliente.IdCliente.ToString()));
            }
            else if (usuario.Rol == "Empresa")
            {
                var empresa = _context.Empresas.FirstOrDefault(e => e.IdUsuario == usuario.IdUsuario);
                if (empresa != null) claims.Add(new Claim("IdEmpresa", empresa.EmpresaId.ToString()));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}