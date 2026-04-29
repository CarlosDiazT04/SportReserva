﻿using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces; // <-- 1. Este using es obligatorio
using SportReserva.Data;
using System.Linq;

namespace SportReserva.Repositories.Implementations
{
    public class CanchaRepository : ICanchaRepository
    {
        private readonly Conexion _context;

        public CanchaRepository(Conexion context)
        {
            _context = context;
        }

        public void Actualizar(CanchaDTO cancha)
        {
            throw new NotImplementedException();
        }

        public void Agregar(CanchaDTO cancha)
        {
            throw new NotImplementedException();
        }

        public void Desactivar(int id)
        {
            throw new NotImplementedException();
        }

        public CanchaDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        // Reemplaza SOLO el método ObtenerTodas() con esto:
        public IEnumerable<CanchaDTO> ObtenerTodas()
        {
            // Obteniendo datos reales desde la base de datos
            return _context.Canchas.Select(c => new CanchaDTO
            {
                IdCancha = c.IdCancha,
                Nombre = c.Nombre,
                TipoDeporte = c.TipoDeporte,
                PrecioHora = c.PrecioHora,
                Estado = c.Estado,
                Descripcion = c.Descripcion
            }).ToList();
        }
    }
}