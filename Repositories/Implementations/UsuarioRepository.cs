using System;
using System.Collections.Generic;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Actualizar(UsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public void Agregar(UsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public void Desactivar(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioDTO> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public UsuarioDTO ValidarLogin(string username, string clave)
        {
            throw new NotImplementedException();
        }
    }
}