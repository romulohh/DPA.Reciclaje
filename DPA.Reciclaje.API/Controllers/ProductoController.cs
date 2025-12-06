using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

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

            return CreatedAtAction(nameof(GetById), new { id }, new { idProducto = id });
        }

        // NUEVO: actualizar producto
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var existente = await _productoService.GetByIdAsync(id);
            if (existente == null)
                return NotFound("El producto no existe.");

            await _productoService.UpdateAsync(id, dto);
            return NoContent(); // o Ok() si prefieres
        }

        // NUEVO: eliminar producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _productoService.DeleteAsync(id);
            if (!ok)
                return NotFound("El producto no existe.");

            return NoContent();
        }

        // NUEVO: Subir imágenes para un producto
        [HttpPost("{id}/imagenes")]
        public async Task<IActionResult> UploadImages(int id, [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("Debe adjuntar al menos una imagen.");

            if (files.Count > 3)
                return BadRequest("No puede adjuntar más de 3 imágenes.");

            var prod = await _productoService.GetByIdAsync(id);
            if (prod == null)
                return NotFound("El producto no existe.");

            // currentDir: normalmente ...\Trabajo grupal\repo\DPA.Reciclaje.API
            var currentDir = Directory.GetCurrentDirectory();

            // 1 nivel arriba: ...\Trabajo grupal\repo
            var repoRoot = Directory.GetParent(currentDir)!.FullName;

            // 2 niveles arriba: ...\Trabajo grupal
            var trabajosRoot = Directory.GetParent(repoRoot)!.FullName;

            // AHORA sí armo la ruta completa del front:
            // Trabajo grupal\repo fe\DPA_Reciclaje_FE\src\assets\images\producto
            var imagesFolder = Path.Combine(
                trabajosRoot,
                "repo fe",
                "DPA_Reciclaje_FE",
                "src",
                "assets",
                "images",
                "producto"
            );

            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            var nombres = new List<string>();
            var correlativo = 1;

            foreach (var file in files.Where(f => f.Length > 0))
            {
                if (correlativo > 3) break; // seguridad adicional

                var extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrWhiteSpace(extension))
                {
                    extension = ".jpg";
                }

                var fileName = $"{id}-{correlativo}{extension}";
                var fullPath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                nombres.Add(fileName);
                correlativo++;
            }

            if (!nombres.Any())
                return BadRequest("No se pudo guardar ninguna imagen válida.");

            await _productoService.AddImagenesAsync(id, nombres);

            return Ok(new { message = "Imágenes guardadas correctamente.", imagenes = nombres });
        }



        // Filtrar por Categoria y ubicación (departamento, provincia, distrito)
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] int? categoriaId, [FromQuery] int? departamentoId, [FromQuery] int? provinciaId, [FromQuery] int? distritoId)
        {
            var list = await _productoService.FilterAsync(categoriaId, departamentoId, provinciaId, distritoId);
            return Ok(list);
        }
    }
}
