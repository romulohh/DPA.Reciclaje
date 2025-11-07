using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class CarritoProducto
{
    public int IdCarritoItem { get; set; }

    public int? IdCarrito { get; set; }

    public int? IdProducto { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Carrito? IdCarritoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
