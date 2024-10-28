using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class ProyectoUsuario
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProyecto { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
