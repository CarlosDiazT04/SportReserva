using Microsoft.EntityFrameworkCore;
using SportReserva.Data;
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

        public async Task AgregarAsync(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<Empresa?> ObtenerPorIdAsync(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task<IEnumerable<Empresa>> ObtenerTodasAsync()
        {
            return await _context.Empresas.ToListAsync();
        }
    }
}