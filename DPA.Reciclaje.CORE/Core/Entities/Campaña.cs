using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Campania
{
    public int IdCampania { get; set; }

    public string Título { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? IdDistrito { get; set; }

    public int? IdUsuario { get; set; }
    public string? Imagen { get; set; }

    public virtual ICollection<CampaniaImg> CampaniaImg { get; set; } = new List<CampaniaImg>();

    public virtual Distrito? IdDistritoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
