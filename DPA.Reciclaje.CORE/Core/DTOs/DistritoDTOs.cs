namespace DPA.Reciclaje.CORE.Core.DTOs
{
    public class DistritoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
    }

    public class DistritoResponseDTO
    {
        public int IdDistrito { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ProvinciaResponseDTO? Provincia { get; set; }
        public DepartamentoResponseDTO? Departamento { get; set; }
    }
}
