using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Services
{
    public class CanchaService : ICanchaService
    {
        private readonly ICanchaRepository _canchaRepository;

        public CanchaService(ICanchaRepository canchaRepository)
        {
            _canchaRepository = canchaRepository;
        }

        public IEnumerable<CanchaDTO> ObtenerTodas()
        {
            return _canchaRepository.ObtenerTodas();
        }
    }
}