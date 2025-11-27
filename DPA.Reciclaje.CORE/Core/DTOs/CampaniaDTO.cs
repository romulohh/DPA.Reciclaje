using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class CampaniaDTO
    {
        public string Título { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdDistrito { get; set; }
        public int IdUsuario { get; set; }
    }
    public class CampaniaResponseDTO
    {
        public int IdCampania { get; set; }
        public string Título { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DistritoResponseDTO? Distrito { get; set; }
    }

}
