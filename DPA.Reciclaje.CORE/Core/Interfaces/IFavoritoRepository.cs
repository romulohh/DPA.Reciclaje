using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IFavoritoRepository
    {
        Task<IEnumerable<Favorito>> GetAllFavoritos();
        Task<Favorito?> GetFavoritoById(int id);
        Task<int> AddFavorito(Favorito favorito);
    }
}
