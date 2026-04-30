using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportReserva.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDTO>> ObtenerTodosAsync()
        {
            return await _clienteRepository.ObtenerTodosAsync();
        }

        public async Task<ClienteDTO> ObtenerPorIdAsync(int id)
        {
            return await _clienteRepository.ObtenerPorIdAsync(id);
        }
    }
}