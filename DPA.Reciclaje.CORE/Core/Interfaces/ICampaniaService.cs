using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICampaniaService
    {
        Task<int> CreateAsync(CampaniaDTO dto);
        Task<IEnumerable<CampaniaResponseDTO>> GetAllAsync();
        Task<CampaniaResponseDTO?> GetByIdAsync(int id);
    }
}