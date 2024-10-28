using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class AlternativasFinales
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdAlternativa { get; set; }

    public virtual Alternativa? IdAlternativaNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
