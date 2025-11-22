using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _provinciaService;
        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _provinciaService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var provincia = await _provinciaService.GetByIdAsync(id);
            if (provincia == null) return NotFound();
            return Ok(provincia);
        }

        [HttpGet("byDepartamento/{departamentoId}")]
        public async Task<IActionResult> GetByDepartamento(int departamentoId)
        {
            var list = await _provinciaService.GetByDepartamentoAsync(departamentoId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProvinciaDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var id = await _provinciaService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear la Provincia.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}
