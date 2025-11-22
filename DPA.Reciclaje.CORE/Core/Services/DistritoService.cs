using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class DistritoService : IDistritoService
    {
        private readonly IDistritoRepository _distritoRepository;
        public DistritoService(IDistritoRepository distritoRepository)
        {
            _distritoRepository = distritoRepository;
        }

        public async Task<IEnumerable<DistritoResponseDTO>> GetAllAsync()
        {
            var list = await _distritoRepository.GetAllDistritos();
            return list.Select(d => new DistritoResponseDTO { IdDistrito = d.IdDistrito, IdDepartamento = d.IdDepartamento, IdProvincia = d.IdProvincia, Nombre = d.Nombre });
        }

        public async Task<DistritoResponseDTO?> GetByIdAsync(int id)
        {
            var d = await _distritoRepository.GetDistritoById(id);
            if (d == null) return null;
            return new DistritoResponseDTO { IdDistrito = d.IdDistrito, IdDepartamento = d.IdDepartamento, IdProvincia = d.IdProvincia, Nombre = d.Nombre };
        }

        public async Task<int> CreateAsync(DistritoDTO dto)
        {
            var distrito = new Distrito { Nombre = dto.Nombre, IdDepartamento = dto.IdDepartamento, IdProvincia = dto.IdProvincia };
            var id = await _distritoRepository.AddDistrito(distrito);
            return id;
        }

        public async Task<IEnumerable<DistritoResponseDTO>> GetByProvinciaAsync(int provinciaId)
        {
            var list = await _distritoRepository.GetDistritosByProvincia(provinciaId);
            return list.Select(d => new DistritoResponseDTO { IdDistrito = d.IdDistrito, IdDepartamento = d.IdDepartamento, IdProvincia = d.IdProvincia, Nombre = d.Nombre });
        }

        public async Task<IEnumerable<DistritoResponseDTO>> GetByDepartamentoAsync(int departamentoId)
        {
            var list = await _distritoRepository.GetDistritosByDepartamento(departamentoId);
            return list.Select(d => new DistritoResponseDTO { IdDistrito = d.IdDistrito, IdDepartamento = d.IdDepartamento, IdProvincia = d.IdProvincia, Nombre = d.Nombre });
        }
    }
}
