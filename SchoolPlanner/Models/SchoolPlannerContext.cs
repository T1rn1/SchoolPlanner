using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SchoolPlanner.Models;

public partial class SchoolPlannerContext : DbContext
{
    public SchoolPlannerContext()
    {
    }

    public SchoolPlannerContext(DbContextOptions<SchoolPlannerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cycliccommission> Cycliccommissions { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<Lessontype> Lessontypes { get; set; }

    public virtual DbSet<Pass> Passes { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=schoolplanner;user=root;password=1905", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cycliccommission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cycliccommission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grade");

            entity.HasIndex(e => e.IdLessonType, "ID_lessonType");

            entity.HasIndex(e => e.IdSubject, "ID_subject");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Grade1).HasColumnName("grade");
            entity.Property(e => e.IdLessonType).HasColumnName("ID_lessonType");
            entity.Property(e => e.IdSubject).HasColumnName("ID_subject");

            entity.HasOne(d => d.IdLessonTypeNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.IdLessonType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grade_ibfk_1");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Grades)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grade_ibfk_2");
        });

        modelBuilder.Entity<Homework>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("homework");

            entity.HasIndex(e => e.IdSubject, "ID_subject");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdSubject).HasColumnName("ID_subject");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Tusk)
                .HasMaxLength(20)
                .HasColumnName("tusk");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("homework_ibfk_1");
        });

        modelBuilder.Entity<Lessontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lessontype");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Pass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pass");

            entity.HasIndex(e => e.IdReason, "ID_reason");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdReason).HasColumnName("ID_reason");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Time)
                .HasColumnType("time")
                .HasColumnName("time");

            entity.HasOne(d => d.IdReasonNavigation).WithMany(p => p.Passes)
                .HasForeignKey(d => d.IdReason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pass_ibfk_1");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reason");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(35)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("schedule");

            entity.HasIndex(e => e.IdPass, "ID_pass");

            entity.HasIndex(e => e.IdSubject, "ID_subject");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Class).HasColumnName("class");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdPass).HasColumnName("ID_pass");
            entity.Property(e => e.IdSubject).HasColumnName("ID_subject");
            entity.Property(e => e.Time)
                .HasColumnType("time")
                .HasColumnName("time");

            entity.HasOne(d => d.IdPassNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdPass)
                .HasConstraintName("schedule_ibfk_1");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("schedule_ibfk_2");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("subject");

            entity.HasIndex(e => e.IdCyclicCommission, "ID_cyclicCommission");

            entity.HasIndex(e => e.IdTeacher, "ID_teacher");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdCyclicCommission).HasColumnName("ID_cyclicCommission");
            entity.Property(e => e.IdTeacher).HasColumnName("ID_teacher");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");

            entity.HasOne(d => d.IdCyclicCommissionNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdCyclicCommission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_ibfk_1");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdTeacher)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("subject_ibfk_2");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teacher");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .HasColumnName("fullName");
            entity.Property(e => e.TelephoneNumber).HasColumnName("telephoneNumber");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(15)
                .HasColumnName("workingHours");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
