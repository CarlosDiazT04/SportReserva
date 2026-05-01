using SportReserva.Models.DTOs;

public interface IReservaService
    {
        IEnumerable<ReservaDTO> ObtenerTodas();
        IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId);
        IEnumerable<ReservaDTO> ObtenerPorEmpresaId(int empresaId);
        void Agregar(ReservaDTO reserva);
        bool ActualizarEstado(int idReserva, string nuevoEstado);
        bool ExisteCruce(int idCancha, DateTime fechaReserva, int idHorario);
}

