using System;
using System.Collections.Generic;

namespace DSS_Scoring.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<FaseProceso> FaseProcesos { get; set; } = new List<FaseProceso>();

    public virtual ICollection<LluviaIdea> LluviaIdeas { get; set; } = new List<LluviaIdea>();

    public virtual ICollection<ProyectoUsuario> ProyectoUsuarios { get; set; } = new List<ProyectoUsuario>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<VotacionAlternativa> VotacionAlternativas { get; set; } = new List<VotacionAlternativa>();

    public virtual ICollection<VotacionCriterio> VotacionCriterios { get; set; } = new List<VotacionCriterio>();

    public virtual ICollection<VotacionPeso> VotacionPesos { get; set; } = new List<VotacionPeso>();
}
