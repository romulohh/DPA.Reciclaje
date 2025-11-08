using System;
using System.Text.Json.Serialization;

namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class PagoResponseDTO
    {
        [JsonPropertyName("idPago")]
        public int IdPago { get; set; }

        [JsonPropertyName("carrito")]
        public CarritoResponseDTO? Carrito { get; set; }

        [JsonPropertyName("metodoPago")]
        public MetodoPagoResponseDTO? MetodoPago { get; set; }

        [JsonPropertyName("monto")]
        public decimal Monto { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime? Fecha { get; set; }

        [JsonPropertyName("numeroOperacion")]
        public string NumeroOperacion { get; set; } = string.Empty;
    }
}