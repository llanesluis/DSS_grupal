using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Objetivo { get; set; } = null!;

    public int? IdFacilitador { get; set; }

    public virtual ICollection<Alternativa> Alternativas { get; set; } = new List<Alternativa>();

    public virtual ICollection<AlternativasFinales> AlternativasFinales { get; set; } = new List<AlternativasFinales>();

    public virtual ICollection<CategorizacionAlternativa> CategorizacionAlternativas { get; set; } = new List<CategorizacionAlternativa>();

    public virtual ICollection<CategorizacionCriterio> CategorizacionCriterios { get; set; } = new List<CategorizacionCriterio>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Criterio> Criterios { get; set; } = new List<Criterio>();

    public virtual ICollection<CriteriosFinales> CriteriosFinales { get; set; } = new List<CriteriosFinales>();

    public virtual Usuario? IdFacilitadorNavigation { get; set; }

    public virtual ICollection<LluviaIdea> LluviaIdeas { get; set; } = new List<LluviaIdea>();

    public virtual ICollection<PesoFinal> PesosFinales { get; set; } = new List<PesoFinal>();

    public virtual ICollection<ProyectoUsuario> ProyectoUsuarios { get; set; } = new List<ProyectoUsuario>();

    public virtual ICollection<VotacionAlternativa> VotacionAlternativas { get; set; } = new List<VotacionAlternativa>();

    public virtual ICollection<VotacionCriterio> VotacionCriterios { get; set; } = new List<VotacionCriterio>();

    public virtual ICollection<VotacionPeso> VotacionPesos { get; set; } = new List<VotacionPeso>();
}
