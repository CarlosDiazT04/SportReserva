using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces; // <-- 1. Este using es obligatorio

namespace SportReserva.Repositories.Implementations
{
    public class CanchaRepository : ICanchaRepository
    {
        public void Actualizar(CanchaDTO cancha)
        {
            throw new NotImplementedException();
        }

        public void Agregar(CanchaDTO cancha)
        {
            throw new NotImplementedException();
        }

        public void Desactivar(int id)
        {
            throw new NotImplementedException();
        }

        public CanchaDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        // Reemplaza SOLO el método ObtenerTodas() con esto:
        public IEnumerable<CanchaDTO> ObtenerTodas()
        {
            // Datos quemados temporales usando tus variables exactas
            return new List<CanchaDTO>
            {
                new CanchaDTO { IdCancha = 1, Nombre = "Cancha Principal (Sintético)", TipoDeporte = "Fútbol 7", PrecioHora = 80.00m, Estado = "Disponible", Descripcion = "Césped sintético de última generación." },
                new CanchaDTO { IdCancha = 2, Nombre = "La Jaula", TipoDeporte = "Fútbol 5", PrecioHora = 50.00m, Estado = "Disponible", Descripcion = "Ideal para partidos rápidos y técnicos." },
                new CanchaDTO { IdCancha = 3, Nombre = "Estadio Centenario", TipoDeporte = "Fútbol 11", PrecioHora = 150.00m, Estado = "Mantenimiento", Descripcion = "Campo de medidas oficiales y graderías." }
            };
        }
    }
}