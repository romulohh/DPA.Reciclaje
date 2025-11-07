using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _productoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prod = await _productoService.GetByIdAsync(id);
            if (prod == null) return NotFound();
            return Ok(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var id = await _productoService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el producto.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        // Filtrar por categoría y ubicación (departamento, provincia, distrito)
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] int? categoriaId, [FromQuery] int? departamentoId, [FromQuery] int? provinciaId, [FromQuery] int? distritoId)
        {
            var list = await _productoService.FilterAsync(categoriaId, departamentoId, provinciaId, distritoId);
            return Ok(list);
        }
    }
}
