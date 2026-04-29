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

        public bool RegistrarReserva(ReservaDTO reserva)
        {
            // REGLA DE NEGOCIO (Anexo 2): Evitar cruces de horario.
            // Validar que la combinación Cancha + Fecha + Horario no exista.
            bool existeCruce = _reservaRepository.ExisteCruce(reserva.IdCancha, reserva.FechaReserva, reserva.IdHorario);
            
            if (existeCruce)
            {
                return false; // No se puede registrar, horario ocupado
            }

            reserva.FechaRegistro = DateTime.Now;
            reserva.EstadoReserva = "Pendiente";
            _reservaRepository.Agregar(reserva);
            return true;
        }

        public bool CambiarEstado(int idReserva, string nuevoEstado)
        {
            return _reservaRepository.ActualizarEstado(idReserva, nuevoEstado);
        }
    }
}