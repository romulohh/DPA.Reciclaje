namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class DepartamentoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public int? IdPais { get; set; }
    }

    public class DepartamentoResponseDTO
    {
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
