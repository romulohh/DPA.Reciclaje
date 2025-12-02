using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task<int> CreateAsync(ComentarioDTO dto)
        {
            var comentario = new Comentario
            {
                IdProducto = dto.IdProducto,
                IdUsuarioComprador = dto.IdUsuarioComprador,
                IdUsuarioVendedor = dto.IdUsuarioVendedor,
                Texto = dto.Texto,
                Calificacion = dto.Calificacion,
                Estado = dto.Estado
            };

            var id = await _comentarioRepository.AddComentario(comentario);

            if (dto.Imagenes != null && dto.Imagenes.Any())
            {
                foreach (var img in dto.Imagenes)
                {
                    var cimg = new ComentarioImg { IdComentario = id, Imagen = img };
                    await _comentarioRepository.AddComentarioImagen(cimg);
                }
            }

            return id;
        }

        public async Task<IEnumerable<ComentarioResponseDTO>> GetAllAsync()
        {
            var list = await _comentarioRepository.GetAllComentarios();
            return list.Select(c => new ComentarioResponseDTO
            {
                IdComentario = c.IdComentario,
                IdProducto = c.IdProducto,
                IdUsuarioComprador = c.IdUsuarioComprador,
                IdUsuarioVendedor = c.IdUsuarioVendedor,
                Texto = c.Texto,
                Calificacion = c.Calificacion,
                Estado = c.Estado,
                Fecha = c.Fecha,
                Imagenes = c.ComentarioImg?.Select(ci => ci.Imagen).ToList()
            });
        }

        public async Task<IEnumerable<ComentarioResponseDTO>> GetByProductoAndCompradorAsync(int idProducto, int idUsuarioComprador)
        {
            var list = await _comentarioRepository.GetComentariosByProductoAndComprador(idProducto, idUsuarioComprador);
            return list.Select(c => new ComentarioResponseDTO
            {
                IdComentario = c.IdComentario,
                IdProducto = c.IdProducto,
                IdUsuarioComprador = c.IdUsuarioComprador,
                IdUsuarioVendedor = c.IdUsuarioVendedor,
                Texto = c.Texto,
                Calificacion = c.Calificacion,
                Estado = c.Estado,
                Fecha = c.Fecha,
                Imagenes = c.ComentarioImg?.Select(ci => ci.Imagen).ToList()
            });
        }

        public async Task<IEnumerable<ComentarioResponseDTO>> GetByProductoAndVendedorAsync(int idProducto, int idUsuarioVendedor)
        {
            var list = await _comentarioRepository.GetComentariosByProductoAndVendedor(idProducto, idUsuarioVendedor);
            return list.Select(c => new ComentarioResponseDTO
            {
                IdComentario = c.IdComentario,
                IdProducto = c.IdProducto,
                IdUsuarioComprador = c.IdUsuarioComprador,
                IdUsuarioVendedor = c.IdUsuarioVendedor,
                Texto = c.Texto,
                Calificacion = c.Calificacion,
                Estado = c.Estado,
                Fecha = c.Fecha,
                Imagenes = c.ComentarioImg?.Select(ci => ci.Imagen).ToList()
            });
        }

        public async Task<bool> UpdateAsync(int idComentario, ComentarioDTO dto)
        {
            var comentario = new Comentario
            {
                IdComentario = idComentario,
                Texto = dto.Texto,
                Calificacion = dto.Calificacion,
                Estado = dto.Estado
            };

            return await _comentarioRepository.UpdateComentario(comentario);
        }

        public async Task<ComentarioResponseDTO?> GetByIdAsync(int id)
        {
            var c = await _comentarioRepository.GetComentarioById(id);
            if (c == null) return null;
            return new ComentarioResponseDTO
            {
                IdComentario = c.IdComentario,
                IdProducto = c.IdProducto,
                IdUsuarioComprador = c.IdUsuarioComprador,
                IdUsuarioVendedor = c.IdUsuarioVendedor,
                Texto = c.Texto,
                Calificacion = c.Calificacion,
                Estado = c.Estado,
                Fecha = c.Fecha,
                Imagenes = c.ComentarioImg?.Select(ci => ci.Imagen).ToList()
            };
        }
    }
}
