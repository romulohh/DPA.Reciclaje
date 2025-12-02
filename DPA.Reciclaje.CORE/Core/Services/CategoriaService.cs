using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaResponseDTO>> GetAllAsync()
        {
            var list = await _categoriaRepository.GetAllCategorias();
            return list.Select(c => new CategoriaResponseDTO { IdCategoria = c.IdCategoria, Nombre = c.Nombre });
        }

        public async Task<CategoriaResponseDTO?> GetByIdAsync(int id)
        {
            var c = await _categoriaRepository.GetCategoriaById(id);
            if (c == null) return null;
            return new CategoriaResponseDTO { IdCategoria = c.IdCategoria, Nombre = c.Nombre };
        }

        public async Task<int> CreateAsync(CategoriaDTO dto)
        {
            var categoria = new Categoria { Nombre = dto.Nombre };
            var id = await _categoriaRepository.AddCategoria(categoria);
            return id;
        }
    }
}
