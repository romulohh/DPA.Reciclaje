using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdDepartamento { get; set; }

    public virtual ICollection<Distrito> Distrito { get; set; } = new List<Distrito>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
