using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class ComentarioImg
{
    public int IdComentarioImg { get; set; }

    public int? IdComentario { get; set; }

    public string? Imagen { get; set; }

    public virtual Comentario? IdComentarioNavigation { get; set; }
}
