using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
