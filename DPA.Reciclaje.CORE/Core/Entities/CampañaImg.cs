using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class CampaniaImg
{
    public int IdCampaniaImg { get; set; }

    public int? IdCampania { get; set; }

    public byte[]? Imagen { get; set; }

    public virtual Campania? IdCampaniaNavigation { get; set; }
}
