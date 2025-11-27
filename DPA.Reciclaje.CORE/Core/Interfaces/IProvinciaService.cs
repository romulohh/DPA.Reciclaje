using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IProvinciaService
    {
        Task<IEnumerable<ProvinciaResponseDTO>> GetAllAsync();
        Task<ProvinciaResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(ProvinciaDTO dto);
        Task<IEnumerable<ProvinciaResponseDTO>> GetByDepartamentoAsync(int departamentoId);
    }
}
