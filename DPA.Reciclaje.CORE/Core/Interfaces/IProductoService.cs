using DPA.Reciclaje.CORE.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoResponseDTO>> GetAllAsync();
        Task<ProductoResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(ProductoDTO dto);
        Task<IEnumerable<ProductoResponseDTO>> FilterAsync(int? categoriaId, int? departamentoId, int? provinciaId, int? distritoId);

        // NUEVO
        Task AddImagenesAsync(int idProducto, IEnumerable<string> nombresImagenes);

        // NUEVO: actualizar y eliminar producto
        Task<bool> UpdateAsync(int id, ProductoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
