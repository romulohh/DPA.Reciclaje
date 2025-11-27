namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class ProvinciaDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public int? IdDepartamento { get; set; }
    }

    public class ProvinciaResponseDTO
    {
        public int IdProvincia { get; set; }
        public int? IdDepartamento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DepartamentoResponseDTO? Departamento { get; set; }
    }
}
