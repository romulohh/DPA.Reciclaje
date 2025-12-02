using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IComentarioRepository
    {
        Task<int> AddComentario(Comentario comentario);
        Task<IEnumerable<Comentario>> GetAllComentarios();
        Task<IEnumerable<Comentario>> GetComentariosByProductoAndComprador(int idProducto, int idUsuarioComprador);
        Task<IEnumerable<Comentario>> GetComentariosByProductoAndVendedor(int idProducto, int idUsuarioVendedor);
        Task<Comentario?> GetComentarioById(int id);
        Task<bool> UpdateComentario(Comentario comentario);
        Task<int> AddComentarioImagen(ComentarioImg img);
    }
}
