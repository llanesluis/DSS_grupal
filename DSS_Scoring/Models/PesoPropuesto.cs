using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class PesoPropuesto
{
    public int Id { get; set; }

    public int? IdCriterio { get; set; }

    public decimal Valor { get; set; }

    public virtual Criterio? IdCriterioNavigation { get; set; }

    public virtual ICollection<VotacionPeso> VotacionPesos { get; set; } = new List<VotacionPeso>();
}
