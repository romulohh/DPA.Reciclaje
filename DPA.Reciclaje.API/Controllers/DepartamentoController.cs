using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;
        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _departamentoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var departamento = await _departamentoService.GetByIdAsync(id);
            if (departamento == null) return NotFound();
            return Ok(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartamentoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var id = await _departamentoService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el Departamento.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}
