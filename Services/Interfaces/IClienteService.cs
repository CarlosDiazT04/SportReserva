using SportReserva.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> ObtenerTodosAsync();
        Task<ClienteDTO> ObtenerPorIdAsync(int id);
    }
}