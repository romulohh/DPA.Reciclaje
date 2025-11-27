using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IFavoritoService
    {
        Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync();
        Task<FavoritoResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(FavoritoDTO dto);
    }
}
