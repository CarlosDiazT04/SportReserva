using SportReserva.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<ClienteDTO> ObtenerTodas();
        ClienteDTO ObtenerPorId(int id);
        void Agregar(ClienteDTO cliente);
        void Actualizar(ClienteDTO cliente);
        void Desactivar(int id);
        Task<IEnumerable<ClienteDTO>> ObtenerTodosAsync();
        Task<ClienteDTO> ObtenerPorIdAsync(int id);
    }
}
