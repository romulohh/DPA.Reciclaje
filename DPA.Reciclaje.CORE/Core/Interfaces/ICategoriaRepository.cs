using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllCategorias();
        Task<Categoria?> GetCategoriaById(int id);
        Task<int> AddCategoria(Categoria categoria);
    }
}
