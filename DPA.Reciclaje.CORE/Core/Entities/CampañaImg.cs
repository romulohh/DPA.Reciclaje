using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class CampañaImg
{
    public int IdCampañaImg { get; set; }

    public int? IdCampaña { get; set; }

    public byte[]? Imagen { get; set; }

    public virtual Campaña? IdCampañaNavigation { get; set; }
}
