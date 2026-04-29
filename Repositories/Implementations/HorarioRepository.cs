﻿using System;
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
            throw new NotImplementedException();
        }

        public void Agregar(HorarioDTO horario)
        {
            var nuevoHorario = new Horario
            {
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = "Activo" // Asignamos un estado por defecto
            };
            _context.Horarios.Add(nuevoHorario);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public HorarioDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
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