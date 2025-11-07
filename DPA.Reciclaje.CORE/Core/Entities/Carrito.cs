using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<CarritoProducto> CarritoProducto { get; set; } = new List<CarritoProducto>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
