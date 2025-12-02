using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class ComentarioDTO
    {
        public int? IdUsuarioVendedor { get; set; }
        public int? IdUsuarioComprador { get; set; }
        public int? IdProducto { get; set; }
        public string? Texto { get; set; }
        public string? Calificacion { get; set; }
        public string? Estado { get; set; }
        public List<string>? Imagenes { get; set; }
    }

    public class ComentarioResponseDTO
    {
        public int IdComentario { get; set; }
        public int? IdUsuarioVendedor { get; set; }
        public int? IdUsuarioComprador { get; set; }
        public int? IdProducto { get; set; }
        public string? Texto { get; set; }
        public string? Calificacion { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public List<string>? Imagenes { get; set; }
    }
}
