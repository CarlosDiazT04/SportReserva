using System;
using System.Linq;

namespace SportReserva.Models.Catalogs
{
    public static class TipoDeporteCatalog
    {
        public static readonly string[] Tipos =
        {
            "Fútbol 5",
            "Fútbol 7",
            "Fútbol 11",
            "Vóley",
            "Básquet",
            "Tenis",
            "Pádel"
        };

        public static bool EsValido(string? tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo)) return false;
            return Tipos.Contains(tipo.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}