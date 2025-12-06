using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class CarritoDTO
    {
        public int? IdUsuario { get; set; }
        public string? Estado { get; set; }
        public IEnumerable<CarritoItemDTO>? Items { get; set; }
    }

    public class CarritoItemDTO
    {
        public int? IdProducto { get; set; }
        public decimal? Precio { get; set; }
    }

    public class AddCarritoItemDTO
    {
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
    }

    public class CarritoResponseDTO
    {
        public int IdCarrito { get; set; }
        public UsuarioNestedDTO? Usuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }
        public IEnumerable<CarritoItemResponseDTO>? Items { get; set; }
    }

    public class CarritoItemResponseDTO
    {
        public int IdCarritoItem { get; set; }
        public ProductoNestedDTO? Producto { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class ProductoNestedDTO
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public decimal? Precio { get; set; }
    }
}
