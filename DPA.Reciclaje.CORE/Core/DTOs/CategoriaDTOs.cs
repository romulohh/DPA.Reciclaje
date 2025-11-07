namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class CategoriaDTO
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class CategoriaResponseDTO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
