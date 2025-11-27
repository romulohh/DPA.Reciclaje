using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IDistritoService
    {
        Task<IEnumerable<DistritoResponseDTO>> GetAllAsync();
        Task<DistritoResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(DistritoDTO dto);
        Task<IEnumerable<DistritoResponseDTO>> GetByProvinciaAsync(int provinciaId);
        Task<IEnumerable<DistritoResponseDTO>> GetByDepartamentoAsync(int departamentoId);
    }
}
