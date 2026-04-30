using SportReserva.Data;
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using SportReserva.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportReserva.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Conexion _context;

        public ClienteRepository(Conexion context)
        {
            _context = context;
        }

        public void Actualizar(ClienteDTO cliente)
        {
            var entidad = _context.Clientes.Find(cliente.IdCliente);
            if (entidad != null)
            {
                entidad.Nombres = cliente.Nombres;
                entidad.Apellidos = cliente.Apellidos;
                entidad.DNI = cliente.DNI;
                entidad.Telefono = cliente.Telefono;
                entidad.Correo = cliente.Correo;
                _context.SaveChanges();
            }
        }

        public void Agregar(ClienteDTO cliente)
        {
            var entidad = new Cliente
            {
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                DNI = cliente.DNI,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo,
                IdUsuario = cliente.IdUsuario
            };
            _context.Clientes.Add(entidad);
            _context.SaveChanges();
            cliente.IdCliente = entidad.IdCliente;
        }

        public void Desactivar(int id)
        {
            var entidad = _context.Clientes.Find(id);
            if (entidad != null)
            {
                _context.Clientes.Remove(entidad);
                _context.SaveChanges();
            }
        }

        public ClienteDTO ObtenerPorId(int id)
        {
            var c = _context.Clientes.Find(id);
            if (c == null) return null;
            return new ClienteDTO { IdCliente = c.IdCliente, Nombres = c.Nombres, Apellidos = c.Apellidos, DNI = c.DNI, Telefono = c.Telefono, Correo = c.Correo, IdUsuario = c.IdUsuario };
        }

        public IEnumerable<ClienteDTO> ObtenerTodas()
        {
            return _context.Clientes.Select(c => new ClienteDTO { IdCliente = c.IdCliente, Nombres = c.Nombres, Apellidos = c.Apellidos, DNI = c.DNI, Telefono = c.Telefono, Correo = c.Correo, IdUsuario = c.IdUsuario }).ToList();
        }

        public async Task<IEnumerable<ClienteDTO>> ObtenerTodosAsync()
        {
            return await _context.Clientes.Select(c => new ClienteDTO 
            {
                IdCliente = c.IdCliente,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                DNI = c.DNI,
                Telefono = c.Telefono,
                Correo = c.Correo,
                IdUsuario = c.IdUsuario
            }).ToListAsync();
        }

        public async Task<ClienteDTO> ObtenerPorIdAsync(int id)
        {
            var c = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
            if (c == null) return null;
            
            return new ClienteDTO 
            {
                IdCliente = c.IdCliente,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                DNI = c.DNI,
                Telefono = c.Telefono,
                Correo = c.Correo,
                IdUsuario = c.IdUsuario
            };
        }
    }
}