using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Chat
{
    public int IdChat { get; set; }

    public int? IdUsuarioVendedor { get; set; }

    public int? IdUsuarioComprador { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? FechaInicio { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<ChatMensaje> ChatMensaje { get; set; } = new List<ChatMensaje>();

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioCompradorNavigation { get; set; }

    public virtual Usuario? IdUsuarioVendedorNavigation { get; set; }
}
