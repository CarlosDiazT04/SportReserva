using System.Security.Claims;
using SportReserva.Models.DTOs;

namespace SportReserva.Services
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal?> Authenticate(LoginDTO loginDTO);
    }
}