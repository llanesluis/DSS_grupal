using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class LluviaIdeaDTO
{
  public int Id { get; set; }

  [Required]
  public int IdProyecto { get; set; }

  [Required]
  public int IdUsuario { get; set; }

  [Required]
  public string Idea { get; set; } = null!;

  [Required]
  [RegularExpression("^(alternativas|criterios)$", ErrorMessage = "La etapa debe ser 'alternativas' o 'criterios'.")]
  public string Etapa { get; set; } = null!;
}
