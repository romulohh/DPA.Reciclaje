using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Campaña
{
    public int IdCampaña { get; set; }

    public string Título { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? IdDistrito { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<CampañaImg> CampañaImg { get; set; } = new List<CampañaImg>();

    public virtual Distrito? IdDistritoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
