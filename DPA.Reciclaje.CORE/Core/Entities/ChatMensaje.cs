using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class ChatMensaje
{
    public int IdChatMensaje { get; set; }

    public int? IdChat { get; set; }

    public string Texto { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public bool? Leído { get; set; }

    public virtual Chat? IdChatNavigation { get; set; }
}
