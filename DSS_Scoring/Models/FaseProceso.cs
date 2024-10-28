using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class FaseProceso
{
    public int Id { get; set; }

    public int? IdFacilitador { get; set; }

    public string Etapa { get; set; } = null!;

    public bool? Activa { get; set; }

    public virtual Usuario? IdFacilitadorNavigation { get; set; }
}
