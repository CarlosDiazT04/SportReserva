using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SportReserva.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ICanchaRepository _canchaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository, ICanchaRepository canchaRepository)
        {
            _empresaRepository = empresaRepository;
            _canchaRepository = canchaRepository;
        }

        public async Task<IEnumerable<EmpresaDTO>> ObtenerTodasAsync()
        {
            return await _empresaRepository.ObtenerTodasAsync();
        }

        public async Task<EmpresaDTO?> ObtenerPorIdAsync(int id)
        {
            return await _empresaRepository.ObtenerPorIdAsync(id);
        }

        public IEnumerable<CanchaDTO> ObtenerCanchasDeEmpresa(int idEmpresa)
        {
            return _canchaRepository.ObtenerTodas().Where(c => c.EmpresaId == idEmpresa);
        }

        public void AgregarCancha(CanchaDTO canchaDTO)
        {
            _canchaRepository.Agregar(canchaDTO);
        }
    }
}
