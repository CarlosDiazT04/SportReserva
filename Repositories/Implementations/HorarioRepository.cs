﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using SportReserva.Data;
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly Conexion _context;

        public HorarioRepository(Conexion context)
        {
            _context = context;
        }

        public void Actualizar(HorarioDTO horario)
        {
            var entidad = _context.Horarios.Find(horario.IdHorario);
            if (entidad != null)
            {
                entidad.HoraInicio = horario.HoraInicio;
                entidad.HoraFin = horario.HoraFin;
                entidad.Estado = horario.Estado;
                _context.SaveChanges();
            }
        }

        public void Agregar(HorarioDTO horario)
        {
            var nuevoHorario = new Horario
            {
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = "Activo"
            };
            _context.Horarios.Add(nuevoHorario);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var entidad = _context.Horarios.Find(id);
            if (entidad != null)
            {
                _context.Horarios.Remove(entidad);
                _context.SaveChanges();
            }
        }

        public HorarioDTO ObtenerPorId(int id)
        {
            var h = _context.Horarios.Find(id);
            if (h == null) return null;
            return new HorarioDTO
            {
                IdHorario = h.IdHorario,
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin,
                Estado = h.Estado
            };
        }

        public IEnumerable<HorarioDTO> ObtenerTodos()
        {
            return _context.Horarios.Select(h => new HorarioDTO
            {
                IdHorario = h.IdHorario,
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin,
                Estado = h.Estado
            }).ToList();
        }
    }
}