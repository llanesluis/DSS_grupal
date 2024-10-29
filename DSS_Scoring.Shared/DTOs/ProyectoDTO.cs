using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class ProyectoDTO
{
    public int Id { get; set; }
    
    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string Objetivo { get; set; } = null!;

    [Required (ErrorMessage = "El campo IdFacilitador es requerido y debe ser un Id que corresponda a un usuario con el rol Facilitador")]
    public int IdFacilitador { get; set; }
}
