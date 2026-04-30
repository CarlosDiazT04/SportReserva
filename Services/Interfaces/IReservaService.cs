using SportReserva.Models.DTOs;

public interface IReservaService
    {
        IEnumerable<ReservaDTO> ObtenerTodas();
        IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId);
        void Agregar(ReservaDTO reserva);
        bool ActualizarEstado(int idReserva, string nuevoEstado);
    }

