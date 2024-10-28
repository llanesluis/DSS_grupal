using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class PesoFinal
{
    public int Id { get; set; }

    public int? IdCriterio { get; set; }

    public int? IdProyecto { get; set; }

    public decimal Peso { get; set; }

    public virtual Criterio? IdCriterioNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
