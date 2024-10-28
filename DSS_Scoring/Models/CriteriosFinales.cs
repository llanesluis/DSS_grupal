using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class CriteriosFinales
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdCriterio { get; set; }

    public virtual Criterio? IdCriterioNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
