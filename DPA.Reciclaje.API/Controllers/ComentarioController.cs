using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _comentarioService;
        private readonly IUsuarioRepository _usuarioRepository;
        public ComentarioController(IComentarioService comentarioService, IUsuarioRepository usuarioRepository)
        {
            _comentarioService = comentarioService;
            _usuarioRepository = usuarioRepository;
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

        [HttpGet("byProducto")]
        public async Task<IActionResult> GetByProducto([FromQuery] int idProducto)
        {
            var list = await _comentarioService.GetAllAsync();
            if (list == null) return Ok(Enumerable.Empty<object>());

            var filtered = list.Where(c => c.IdProducto == idProducto).ToList();

            // Fetch comprador user names in a single repository call to avoid concurrent DbContext usage
            var userIds = filtered.Select(c => c.IdUsuarioComprador).Where(id => id.HasValue).Select(id => id!.Value).Distinct().ToList();
            var usersDict = new Dictionary<int, DPA.Reciclaje.CORE.Core.Entities.Usuario?>();
            if (userIds.Any())
            {
                var users = await _usuarioRepository.GetAllUsuarios();
                usersDict = users.Where(u => userIds.Contains(u.IdUsuario)).ToDictionary(u => u.IdUsuario, u => (DPA.Reciclaje.CORE.Core.Entities.Usuario?)u);
            }

            // Map to the desired JSON shape with nested usuarioComprador
            var result = filtered.Select(c => new
            {
                idComentario = c.IdComentario,
                idUsuarioVendedor = c.IdUsuarioVendedor,
                idUsuarioComprador = c.IdUsuarioComprador,
                usuarioComprador = new
                {
                    idUsuario = c.IdUsuarioComprador ?? 0,
                    nombres = c.IdUsuarioComprador.HasValue && usersDict.ContainsKey(c.IdUsuarioComprador.Value) && usersDict[c.IdUsuarioComprador.Value] != null
                        ? string.Join(" ", new[] { usersDict[c.IdUsuarioComprador.Value].Nombres, usersDict[c.IdUsuarioComprador.Value].Apellidos }.Where(s => !string.IsNullOrWhiteSpace(s)))
                        : string.Empty
                },
                idProducto = c.IdProducto,
                texto = c.Texto,
                calificacion = c.Calificacion,
                estado = c.Estado,
                fecha = c.Fecha,
                imagenes = c.Imagenes ?? new List<string>()
            }).ToList();

            return Ok(result);
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
