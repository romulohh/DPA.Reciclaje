using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public int? IdUsuarioVendedor { get; set; }

    public int? IdUsuarioComprador { get; set; }

    public int? IdProducto { get; set; }

    public string? Texto { get; set; }

    public string? Calificacion { get; set; }

    public string? Estado { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual ICollection<ComentarioImg> ComentarioImg { get; set; } = new List<ComentarioImg>();

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioCompradorNavigation { get; set; }

    public virtual Usuario? IdUsuarioVendedorNavigation { get; set; }
}
