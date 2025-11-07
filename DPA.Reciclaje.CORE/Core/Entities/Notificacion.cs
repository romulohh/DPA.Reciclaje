using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Notificacion
{
    public int IdNotificacion { get; set; }

    public int? IdUsuarioVendedor { get; set; }

    public int? IdUsuarioComprador { get; set; }

    public string? Tipo { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Leído { get; set; }

    public string? Mensaje { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioCompradorNavigation { get; set; }

    public virtual Usuario? IdUsuarioVendedorNavigation { get; set; }
}
