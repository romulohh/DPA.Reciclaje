using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdPais { get; set; }

    public virtual ICollection<Distrito> Distrito { get; set; } = new List<Distrito>();

    public virtual Pais? IdPaisNavigation { get; set; }

    public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
}
