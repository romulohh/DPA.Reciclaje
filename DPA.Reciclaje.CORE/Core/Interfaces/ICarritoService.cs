using DPA.Reciclaje.CORE.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICarritoService
    {
        Task<int> CreateAsync(CarritoDTO dto);
        Task<bool> UpdateAsync(int id, CarritoDTO dto);
        Task<IEnumerable<CarritoResponseDTO>> GetAllAsync();
        Task<CarritoResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<CarritoResponseDTO>> GetByUsuarioAsync(int usuarioId);
        Task<bool> DeleteAsync(int id);
    }
}
