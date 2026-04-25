using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        IEnumerable<PagoDTO> ObtenerTodos();
        PagoDTO ObtenerPorId(int id);
        void Agregar(PagoDTO pago);
        void Actualizar(PagoDTO pago);
    }
}