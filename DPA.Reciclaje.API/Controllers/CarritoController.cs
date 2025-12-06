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

        [HttpPost("item")]
        public async Task<IActionResult> AddItem([FromBody] AddCarritoItemDTO dto)
        {
            if (dto == null || dto.IdCarrito <= 0 || dto.IdProducto <= 0 || dto.Precio <= 0)
                return BadRequest("idCarrito, idProducto y precio son obligatorios.");

            var id = await _carritoService.AddItemAsync(dto.IdCarrito, dto.IdProducto, dto.Precio);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdCarrito }, dto);
        }

        [HttpGet("exists-item")]
        public async Task<IActionResult> ExistsItem([FromQuery] int idCarrito, [FromQuery] int idProducto)
        {
            if (idCarrito <= 0 || idProducto <= 0) return BadRequest("idCarrito e idProducto son obligatorios.");

            var exists = await _carritoService.ExistsProductInCarritoAsync(idCarrito, idProducto);
            return Ok(exists);
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

        [HttpGet("active/{usuarioId}")]
        public async Task<IActionResult> GetActiveByUsuario(int usuarioId)
        {
            var list = await _carritoService.GetByUsuarioAsync(usuarioId);
            var active = list?.Where(c => string.Equals(c.Estado, "A", StringComparison.OrdinalIgnoreCase));
            return Ok(active);
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
