using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        IEnumerable<ReservaDTO> ObtenerTodas();
        IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId);
        ReservaDTO ObtenerPorId(int id);
        void Agregar(ReservaDTO reserva);
        void Actualizar(ReservaDTO reserva);
    }
}