using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Marca { get; set; }

    public int? IdCategoria { get; set; }

    public string? Motivo { get; set; }

    public string? Estado { get; set; }

    public string? Disponible { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? FechaPublicación { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<CarritoProducto> CarritoProducto { get; set; } = new List<CarritoProducto>();

    public virtual ICollection<Chat> Chat { get; set; } = new List<Chat>();

    public virtual ICollection<Comentario> Comentario { get; set; } = new List<Comentario>();

    public virtual ICollection<Favorito> Favorito { get; set; } = new List<Favorito>();

    public virtual Categoría? IdCategoriaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Interaccion> Interaccion { get; set; } = new List<Interaccion>();

    public virtual ICollection<Notificacion> Notificacion { get; set; } = new List<Notificacion>();

    public virtual ICollection<ProductoImg> ProductoImg { get; set; } = new List<ProductoImg>();
}
