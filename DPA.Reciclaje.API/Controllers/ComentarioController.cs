using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _comentarioService;
        public ComentarioController(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComentarioDTO dto)
        {
            if (dto == null || dto.IdProducto == null)
                return BadRequest("IdProducto es obligatorio.");

            var id = await _comentarioService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el Comentario.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _comentarioService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("byProductoComprador")]
        public async Task<IActionResult> GetByProductoComprador([FromQuery] int idProducto, [FromQuery] int idUsuarioComprador)
        {
            var list = await _comentarioService.GetByProductoAndCompradorAsync(idProducto, idUsuarioComprador);
            return Ok(list);
        }

        [HttpGet("byProductoVendedor")]
        public async Task<IActionResult> GetByProductoVendedor([FromQuery] int idProducto, [FromQuery] int idUsuarioVendedor)
        {
            var list = await _comentarioService.GetByProductoAndVendedorAsync(idProducto, idUsuarioVendedor);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fav = await _comentarioService.GetByIdAsync(id);
            if (fav == null) return NotFound();
            return Ok(fav);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComentarioDTO dto)
        {
            if (dto == null) return BadRequest();

            var updated = await _comentarioService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }
    }
}
