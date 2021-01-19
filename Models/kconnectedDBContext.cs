using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<CodeJam> CodeJams { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Knowledge> Knowledges { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=127.0.0.1,1433;database=kconnectedDB;User ID=sa;password=sastudent;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CodeJam>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CodeJam");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CodejamId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CodejamID");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descp).HasColumnType("text");

                entity.Property(e => e.Host)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.HostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Host)
                    .HasConstraintName("FK__CodeJam__Host__5BE2A6F2");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Entity__536C85E51A66C9EF");

                entity.ToTable("Entity");

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
                    .HasMaxLength(320)
                    .IsUnicode(false)
                    .HasColumnName("email");

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
                entity.ToTable("Knowledge");

                entity.Property(e => e.KnowledgeId).HasColumnName("Knowledge_ID");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Skill_name");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.SkillNameNavigation)
                    .WithMany(p => p.Knowledges)
                    .HasForeignKey(d => d.SkillName)
                    .HasConstraintName("FK__Knowledge__FK_Sk__3C69FB99");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Knowledges)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Knowledge__FK_Us__3B75D760");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.SkillName)
                    .HasName("PK__Skill__90CD761ED7795E6A");

                entity.ToTable("Skill");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Skill_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
