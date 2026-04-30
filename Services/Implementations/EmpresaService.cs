using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<EmpresaDTO>> ObtenerTodasAsync()
        {
            // Ahora ambos, el Repositorio y el Servicio, hablan el mismo idioma (DTO)
            return await _empresaRepository.ObtenerTodasAsync();
        }

        public async Task<EmpresaDTO?> ObtenerPorIdAsync(int id)
        {
            return await _empresaRepository.ObtenerPorIdAsync(id);
        }
    }
}
