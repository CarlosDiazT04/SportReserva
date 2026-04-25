using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public AuthService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public async Task<ClaimsPrincipal?> Authenticate(LoginDTO loginDTO)
        {
            var usuario = _usuarioRepo.ValidarLogin(loginDTO.NombreUsuario, loginDTO.Clave);

            if (usuario == null) return null;

            // La lógica de creación de identidad se queda aquí, fuera del controlador
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "Cliente")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}