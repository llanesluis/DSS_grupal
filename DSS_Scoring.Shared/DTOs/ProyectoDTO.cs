using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class ProyectoDTO
{
    public int Id { get; set; }
    
    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string Objetivo { get; set; } = null!;

    [Required]
    public int IdFacilitador { get; set; }
}
