﻿﻿﻿using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
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
            var entidad = _context.Canchas.Find(cancha.IdCancha);
            if (entidad != null)
            {
                entidad.Nombre = cancha.Nombre;
                entidad.TipoDeporte = cancha.TipoDeporte;
                entidad.PrecioHora = cancha.PrecioHora;
                entidad.Estado = cancha.Estado;
                entidad.Descripcion = cancha.Descripcion;
                entidad.EmpresaId = cancha.EmpresaId;
                _context.SaveChanges();
            }
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
            var entidad = _context.Canchas.Find(id);
            if (entidad != null)
            {
                entidad.Estado = "Inactivo";
                _context.SaveChanges();
            }
        }

        public CanchaDTO ObtenerPorId(int id)
        {
            var c = _context.Canchas.Find(id);
            if (c == null) return null;
            return new CanchaDTO
            {
                IdCancha = c.IdCancha,
                Nombre = c.Nombre,
                TipoDeporte = c.TipoDeporte,
                PrecioHora = c.PrecioHora,
                Estado = c.Estado,
                Descripcion = c.Descripcion,
                EmpresaId = c.EmpresaId
            };
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

        public IEnumerable<CanchaDTO> ObtenerTodas()
        {
            return _context.Canchas.Select(c => new CanchaDTO
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
    }
}