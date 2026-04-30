﻿using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces; // <-- 1. Este using es obligatorio
using SportReserva.Data;
using System.Linq;
using SportReserva.Models.Entities;

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
            var nuevaCancha = new Cancha
            {
                Nombre = cancha.Nombre,
                TipoDeporte = cancha.TipoDeporte,
                PrecioHora = cancha.PrecioHora,
                Estado = cancha.Estado,
                Descripcion = cancha.Descripcion,
                EmpresaId = cancha.EmpresaId
            };
            _context.Canchas.Add(nuevaCancha);
            _context.SaveChanges();
        }

        public void Desactivar(int id)
        {
            throw new NotImplementedException();
        }

        public CanchaDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CanchaDTO> ObtenerPorEmpresa(int idEmpresa)
        {
            return _context.Canchas
                .Where(c => c.EmpresaId == idEmpresa)
                .Select(c => new CanchaDTO
                {
                    IdCancha = c.IdCancha,
                    Nombre = c.Nombre,
                    TipoDeporte = c.TipoDeporte,
                    PrecioHora = c.PrecioHora,
                    Estado = c.Estado,
                    Descripcion = c.Descripcion,
                    EmpresaId = c.EmpresaId
                }).ToList();
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