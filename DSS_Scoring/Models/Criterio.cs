using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class Criterio
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<CategorizacionCriterio> CategorizacionCriterios { get; set; } = new List<CategorizacionCriterio>();

    public virtual ICollection<CriteriosFinales> CriteriosFinales { get; set; } = new List<CriteriosFinales>();

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<PesoFinal> PesoFinals { get; set; } = new List<PesoFinal>();

    public virtual ICollection<PesoPropuesto> PesoPropuestos { get; set; } = new List<PesoPropuesto>();

    public virtual ICollection<VotacionCriterio> VotacionCriterios { get; set; } = new List<VotacionCriterio>();
}
