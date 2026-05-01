using Microsoft.EntityFrameworkCore;
using SportReserva.Data;
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using SportReserva.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Repositories.Implementations
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly Conexion _context;

        public EmpresaRepository(Conexion context)
        {
            _context = context;
        }

        public async Task AgregarAsync(EmpresaDTO empresa)
        {
            var entidad = new Empresa
            {
                Nombre = empresa.Nombre,
                RUC = empresa.RUC,
                Direccion = empresa.Direccion,
                Telefono = empresa.Telefono,
                Email = empresa.Correo,
                UrlMapa = empresa.UrlMapa,
                NumeroBilletera = empresa.NumeroBilletera,
                IdUsuario = empresa.IdUsuario,
                FechaRegistro = empresa.FechaRegistro
            };
            await _context.Empresas.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public Task AgregarAsync(Empresa empresa)
        {
            throw new NotImplementedException();
        }

        public async Task<EmpresaDTO?> ObtenerPorIdAsync(int id)
        {
            var e = await _context.Empresas.FindAsync(id);
            if (e == null) return null;
            return new EmpresaDTO
            {
                EmpresaId = e.EmpresaId,
                Nombre = e.Nombre,
                RUC = e.RUC,
                Direccion = e.Direccion,
                Telefono = e.Telefono,
                Correo = e.Email,
                UrlMapa = e.UrlMapa,
                NumeroBilletera = e.NumeroBilletera,
                IdUsuario = e.IdUsuario,
                FechaRegistro = e.FechaRegistro
            };
        }

        public async Task<IEnumerable<EmpresaDTO>> ObtenerTodasAsync()
        {
            return await _context.Empresas.Select(e => new EmpresaDTO
            {
                EmpresaId = e.EmpresaId,
                Nombre = e.Nombre,
                RUC = e.RUC,
                Direccion = e.Direccion,
                Telefono = e.Telefono,
                Correo = e.Email,
                UrlMapa = e.UrlMapa,
                NumeroBilletera = e.NumeroBilletera,
                IdUsuario = e.IdUsuario,
                FechaRegistro = e.FechaRegistro
            }).ToListAsync();
        }
    }
}