using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class ClienteMapper
    {
        public static ClienteDTO ToDTO(Cliente cliente)
        {
            if (cliente == null) return null;

            return new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                DNI = cliente.DNI,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo,
                IdUsuario = cliente.IdUsuario
            };
        }

        public static Cliente ToEntity(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null) return null;

            return new Cliente
            {
                IdCliente = clienteDTO.IdCliente,
                Nombres = clienteDTO.Nombres,
                Apellidos = clienteDTO.Apellidos,
                DNI = clienteDTO.DNI,
                Telefono = clienteDTO.Telefono,
                Correo = clienteDTO.Correo,
                IdUsuario = clienteDTO.IdUsuario
            };
        }
    }
}
