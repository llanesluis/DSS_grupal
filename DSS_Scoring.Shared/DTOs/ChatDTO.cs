using System.ComponentModel.DataAnnotations;

namespace DSS_Scoring.Shared.DTOs;

public class ChatDTO
{
    public int Id { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdProyecto { get; set; }

    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    public TimeOnly Hora { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    [Required]
    public string Mensaje { get; set; } = null!;

    public UsuarioDTO? Usuario { get; set; } = null!;
}
