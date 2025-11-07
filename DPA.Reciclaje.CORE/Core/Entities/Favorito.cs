using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Favorito
{
    public int IdFavorito { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
