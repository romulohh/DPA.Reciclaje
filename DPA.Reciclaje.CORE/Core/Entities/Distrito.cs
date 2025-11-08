using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Distrito
{
    public int IdDistrito { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdDepartamento { get; set; }

    public int? IdProvincia { get; set; }

    public virtual ICollection<Campania> Campania { get; set; } = new List<Campania>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Provincia? IdProvinciaNavigation { get; set; }

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
