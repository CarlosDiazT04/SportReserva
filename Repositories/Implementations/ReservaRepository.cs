﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using SportReserva.Data;
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using SportReserva.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SportReserva.Repositories.Implementations
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly Conexion _context;

        public ReservaRepository(Conexion context)
        {
            _context = context;
        }

        public void Actualizar(ReservaDTO reserva)
        {
            var entidad = _context.Reservas.Find(reserva.IdReserva);
            if (entidad != null)
            {
                entidad.IdCliente = reserva.IdCliente;
                entidad.IdCancha = reserva.IdCancha;
                entidad.IdHorario = reserva.IdHorario;
                entidad.FechaReserva = reserva.FechaReserva;
                entidad.PrecioTotal = reserva.PrecioTotal;
                entidad.EstadoReserva = reserva.EstadoReserva;
                _context.SaveChanges();
            }
        }

        public void Agregar(ReservaDTO reserva)
        {
            var reservaEntity = new Reserva
            {
                IdCliente = reserva.IdCliente,
                IdCancha = reserva.IdCancha,
                IdHorario = reserva.IdHorario,
                FechaReserva = reserva.FechaReserva,
                FechaRegistro = reserva.FechaRegistro,
                PrecioTotal = reserva.PrecioTotal,
                EstadoReserva = reserva.EstadoReserva
            };
            _context.Reservas.Add(reservaEntity);
            _context.SaveChanges();
        }

        public IEnumerable<ReservaDTO> ObtenerPorClienteId(int clienteId)
        {
            return _context.Reservas
                .Where(r => r.IdCliente == clienteId)
                .Include(r => r.Cancha)
                .Include(r => r.Horario)
                .Select(r => new ReservaDTO
                {
                    IdReserva = r.IdReserva,
                    IdCliente = r.IdCliente,
                    IdCancha = r.IdCancha,
                    IdHorario = r.IdHorario,
                    FechaReserva = r.FechaReserva,
                    FechaRegistro = r.FechaRegistro,
                    PrecioTotal = r.PrecioTotal,
                    EstadoReserva = r.EstadoReserva,
                    CanchaNombre = r.Cancha != null ? r.Cancha.Nombre : string.Empty,
                    HorarioTexto = r.Horario != null ? $"{r.Horario.HoraInicio:hh\\:mm} - {r.Horario.HoraFin:hh\\:mm}" : string.Empty
                }).ToList();
        }

        public IEnumerable<ReservaDTO> ObtenerPorEmpresaId(int empresaId)
        {
            return _context.Reservas
                .Include(r => r.Cancha) 
                .Include(r => r.Horario)
                .Where(r => r.Cancha != null && r.Cancha.EmpresaId == empresaId)
                .Select(r => new ReservaDTO 
                { 
                    IdReserva = r.IdReserva,
                    IdCliente = r.IdCliente,
                    IdCancha = r.IdCancha,
                    IdHorario = r.IdHorario,
                    FechaReserva = r.FechaReserva,
                    FechaRegistro = r.FechaRegistro,
                    PrecioTotal = r.PrecioTotal,
                    EstadoReserva = r.EstadoReserva,
                    CanchaNombre = r.Cancha != null ? r.Cancha.Nombre : string.Empty,
                    HorarioTexto = r.Horario != null ? $"{r.Horario.HoraInicio:hh\\:mm} - {r.Horario.HoraFin:hh\\:mm}" : string.Empty
                })
                .ToList();
        }

        public ReservaDTO ObtenerPorId(int id)
        {
            var r = _context.Reservas.Find(id);
            if (r == null) return null;
            return new ReservaDTO
            {
                IdReserva = r.IdReserva,
                IdCliente = r.IdCliente,
                IdCancha = r.IdCancha,
                IdHorario = r.IdHorario,
                FechaReserva = r.FechaReserva,
                FechaRegistro = r.FechaRegistro,
                PrecioTotal = r.PrecioTotal,
                EstadoReserva = r.EstadoReserva
            };
        }
        
        public IEnumerable<ReservaDTO> ObtenerTodas()
        {
            return _context.Reservas
                .Include(r => r.Cancha)
                .Include(r => r.Horario)
                .Select(r => new ReservaDTO
            {
                IdReserva = r.IdReserva,
                IdCliente = r.IdCliente,
                IdCancha = r.IdCancha,
                IdHorario = r.IdHorario,
                FechaReserva = r.FechaReserva,
                FechaRegistro = r.FechaRegistro,
                PrecioTotal = r.PrecioTotal,
                EstadoReserva = r.EstadoReserva,
                CanchaNombre = r.Cancha != null ? r.Cancha.Nombre : string.Empty,
                HorarioTexto = r.Horario != null ? $"{r.Horario.HoraInicio:hh\\:mm} - {r.Horario.HoraFin:hh\\:mm}" : string.Empty
            }).ToList();
        }

        public bool ExisteCruce(int idCancha, DateTime fechaReserva, int idHorario)
        {
            return _context.Reservas.Any(r =>
                r.IdCancha == idCancha &&
                r.FechaReserva.Date == fechaReserva.Date &&
                r.IdHorario == idHorario &&
                r.EstadoReserva != "Cancelada");
        }

        public bool ActualizarEstado(int idReserva, string nuevoEstado)
        {
            var reserva = _context.Reservas.Find(idReserva);
            if (reserva == null) return false;

            reserva.EstadoReserva = nuevoEstado;
            _context.SaveChanges();
            return true;
        }
    }
}