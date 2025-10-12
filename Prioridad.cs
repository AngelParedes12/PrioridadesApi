namespace PrioridadesApi.Models;

public enum NivelPrioridad { Baja = 1, Media = 2, Alta = 3 }

public class Prioridad
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public NivelPrioridad Nivel { get; set; } = NivelPrioridad.Media;
    public DateTime? FechaVencimiento { get; set; }
    public bool Completada { get; set; } = false;

    public DateTime CreadaEl { get; set; } = DateTime.UtcNow;
    public DateTime? ActualizadaEl { get; set; }
}
