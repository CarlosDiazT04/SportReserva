using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        IEnumerable<ReservaDTO> ObtenerTodas();
        IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId);
        IEnumerable<ReservaDTO> ObtenerPorEmpresaId(int empresaId);
        ReservaDTO ObtenerPorId(int id);
        void Agregar(ReservaDTO reserva);
        void Actualizar(ReservaDTO reserva);
        bool ExisteCruce(int idCancha, DateTime fechaReserva, int idHorario);
        bool ActualizarEstado(int idReserva, string nuevoEstado);
    }
}