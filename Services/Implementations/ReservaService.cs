using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public IEnumerable<ReservaDTO> ObtenerTodas()
        {
            return _reservaRepository.ObtenerTodas();
        }

        public IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId)
        {
            // Esto asume que agregarás este método a tu IReservaRepository
            return _reservaRepository.ObtenerPorClienteId(clienteId);
        }

        public void Agregar(ReservaDTO reserva)
        {
            _reservaRepository.Agregar(reserva);
        }

        public bool ExisteCruce(int idCancha, DateTime fechaReserva, int idHorario)
        {
            return _reservaRepository.ExisteCruce(idCancha, fechaReserva, idHorario);
        }

        public bool ActualizarEstado(int idReserva, string nuevoEstado)
        {
            return _reservaRepository.ActualizarEstado(idReserva, nuevoEstado);
        }
    }
}