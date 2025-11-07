using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class UsuarioResponseDTO
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int IdDistrito { get; set; }
        public string Rol { get; set; } = string.Empty;
        public string Situacion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
