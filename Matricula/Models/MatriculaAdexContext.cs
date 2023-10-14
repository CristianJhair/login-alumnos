using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Matricula.Models;

public partial class MatriculaAdexContext : DbContext
{
    public MatriculaAdexContext()
    {
    }

    public MatriculaAdexContext(DbContextOptions<MatriculaAdexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAlumno> TbAlumnos { get; set; }

    public virtual DbSet<TbAula> TbAulas { get; set; }

    public virtual DbSet<TbCiclo> TbCiclos { get; set; }

    public virtual DbSet<TbCurso> TbCursos { get; set; }

    public virtual DbSet<TbMatricula> TbMatriculas { get; set; }

    public virtual DbSet<TbSeccion> TbSeccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2JLPE8T\\SQLEXPRESS; Database=MATRICULA_ADEX; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAlumno>(entity =>
        {
            entity.HasKey(e => e.NIdAlumno).HasName("PK__tb_Alumn__6D766B2EDAC7C397");

            entity.ToTable("tb_Alumnos");

            entity.Property(e => e.NIdAlumno).HasColumnName("nIdAlumno");
            entity.Property(e => e.ApellidoAlumno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoAlumno");
            entity.Property(e => e.NIdCiclo).HasColumnName("nIdCiclo");
            entity.Property(e => e.NombreAlumno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreAlumno");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.NIdCicloNavigation).WithMany(p => p.TbAlumnos)
                .HasForeignKey(d => d.NIdCiclo)
                .HasConstraintName("FK__tb_Alumno__nIdCi__3E52440B");
        });

        modelBuilder.Entity<TbAula>(entity =>
        {
            entity.HasKey(e => e.NIdAula).HasName("PK__tb_Aulas__5E666E5BAED66B1D");

            entity.ToTable("tb_Aulas");

            entity.Property(e => e.NIdAula).HasColumnName("nIdAula");
            entity.Property(e => e.NCapacidad).HasColumnName("nCapacidad");
        });

        modelBuilder.Entity<TbCiclo>(entity =>
        {
            entity.HasKey(e => e.NIdCiclo).HasName("PK__tb_Ciclo__C98326D94CCA02F3");

            entity.ToTable("tb_Ciclo");

            entity.Property(e => e.NIdCiclo).HasColumnName("nIdCiclo");
            entity.Property(e => e.NombreCiclo)
                .HasMaxLength(50)
                .HasColumnName("nombre_ciclo");
        });

        modelBuilder.Entity<TbCurso>(entity =>
        {
            entity.HasKey(e => e.NIdCurso).HasName("PK__tb_Curso__3105D2E16E34CC6F");

            entity.ToTable("tb_Cursos");

            entity.Property(e => e.NIdCurso).HasColumnName("nIdCurso");
            entity.Property(e => e.NIdCiclo).HasColumnName("nIdCiclo");
            entity.Property(e => e.SDesCurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sDesCurso");

            entity.HasOne(d => d.NIdCicloNavigation).WithMany(p => p.TbCursos)
                .HasForeignKey(d => d.NIdCiclo)
                .HasConstraintName("FK__tb_Cursos__nIdCi__3B75D760");
        });

        modelBuilder.Entity<TbMatricula>(entity =>
        {
            entity.HasKey(e => new { e.NIdAlumno, e.NIdSeccion }).HasName("PK__tb_Matri__3D91C883022F792F");

            entity.ToTable("tb_Matricula");

            entity.Property(e => e.NIdAlumno).HasColumnName("nIdAlumno");
            entity.Property(e => e.NIdSeccion).HasColumnName("nIdSeccion");
            entity.Property(e => e.NIdCurso).HasColumnName("nIdCurso");

            entity.HasOne(d => d.NIdAlumnoNavigation).WithMany(p => p.TbMatriculas)
                .HasForeignKey(d => d.NIdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Matric__nIdAl__44FF419A");

            entity.HasOne(d => d.NIdCursoNavigation).WithMany(p => p.TbMatriculas)
                .HasForeignKey(d => d.NIdCurso)
                .HasConstraintName("fk_nIdCurso");

            entity.HasOne(d => d.NIdSeccionNavigation).WithMany(p => p.TbMatriculas)
                .HasForeignKey(d => d.NIdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Matric__nIdSe__45F365D3");
        });

        modelBuilder.Entity<TbSeccion>(entity =>
        {
            entity.HasKey(e => e.NIdSeccion).HasName("PK__tb_Secci__0E7A3AD48F3F9982");

            entity.ToTable("tb_Seccion");

            entity.Property(e => e.NIdSeccion).HasColumnName("nIdSeccion");
            entity.Property(e => e.NIdAula).HasColumnName("nIdAula");
            entity.Property(e => e.NIdCurso).HasColumnName("nIdCurso");
            entity.Property(e => e.STurno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sTurno");
            entity.Property(e => e.THoraFin).HasColumnName("tHoraFin");
            entity.Property(e => e.THoraInicio).HasColumnName("tHoraInicio");

            entity.HasOne(d => d.NIdAulaNavigation).WithMany(p => p.TbSeccions)
                .HasForeignKey(d => d.NIdAula)
                .HasConstraintName("FK__tb_Seccio__nIdAu__412EB0B6");

            entity.HasOne(d => d.NIdCursoNavigation).WithMany(p => p.TbSeccions)
                .HasForeignKey(d => d.NIdCurso)
                .HasConstraintName("FK__tb_Seccio__nIdCu__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
