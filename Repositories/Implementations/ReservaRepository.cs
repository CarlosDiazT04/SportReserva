using System;
using System.Collections.Generic;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class ReservaRepository : IReservaRepository
    {
        public void Actualizar(ReservaDTO reserva)
        {
            throw new NotImplementedException();
        }

        public void Agregar(ReservaDTO reserva)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId)
        {
            throw new NotImplementedException();
        }

        public ReservaDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservaDTO> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}