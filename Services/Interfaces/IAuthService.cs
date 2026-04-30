using System.Security.Claims;
using SportReserva.Models.DTOs;
using System.Threading.Tasks;

namespace SportReserva.Services
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal> Authenticate(LoginDTO loginDTO);
        Task<ResultadoRegistroDTO> RegistrarClienteAsync(RegistroDTO dto);
        Task<ResultadoRegistroDTO> RegistrarEmpresaAsync(RegistroEmpresaDTO dto);
    }
}