namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class PagoDTO
    {
        public int? IdCarrito { get; set; }
        public int? IdMetodoPago { get; set; }
        public decimal Monto { get; set; }
        public string NumeroOperacion { get; set; } = string.Empty;
    }
}