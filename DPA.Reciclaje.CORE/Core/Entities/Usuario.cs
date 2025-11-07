using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string? Direccion { get; set; }

    public int? IdDistrito { get; set; }

    public string Puntuacion { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? Situacion { get; set; }

    public string? Estado { get; set; }

    public string? UltAccionVenta { get; set; }

    public string? UltAccionCompra { get; set; }

    public byte[]? Imagen { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Campaña> Campaña { get; set; } = new List<Campaña>();

    public virtual ICollection<Carrito> Carrito { get; set; } = new List<Carrito>();

    public virtual ICollection<Chat> ChatIdUsuarioCompradorNavigation { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatIdUsuarioVendedorNavigation { get; set; } = new List<Chat>();

    public virtual ICollection<Comentario> ComentarioIdUsuarioCompradorNavigation { get; set; } = new List<Comentario>();

    public virtual ICollection<Comentario> ComentarioIdUsuarioVendedorNavigation { get; set; } = new List<Comentario>();

    public virtual ICollection<Favorito> Favorito { get; set; } = new List<Favorito>();

    public virtual Distrito? IdDistritoNavigation { get; set; }

    public virtual ICollection<Interaccion> Interaccion { get; set; } = new List<Interaccion>();

    public virtual ICollection<Notificacion> NotificacionIdUsuarioCompradorNavigation { get; set; } = new List<Notificacion>();

    public virtual ICollection<Notificacion> NotificacionIdUsuarioVendedorNavigation { get; set; } = new List<Notificacion>();

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
