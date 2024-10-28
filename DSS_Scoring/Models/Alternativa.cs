using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class Alternativa
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<AlternativasFinales> AlternativasFinales { get; set; } = new List<AlternativasFinales>();

    public virtual ICollection<CategorizacionAlternativa> CategorizacionAlternativas { get; set; } = new List<CategorizacionAlternativa>();

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<VotacionAlternativa> VotacionAlternativas { get; set; } = new List<VotacionAlternativa>();
}
