using SportReserva.Models.DTOs;

namespace SportReserva.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<ClienteDTO> ObtenerTodas();
        ClienteDTO ObtenerPorId(int id);
        void Agregar(ClienteDTO cliente);
        void Actualizar(ClienteDTO cliente);
        void Desactivar(int id);
    }
}

