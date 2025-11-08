using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoría>> GetAllCategorias();
        Task<Categoría?> GetCategoriaById(int id);
        Task<int> AddCategoria(Categoría categoria);
    }
}
