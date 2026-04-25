using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        public void Actualizar(ClienteDTO cliente)
        {
            throw new NotImplementedException();
        }

        public void Agregar(ClienteDTO cliente)
        {
            throw new NotImplementedException();
        }

        public void Desactivar(int id)
        {
            throw new NotImplementedException();
        }

        public ClienteDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ClienteDTO> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}