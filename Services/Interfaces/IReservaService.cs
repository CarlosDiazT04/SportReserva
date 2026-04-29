using SportReserva.Models.DTOs;

namespace SportReserva.Services
{
    public interface IReservaService
    {
        IEnumerable<ReservaDTO> ObtenerTodas();
        IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId);
        bool RegistrarReserva(ReservaDTO reserva);
        bool CambiarEstado(int idReserva, string nuevoEstado);
    }
}