using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class VotacionPeso
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdPesoPropuesto { get; set; }

    public int? IdUsuario { get; set; }

    public bool Voto { get; set; }

    public virtual PesoPropuesto? IdPesoPropuestoNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
