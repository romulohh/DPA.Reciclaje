using System;
using System.Collections.Generic;

namespace DPA.Reciclaje.CORE.Core.Entities;

public partial class Pago
{
    public int IdPago { get; set; }

    public int? IdCarrito { get; set; }

    public int? IdMetodoPago { get; set; }

    public decimal Monto { get; set; }

    public DateTime? Fecha { get; set; }

    public string? NumeroOperacion { get; set; }

    public virtual Carrito? IdCarritoNavigation { get; set; }

    public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }
}
