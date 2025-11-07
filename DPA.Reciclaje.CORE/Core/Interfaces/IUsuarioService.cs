using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDTO?> SignInAsync(SignInDTO dto);
        Task<int> SignUpAsync(SignUpDTO dto);
    }
}