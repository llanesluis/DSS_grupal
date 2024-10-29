using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class UsuarioDTO
{
  public int Id { get; set; }

  [Required]
  public string Nombre { get; set; } = null!;

  [Required]
  [EmailAddress(ErrorMessage = "Por favor, ingresa un correo electrónico válido. De preferencia usar el correo de la universidad")]
  public string Email { get; set; } = null!;

  [Required (ErrorMessage = "El campo Password es requerido, de preferencia usar la matrícula de la universidad como contraseña")]
  public string Password { get; set; } = null!;

  [Required]
  [RegularExpression("^(admin|facilitador|decisor)$", ErrorMessage = "El rol debe ser 'admin', 'facilitador' o 'decisor'.")]
  public string Rol { get; set; } = null!;
}
