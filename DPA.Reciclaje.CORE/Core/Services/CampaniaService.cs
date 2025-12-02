using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class CampaniaService : ICampaniaService
    {
        private readonly ICampaniaRepository _campaniaRepository;
        public CampaniaService(ICampaniaRepository campaniaRepository)
        {
            _campaniaRepository = campaniaRepository;
        }
        public async Task<IEnumerable<CampaniaResponseDTO>> GetAllAsync()
        {
            var list = await _campaniaRepository.GetAllCampanias();
            return list.Select(c => MapToDto(c));
        }
        public async Task<CampaniaResponseDTO?> GetByIdAsync(int id)
        {
            var c = await _campaniaRepository.GetCampaniaById(id);
            if (c == null) return null;
            return MapToDto(c);
        }
        public async Task<int> CreateAsync(CampaniaDTO dto)
        {
            var campania = new Campania
            {
                Título = dto.Título,
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                IdDistrito = dto.IdDistrito,
                IdUsuario = dto.IdUsuario
            };
            var id = await _campaniaRepository.AddCampania(campania);
            return id;
        }
        private static CampaniaResponseDTO MapToDto(Campania c)
        {
            return new CampaniaResponseDTO
            {
                IdCampania = c.IdCampania,
                Título = c.Título,
                Descripcion = c.Descripcion,
                FechaInicio = (DateTime)c.FechaInicio,
                FechaFin = (DateTime)c.FechaFin,
                IdDistrito = (int)c.IdDistrito
            };
        }
    }
}
