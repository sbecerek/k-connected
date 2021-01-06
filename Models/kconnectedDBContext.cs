using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace k_connected.API.Models
{
    public partial class kconnectedDBContext : DbContext
    {
        public kconnectedDBContext()
        {
        }

        public kconnectedDBContext(DbContextOptions<kconnectedDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<Knowledge> Knowledge { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=127.0.0.1,1433;database=kconnectedDB;User ID=sa;password=sastudent;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Entity__536C85E51A66C9EF");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Knowledge>(entity =>
            {
                entity.Property(e => e.KnowledgeId).HasColumnName("Knowledge_ID");

                entity.Property(e => e.SkillName)
                    .HasColumnName("Skill_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.SkillNameNavigation)
                    .WithMany(p => p.Knowledge)
                    .HasForeignKey(d => d.SkillName)
                    .HasConstraintName("FK__Knowledge__FK_Sk__3C69FB99");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Knowledge)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Knowledge__FK_Us__3B75D760");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.SkillName)
                    .HasName("PK__Skill__90CD761ED7795E6A");

                entity.Property(e => e.SkillName)
                    .HasColumnName("Skill_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
