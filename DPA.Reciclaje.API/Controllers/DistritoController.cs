using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistritoController : ControllerBase
    {
        private readonly IDistritoService _distritoService;
        public DistritoController(IDistritoService distritoService)
        {
            _distritoService = distritoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _distritoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var distrito = await _distritoService.GetByIdAsync(id);
            if (distrito == null) return NotFound();
            return Ok(distrito);
        }

        [HttpGet("byProvincia/{provinciaId}")]
        public async Task<IActionResult> GetByProvincia(int provinciaId)
        {
            var list = await _distritoService.GetByProvinciaAsync(provinciaId);
            return Ok(list);
        }

        [HttpGet("byDepartamento/{departamentoId}")]
        public async Task<IActionResult> GetByDepartamento(int departamentoId)
        {
            var list = await _distritoService.GetByDepartamentoAsync(departamentoId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DistritoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var id = await _distritoService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el Distrito.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}
