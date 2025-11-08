namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class FavoritoDTO
    {
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
    }

    public class FavoritoResponseDTO
    {
        public int IdFavorito { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProducto { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
