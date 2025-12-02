using System;

namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class ProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Marca { get; set; }
        public int? IdCategoria { get; set; }
        public string? Motivo { get; set; }
        public string? Estado { get; set; }
        public string? Disponible { get; set; }
        public decimal? Precio { get; set; }
        public int? IdUsuario { get; set; }
    }

    public class ProductoResponseDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Marca { get; set; }
        public string? Motivo { get; set; }
        public string? Estado { get; set; }
        public string? Disponible { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? FechaPublicacion { get; set; }

        // Agregado para publicar imagen principal
        public string? Imagen { get; set; }

        public CategoriaResponseDTO? Categoria { get; set; }

        public UsuarioNestedDTO? Usuario { get; set; }
    }


    public class UsuarioNestedDTO
    {
        public int IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? Email { get; set; }
        public DistritoNestedDTO? Distrito { get; set; }
    }

    public class DistritoNestedDTO
    {
        public int IdDistrito { get; set; }
        public string? Nombre { get; set; }
        public DepartamentoNestedDTO? Departamento { get; set; }
        public ProvinciaNestedDTO? Provincia { get; set; }
    }

    public class ProvinciaNestedDTO
    {
        public int IdProvincia { get; set; }
        public string? Nombre { get; set; }
    }

    public class DepartamentoNestedDTO
    {
        public int IdDepartamento { get; set; }
        public string? Nombre { get; set; }
        public PaisNestedDTO? Pais { get; set; }
    }

    public class PaisNestedDTO
    {
        public int IdPais { get; set; }
        public string? Nombre { get; set; }
    }
}
