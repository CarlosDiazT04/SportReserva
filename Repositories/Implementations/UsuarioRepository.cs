﻿using System.Linq;
using SportReserva.Data;
using SportReserva.Models.Entities;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Conexion _context;

        public UsuarioRepository(Conexion context)
        {
            _context = context;
        }

        public UsuarioDTO? ValidarLogin(string username, string clave)
        {
            var usuarioDb = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == username && u.Clave == clave);
            
            if (usuarioDb == null) return null;

            return new UsuarioDTO
            {
                IdUsuario = usuarioDb.IdUsuario,
                NombreUsuario = usuarioDb.NombreUsuario,
                Clave = usuarioDb.Clave,
                Rol = usuarioDb.Rol
            };
        }
        public void Actualizar(UsuarioDTO usuario)
        {
            var entidad = _context.Usuarios.Find(usuario.IdUsuario);
            if (entidad != null)
            {
                entidad.NombreUsuario = usuario.NombreUsuario;
                entidad.Clave = usuario.Clave;
                entidad.Rol = usuario.Rol;
                _context.SaveChanges();
            }
        }

        public void Agregar(UsuarioDTO usuario)
        {
            var entidad = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                Rol = usuario.Rol
            };
            _context.Usuarios.Add(entidad);
            _context.SaveChanges();
            usuario.IdUsuario = entidad.IdUsuario;
        }

        public void Desactivar(int id)
        {
            var entidad = _context.Usuarios.Find(id);
            if (entidad != null)
            {
                _context.Usuarios.Remove(entidad);
                _context.SaveChanges();
            }
        }

        public UsuarioDTO ObtenerPorId(int id)
        {
            var u = _context.Usuarios.Find(id);
            if (u == null) return null;
            
            return new UsuarioDTO { IdUsuario = u.IdUsuario, NombreUsuario = u.NombreUsuario, Clave = u.Clave, Rol = u.Rol };
        }

        public IEnumerable<UsuarioDTO> ObtenerTodos()
        {
            return _context.Usuarios.Select(u => new UsuarioDTO
            {
                IdUsuario = u.IdUsuario,
                NombreUsuario = u.NombreUsuario,
                Clave = u.Clave,
                Rol = u.Rol
            }).ToList();
        }

    }
}