using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICampaniaService
    {
        Task<int> CreateAsync(CampaniaDTO dto);
        Task<IEnumerable<CampaniaResponseDTO>> GetAllAsync();
        Task<CampaniaResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<CampaniaResponseDTO>> GetVigentesAsync();
        Task<bool> UpdateAsync(int id, CampaniaDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}