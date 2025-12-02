using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class FavoritoService : IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository;
        public FavoritoService(IFavoritoRepository favoritoRepository)
        {
            _favoritoRepository = favoritoRepository;
        }

        public async Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync()
        {
            var list = await _favoritoRepository.GetAllFavoritos();
            return list.Select(f => new FavoritoResponseDTO { IdFavorito = f.IdFavorito, IdUsuario = f.IdUsuario, IdProducto = f.IdProducto, Fecha = f.Fecha });
        }

        public async Task<FavoritoResponseDTO?> GetByIdAsync(int id)
        {
            var f = await _favoritoRepository.GetFavoritoById(id);
            if (f == null) return null;
            return new FavoritoResponseDTO { IdFavorito = f.IdFavorito, IdUsuario = f.IdUsuario, IdProducto = f.IdProducto, Fecha = f.Fecha };
        }

        public async Task<int> CreateAsync(FavoritoDTO dto)
        {
            var fav = new Favorito { IdUsuario = dto.IdUsuario, IdProducto = dto.IdProducto };
            var id = await _favoritoRepository.AddFavorito(fav);
            return id;
        }

        public async Task<bool> DeleteByProductoUsuarioAsync(int idProducto, int idUsuario)
        {
            return await _favoritoRepository.DeleteFavoritosByProductoUsuario(idProducto, idUsuario);
        }
    }
}
