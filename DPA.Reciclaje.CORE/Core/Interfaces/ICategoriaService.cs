using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponseDTO>> GetAllAsync();
        Task<CategoriaResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoriaDTO dto);
    }
}
