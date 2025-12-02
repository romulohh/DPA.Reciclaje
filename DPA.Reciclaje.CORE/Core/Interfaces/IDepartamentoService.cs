using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IDepartamentoService
    {
        Task<IEnumerable<DepartamentoResponseDTO>> GetAllAsync();
        Task<DepartamentoResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(DepartamentoDTO dto);
    }
}
