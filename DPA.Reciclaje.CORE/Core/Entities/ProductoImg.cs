using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class ProductoImg
{
    public int IdProductoImg { get; set; }

    public int? IdProducto { get; set; }

    public string? Imagen { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
