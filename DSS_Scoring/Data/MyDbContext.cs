using DSS_Scoring.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Scoring.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options): base(options){}
    public virtual DbSet<Alternativa> Alternativas { get; set; }
    public virtual DbSet<AlternativasFinales> AlternativasFinales { get; set; }
    public virtual DbSet<CategorizacionAlternativa> CategorizacionAlternativas { get; set; }
    public virtual DbSet<CategorizacionCriterio> CategorizacionCriterios { get; set; }
    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<Criterio> Criterios { get; set; }
    public virtual DbSet<CriteriosFinales> CriteriosFinales { get; set; }
    public virtual DbSet<FaseProceso> FaseProcesos { get; set; }
    public virtual DbSet<LluviaIdea> LluviaIdeas { get; set; }
    public virtual DbSet<PesoFinal> PesosFinales { get; set; }
    public virtual DbSet<PesoPropuesto> PesosPropuestos { get; set; }
    public virtual DbSet<Proyecto> Proyectos { get; set; }
    public virtual DbSet<ProyectoUsuario> ProyectoUsuarios { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<VotacionAlternativa> VotacionAlternativas { get; set; }
    public virtual DbSet<VotacionCriterio> VotacionCriterios { get; set; }
    public virtual DbSet<VotacionPeso> VotacionPesos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alternativa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("alternativa_pkey");

            entity.ToTable("alternativa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Alternativas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("alternativa_id_proyecto_fkey");
        });

        modelBuilder.Entity<AlternativasFinales>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("alternativas_finales_pkey");

            entity.ToTable("alternativas_finales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAlternativa).HasColumnName("id_alternativa");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

            entity.HasOne(d => d.IdAlternativaNavigation).WithMany(p => p.AlternativasFinales)
                .HasForeignKey(d => d.IdAlternativa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("alternativas_finales_id_alternativa_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.AlternativasFinales)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("alternativas_finales_id_proyecto_fkey");
        });

        modelBuilder.Entity<CategorizacionAlternativa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categorizacion_alternativas_pkey");

            entity.ToTable("categorizacion_alternativas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAlternativa).HasColumnName("id_alternativa");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Modificado)
                .HasDefaultValue(false)
                .HasColumnName("modificado");

            entity.HasOne(d => d.IdAlternativaNavigation).WithMany(p => p.CategorizacionAlternativas)
                .HasForeignKey(d => d.IdAlternativa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("categorizacion_alternativas_id_alternativa_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.CategorizacionAlternativas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("categorizacion_alternativas_id_proyecto_fkey");
        });

        modelBuilder.Entity<CategorizacionCriterio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categorizacion_criterios_pkey");

            entity.ToTable("categorizacion_criterios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCriterio).HasColumnName("id_criterio");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Modificado)
                .HasDefaultValue(false)
                .HasColumnName("modificado");

            entity.HasOne(d => d.IdCriterioNavigation).WithMany(p => p.CategorizacionCriterios)
                .HasForeignKey(d => d.IdCriterio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("categorizacion_criterios_id_criterio_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.CategorizacionCriterios)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("categorizacion_criterios_id_proyecto_fkey");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chat_pkey");

            entity.ToTable("chat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Chats)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("chat_id_proyecto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Chats)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("chat_id_usuario_fkey");
        });

        modelBuilder.Entity<Criterio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("criterio_pkey");

            entity.ToTable("criterio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Criterios)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("criterio_id_proyecto_fkey");
        });

        modelBuilder.Entity<CriteriosFinales>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("criterios_finales_pkey");

            entity.ToTable("criterios_finales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCriterio).HasColumnName("id_criterio");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

            entity.HasOne(d => d.IdCriterioNavigation).WithMany(p => p.CriteriosFinales)
                .HasForeignKey(d => d.IdCriterio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("criterios_finales_id_criterio_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.CriteriosFinales)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("criterios_finales_id_proyecto_fkey");
        });

        modelBuilder.Entity<FaseProceso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fase_proceso_pkey");

            entity.ToTable("fase_proceso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activa)
                .HasDefaultValue(true)
                .HasColumnName("activa");
            entity.Property(e => e.Etapa)
                .HasMaxLength(50)
                .HasColumnName("etapa");
            entity.Property(e => e.IdFacilitador).HasColumnName("id_facilitador");

            entity.HasOne(d => d.IdFacilitadorNavigation).WithMany(p => p.FaseProcesos)
                .HasForeignKey(d => d.IdFacilitador)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fase_proceso_id_facilitador_fkey");
        });

        modelBuilder.Entity<LluviaIdea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lluvia_ideas_pkey");

            entity.ToTable("lluvia_ideas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Etapa)
                .HasMaxLength(20)
                .HasColumnName("etapa");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Idea).HasColumnName("idea");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.LluviaIdeas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lluvia_ideas_id_proyecto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LluviaIdeas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("lluvia_ideas_id_usuario_fkey");
        });

        modelBuilder.Entity<PesoFinal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("peso_final_pkey");

            entity.ToTable("peso_final");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCriterio).HasColumnName("id_criterio");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Peso).HasColumnName("peso");

            entity.HasOne(d => d.IdCriterioNavigation).WithMany(p => p.PesosFinales)
                .HasForeignKey(d => d.IdCriterio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("peso_final_id_criterio_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.PesosFinales)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("peso_final_id_proyecto_fkey");
        });

        modelBuilder.Entity<PesoPropuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("peso_propuesto_pkey");

            entity.ToTable("peso_propuesto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCriterio).HasColumnName("id_criterio");
            entity.Property(e => e.Valor).HasColumnName("valor");

            entity.HasOne(d => d.IdCriterioNavigation).WithMany(p => p.PesosPropuestos)
                .HasForeignKey(d => d.IdCriterio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("peso_propuesto_id_criterio_fkey");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("proyecto_pkey");

            entity.ToTable("proyecto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdFacilitador).HasColumnName("id_facilitador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Objetivo).HasColumnName("objetivo");

            entity.HasOne(d => d.IdFacilitadorNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdFacilitador)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("proyecto_id_facilitador_fkey");
        });

        modelBuilder.Entity<ProyectoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("proyecto_usuario_pkey");

            entity.ToTable("proyecto_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProyectoUsuarios)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("proyecto_usuario_id_proyecto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ProyectoUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("proyecto_usuario_id_usuario_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<VotacionAlternativa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("votacion_alternativa_pkey");

            entity.ToTable("votacion_alternativa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAlternativa).HasColumnName("id_alternativa");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Voto).HasColumnName("voto");

            entity.HasOne(d => d.IdAlternativaNavigation).WithMany(p => p.VotacionAlternativas)
                .HasForeignKey(d => d.IdAlternativa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votacion_alternativa_id_alternativa_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.VotacionAlternativas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votacion_alternativa_id_proyecto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.VotacionAlternativas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("votacion_alternativa_id_usuario_fkey");
        });

        modelBuilder.Entity<VotacionCriterio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("votacion_criterio_pkey");

            entity.ToTable("votacion_criterio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCriterio).HasColumnName("id_criterio");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Voto).HasColumnName("voto");

            entity.HasOne(d => d.IdCriterioNavigation).WithMany(p => p.VotacionCriterios)
                .HasForeignKey(d => d.IdCriterio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votacion_criterio_id_criterio_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.VotacionCriterios)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votacion_criterio_id_proyecto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.VotacionCriterios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("votacion_criterio_id_usuario_fkey");
        });

        modelBuilder.Entity<VotacionPeso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("votacion_peso_pkey");

            entity.ToTable("votacion_peso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPesoPropuesto).HasColumnName("id_peso_propuesto");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Voto).HasColumnName("voto");

            entity.HasOne(d => d.IdPesoPropuestoNavigation).WithMany(p => p.VotacionPesos)
                .HasForeignKey(d => d.IdPesoPropuesto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votacion_peso_id_peso_propuesto_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.VotacionPesos)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("votacion_peso_id_proyecto_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.VotacionPesos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("votacion_peso_id_usuario_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
