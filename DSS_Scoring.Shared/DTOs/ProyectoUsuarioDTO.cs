using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class ProyectoUsuarioDTO
{
  public int Id { get; set; }

  [Required]
  public int IdProyecto { get; set; }

  [Required]
  public int IdUsuario { get; set; }
}
