using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class Chat
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProyecto { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string Mensaje { get; set; } = null!;

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
