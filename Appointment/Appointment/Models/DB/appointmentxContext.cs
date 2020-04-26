using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace appoimntlastlq.Models.DB
{
    public partial class appointmentxContext : DbContext
    {
        public appointmentxContext()
        {
        }

        public appointmentxContext(DbContextOptions<appointmentxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppTable> AppTable { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Control> Control { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                 optionsBuilder.UseSqlServer("Server=10.0.0.7;Database=appointmentx;User ID=superUser;Password=root123");
                // optionsBuilder.UseSqlServer("Server=DESKTOP-4E3G643;Database=appointmentx;Integrated Security=True");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppTable>(entity =>
            {
                entity.HasKey(e => e.ReNo);

                entity.ToTable("app_Table");

                entity.Property(e => e.ReNo)
                    .HasColumnName("Re_no")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ReArea).HasColumnName("Re_area");

                entity.Property(e => e.ReDate)
                    .HasColumnName("Re_date")
                    .HasColumnType("date");

                entity.Property(e => e.ReNid)
                    .HasColumnName("Re_nid")
                    .HasMaxLength(12);

                entity.Property(e => e.RePersonId)
                    .HasColumnName("Re_PersonID")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ReQuidNo)
                    .HasColumnName("Re_QuidNo")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.AreaName).HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityId)
                    .HasColumnName("CityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Control>(entity =>
            {
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
