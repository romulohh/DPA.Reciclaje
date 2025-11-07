using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> AddUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<Usuario?> GetUsuarioByEmail(string email);
        Task<Usuario?> GetUsuarioById(int id);
        Task<bool> UpdateUsuario(Usuario usuario);
    }
}