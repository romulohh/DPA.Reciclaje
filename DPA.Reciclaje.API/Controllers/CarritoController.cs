using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;
        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarritoDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            var id = await _carritoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarritoDTO dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            var ok = await _carritoService.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _carritoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _carritoService.GetByIdAsync(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpGet("byuser/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var list = await _carritoService.GetByUsuarioAsync(usuarioId);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _carritoService.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
