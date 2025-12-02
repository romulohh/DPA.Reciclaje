using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IComentarioService
    {
        Task<int> CreateAsync(ComentarioDTO dto);
        Task<IEnumerable<ComentarioResponseDTO>> GetAllAsync();
        Task<IEnumerable<ComentarioResponseDTO>> GetByProductoAndCompradorAsync(int idProducto, int idUsuarioComprador);
        Task<IEnumerable<ComentarioResponseDTO>> GetByProductoAndVendedorAsync(int idProducto, int idUsuarioVendedor);
        Task<bool> UpdateAsync(int idComentario, ComentarioDTO dto);
        Task<ComentarioResponseDTO?> GetByIdAsync(int id);
    }
}
