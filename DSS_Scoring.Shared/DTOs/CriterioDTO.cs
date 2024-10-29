using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class CriterioDTO
{
    public int Id { get; set; }

    [Required]
    public int IdProyecto { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;
}
