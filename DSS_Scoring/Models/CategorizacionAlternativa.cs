using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class CategorizacionAlternativa
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdAlternativa { get; set; }

    public bool? Modificado { get; set; }

    public virtual Alternativa? IdAlternativaNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
